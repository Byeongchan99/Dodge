using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    /****************************************************************************
                                 private Fields
    ****************************************************************************/
    private List<IHealthObserver> observers = new List<IHealthObserver>(); // 체력 옵저버 리스트

    [SerializeField] private int _maxHealth; // 플레이어 최대 체력
    [SerializeField] private float _initialMoveSpeed; // 플레이어 초기 이동 속도

    private Coroutine invincibilityRoutine = null; // 무적 상태 코루틴
    private float _remainingInvincibilityDuration = 0f; // 남은 무적 상태 지속 시간

    private int _currentCharacterType = 0; // 기본 캐릭터 타입
    [SerializeField] private Blink blink; // 플레이어 점멸 능력
    [SerializeField] private EMP emp; // 플레이어 EMP 능력
    [SerializeField] private DefenseProtocol defenseProtocol; // 플레이어 방어 프로토콜 능력

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // 현재 적용 중인 아이템 효과 리스트

    /****************************************************************************
                             public Fields
    ****************************************************************************/
    public static PlayerStat Instance { get; private set; } // 싱글톤 인스턴스

    public Transform currentPosition; // 플레이어 현재 위치
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public int currentHealth; // 플레이어 현재 체력
    public float currentMoveSpeed; // 플레이어 현재 이동 속도

    public bool isInvincibility; // 무적 상태인지 여부 

    public IPlayerAbility playerAbility; // 플레이어 특수 능력

    // 캐릭터 타입을 위한 enum
    public enum CharacterType
    {
        Light,
        Medium,
        Heavy
    }

    // 캐릭터 데이터
    [System.Serializable]
    public class CharacterData
    {
        public Sprite characterSprite;
        public RuntimeAnimatorController animatorController;  // 애니메이터 컨트롤러 추가
        public int characterTypeIndex; // 캐릭터 타입 인덱스
    }

    // 캐릭터 데이터 리스트
    public List<CharacterData> characterList;

    /****************************************************************************
                               Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
            Init();
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    void Update()
    {
        // 플레이어가 움직일 때마다 currentPosition을 업데이트
        currentPosition.position = this.transform.position;
    }

    /****************************************************************************
                                private Methods
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        isInvincibility = false;

        // 기본 캐릭터 설정
        SetCharacter();
    }

    /// <summary> 깜빡임 효과 코루틴 </summary>
    private IEnumerator FlickerEffect(float duration)
    {
        float timeLeft = duration;  // 남은 시간을 추적하는 변수
        while (timeLeft > 0)
        {
            // 깜빡이는 이펙트: 반투명
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.1f);  // 0.1초 대기

            // 깜빡이는 이펙트: 원래 상태
            spriteRenderer.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.1f);  // 0.1초 대기

            timeLeft -= 0.2f;  // 지연 시간(0.1 + 0.1)만큼 시간 감소
        }
        // 루프 종료 후 색상 원상 복구
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    /// <summary> 무적 시간 적용 타이머 </summary>
    private IEnumerator ApplyInvincibility(float duration)
    {
        Debug.Log("무적 상태 시작");
        isInvincibility = true;

        _remainingInvincibilityDuration = duration;  // 무적 지속 시간 업데이트
        while (_remainingInvincibilityDuration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            _remainingInvincibilityDuration -= 0.1f;  // 실제 시간 감소로 업데이트
        }

        Debug.Log("무적 상태 종료");
        invincibilityRoutine = null;
        isInvincibility = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    /// <summary> 아이템 효과 적용 타이머 </summary>
    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
    }

    /****************************************************************************
                                public Methods
    ****************************************************************************/
    /// <summary> 캐릭터 선택 버튼 클릭 </summary>
    public void OnCharacterTypeClicked(int typeIndex)
    {
        _currentCharacterType = typeIndex;
        Debug.Log("선택한 캐릭터 타입: " + _currentCharacterType);
    }

    /// <summary> 캐릭터 변경 </summary>
    public void SetCharacter()
    {
        Debug.Log("캐릭터 변경: " + _currentCharacterType);
        if (characterList[_currentCharacterType] != null)
        {
            CharacterData data = characterList[_currentCharacterType];
            spriteRenderer.sprite = data.characterSprite;
            animator.runtimeAnimatorController = data.animatorController;  // 애니메이터 컨트롤러 설정

            // 플레이어 타입에 따른 체력과 어빌리티 설정
            if (_currentCharacterType == 0)
            {
                playerAbility = blink;
                _maxHealth = 2;
            }
            else if (_currentCharacterType == 1)
            {
                playerAbility = emp;
                _maxHealth = 3;
            }
            else if (_currentCharacterType == 2)
            {
                playerAbility = defenseProtocol;
                _maxHealth = 4;
            }
        }
        currentHealth = _maxHealth;
    }

    /// <summary> 데미지 처리 </summary>
    public void TakeDamage()
    {
        if (!isInvincibility) // 무적 상태가 아닐 때
        {
            // 피격 처리
            currentHealth--;
            NotifyObservers();
            Debug.Log("피격! 현재 체력: " + currentHealth);
            StartCoroutine(FlickerEffect(1.5f)); // 1.5초 동안 깜빡임 효과
            StartInvincibility(1.5f); // 1초 동안 무적
        }
        // 나중에 피격 로직 수정
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 플레이어 파괴
        }
    }

    /// <summary> 무적 효과 적용 </summary>
    public void StartInvincibility(float duration)
    {
        if (invincibilityRoutine != null && _remainingInvincibilityDuration > duration)
        {
            Debug.Log("현재 무적 효과가 남아있는 시간이 더 길므로 새 요청 무시");
            return;  // 현재 남아있는 슬로우 모션이 더 길면 새 요청 무시
        }

        if (invincibilityRoutine != null)
        {
            StopCoroutine(invincibilityRoutine);  // 이전 슬로우 모션 코루틴 중지
        }

        invincibilityRoutine = StartCoroutine(ApplyInvincibility(duration));
    }


    /// <summary> 아이템 효과 적용 </summary>
    public void ApplyItemEffect(ItemEffect effect)
    {
        // 버프 적용 처리
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
    }

    /// <summary> 아이템 효과 제거 </summary>
    public void RemoveAllEffects()
    {
        foreach (ItemEffect effect in activeItems)
        {
            effect.RemoveEffect();
        }
        activeItems.Clear();
    }

    /****************************************************************************
                               Observer Pattern
    ****************************************************************************/
    /// <summary> 옵저버 등록 </summary>
    public void RegisterObserver(IHealthObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    /// <summary> 옵저버 해제 </summary>
    public void UnregisterObserver(IHealthObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    /// <summary> 옵저버에게 알림 </summary>
    private void NotifyObservers()
    {
        foreach (IHealthObserver observer in observers)
        {
            observer.OnHealthChanged(currentHealth);
        }
    }
}

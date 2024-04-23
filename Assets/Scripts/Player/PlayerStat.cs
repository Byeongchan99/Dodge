using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // 싱글톤 인스턴스

    public Transform currentPosition; // 플레이어 현재 위치

    [SerializeField] private int _maxHealth; // 플레이어 최대 체력
    public int currentHealth; // 플레이어 현재 체력
    [SerializeField] private float _initialMoveSpeed; // 플레이어 초기 이동 속도
    public float currentMoveSpeed; // 플레이어 현재 이동 속도

    private Coroutine invincibilityRoutine = null; // 무적 상태 코루틴
    private float _remainingInvincibilityDuration = 0f; // 남은 무적 상태 지속 시간
    public bool isInvincibility; // 무적 상태인지 여부 

    public IPlayerAbility playerAbility; // 플레이어 특수 능력
    [SerializeField] private Blink blink; // 플레이어 점멸 능력
    [SerializeField] private EMP emp; // 플레이어 EMP 능력
    [SerializeField] private DefenseProtocol defenseProtocol; // 플레이어 방어 프로토콜 능력

    private List<ItemEffect> activeItems = new List<ItemEffect>(); // 현재 적용 중인 아이템 효과 리스트

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

    /// <summary> 초기화 </summary>
    void Init()
    {
        currentHealth = _maxHealth;
        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        isInvincibility = false;
        // 특수 능력 선택 로직 나중에 추가하기
        this.SetAbility(blink);
    }

    /// <summary> 어빌리티 설정 </summary>
    public void SetAbility(IPlayerAbility newAbility)
    {
        this.playerAbility = newAbility;
    }

    /// <summary> 데미지 처리 </summary>
    public void TakeDamage()
    {
        if (!isInvincibility) // 무적 상태가 아닐 때
        {
            // 피격 처리
            currentHealth--;
            Debug.Log("피격! 현재 체력: " + currentHealth);
            StartInvincibility(1.5f); // 1초 동안 무적
        }
        // 나중에 피격 로직 수정
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 플레이어 파괴
        }
    }

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

    /// <summary> 무적 시간 적용 타이머 </summary>
    private IEnumerator ApplyInvincibility(float duration)
    {
        Debug.Log("무적 상태 시작");
        isInvincibility = true;
        
        _remainingInvincibilityDuration = duration;  // 무적 지속 시간 업데이트
        while (_remainingInvincibilityDuration > 0)
        {
            yield return null;
            _remainingInvincibilityDuration -= Time.deltaTime;  // 실제 시간 감소로 업데이트
        }

        invincibilityRoutine = null;
        isInvincibility = false;
    }

    /// <summary> 아이템 효과 적용 </summary>
    public void ApplyItemEffect(ItemEffect effect)
    {
        // 버프 적용 처리
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
    }

    /// <summary> 아이템 효과 적용 타이머 </summary>
    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
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
}

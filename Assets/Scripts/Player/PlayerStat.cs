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

    void Init()
    {
        currentHealth = _maxHealth;
        currentMoveSpeed = _initialMoveSpeed;
        currentPosition = transform;
        // 특수 능력 선택 로직 나중에 추가하기
        this.SetAbility(emp);
    }

    public void SetAbility(IPlayerAbility newAbility)
    {
        this.playerAbility = newAbility;
    }

    public void TakeDamage()
    {
        if (!isInvincibility) // 무적 상태가 아닐 때
        {
            // 피격 처리
            currentHealth--;
        }
        // 나중에 피격 로직 수정
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 플레이어 파괴
        }
    }

    public void ApplyItemEffect(ItemEffect effect)
    {
        // 버프 적용 처리
        effect.ApplyEffect();
        activeItems.Add(effect);
        StartCoroutine(RemoveItemEffectEffectAfterDuration(effect));
    }

    private IEnumerator RemoveItemEffectEffectAfterDuration(ItemEffect effect)
    {
        yield return new WaitForSecondsRealtime(effect.GetDuration());
        effect.RemoveEffect();
        activeItems.Remove(effect);
    }

    public void RemoveAllEffects()
    {
        foreach (ItemEffect effect in activeItems)
        {
            effect.RemoveEffect();
        }
        activeItems.Clear();
    }
}

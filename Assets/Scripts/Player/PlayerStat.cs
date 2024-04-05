using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // 싱글톤 인스턴스

    public Transform currentPosition; // 플레이어 현재 위치

    public int maxHealth = 3; // 플레이어 최대 체력
    public int currentHealth; // 플레이어 현재 체력
    public float initialMoveSpeed = 150f; // 플레이어 초기 이동 속도
    public float currentMoveSpeed; // 플레이어 현재 이동 속도

    public IPlayerAbility playerAbility; // 플레이어 특수 능력
    public Blink blink; // 플레이어 점멸 능력

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
        currentHealth = maxHealth;
        currentMoveSpeed = initialMoveSpeed;
        currentPosition = transform;
        this.SetAbility(blink);
    }

    public void SetAbility(IPlayerAbility newAbility)
    {
        this.playerAbility = newAbility;
    }

    public void TakeDamage()
    {
        // 피격 처리
        currentHealth--;
        // 나중에 피격 로직 수정
        if (currentHealth <= 0)
        {
            Destroy(gameObject); // 플레이어 파괴
        }
    }

    public void ApplyBuff()
    {
        // 버프 적용 처리
    }

    public void RemoveBuff()
    {
        // 버프 제거 처리
    }
}

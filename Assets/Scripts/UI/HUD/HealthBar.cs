using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IHealthObserver
{
    void OnHealthChanged(float health);
}

public class HealthBar : MonoBehaviour, IHealthObserver
{
    [SerializeField] private GameObject[] healthUnits; // 체력 칸을 나타내는 GameObject 배열

    void Start()
    {
        // 플레이어 체력 옵저버 등록
        PlayerStat.Instance.RegisterObserver(this);
        OnHealthChanged(PlayerStat.Instance.MaxHealth);
    }

    // 플레이어 체력이 변경될 때 호출되는 메서드
    public void OnHealthChanged(float health)
    {
        //Debug.Log("체력 칸 활성화");
        // 체력 칸 활성화
        for (int i = 0; i < PlayerStat.Instance.MaxHealth; i++)
        {
            if (i < PlayerStat.Instance.currentHealth)
            {
                healthUnits[i].SetActive(true);
            }
            else
            {
                healthUnits[i].SetActive(false);
            }
        }
    }

    void OnDestroy()
    {
        // 옵저버 해제
        if (PlayerStat.Instance != null)
        {
            PlayerStat.Instance.UnregisterObserver(this);
        }
    }
}

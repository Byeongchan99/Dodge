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
    private GameObject[] healthUnits; // 체력 칸을 나타내는 GameObject 배열
    PlayerStat playerStat = FindObjectOfType<PlayerStat>();

    void Start()
    {
        // 플레이어 객체를 찾아서 옵저버로 등록
        if (playerStat != null)
        {
            playerStat.RegisterObserver(this);
        }
    }

    public void OnHealthChanged(float health)
    {
        // 체력 칸 활성화
        for (int i = 0; i < healthUnits.Length; i++)
        {
            if (i < playerStat.currentHealth)
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
        if (playerStat != null)
        {
            playerStat.UnregisterObserver(this);
        }
    }
}

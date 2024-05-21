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
    [SerializeField] private GameObject[] healthUnits; // ü�� ĭ�� ��Ÿ���� GameObject �迭
    PlayerStat playerStat;

    void Start()
    {
        playerStat = FindObjectOfType<PlayerStat>();
        // �÷��̾� ��ü�� ã�Ƽ� �������� ���
        if (playerStat != null)
        {
            playerStat.RegisterObserver(this);
            // OnHealthChanged(playerStat.MaxHealth);
        }
    }

    public void OnHealthChanged(float health)
    {
        // ü�� ĭ Ȱ��ȭ
        for (int i = 0; i < playerStat.MaxHealth; i++)
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
        // ������ ����
        if (playerStat != null)
        {
            playerStat.UnregisterObserver(this);
        }
    }
}

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
    private GameObject[] healthUnits; // ü�� ĭ�� ��Ÿ���� GameObject �迭
    PlayerStat playerStat = FindObjectOfType<PlayerStat>();

    void Start()
    {
        // �÷��̾� ��ü�� ã�Ƽ� �������� ���
        if (playerStat != null)
        {
            playerStat.RegisterObserver(this);
        }
    }

    public void OnHealthChanged(float health)
    {
        // ü�� ĭ Ȱ��ȭ
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
        // ������ ����
        if (playerStat != null)
        {
            playerStat.UnregisterObserver(this);
        }
    }
}

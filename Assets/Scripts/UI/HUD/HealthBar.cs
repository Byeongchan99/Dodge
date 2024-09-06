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

    void Start()
    {
        // �÷��̾� ü�� ������ ���
        PlayerStat.Instance.RegisterObserver(this);
        OnHealthChanged(PlayerStat.Instance.MaxHealth);
    }

    // �÷��̾� ü���� ����� �� ȣ��Ǵ� �޼���
    public void OnHealthChanged(float health)
    {
        //Debug.Log("ü�� ĭ Ȱ��ȭ");
        // ü�� ĭ Ȱ��ȭ
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
        // ������ ����
        if (PlayerStat.Instance != null)
        {
            PlayerStat.Instance.UnregisterObserver(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // �ӽ� �ð� ǥ��
    [SerializeField] private GameObject timerUI; // Ÿ�̸� UI
    [SerializeField] private GameObject healthBar; // ü�¹� UI
    [SerializeField] private GameObject cooldownTimer; // ��Ÿ�� Ÿ�̸� UI

    // Ÿ�̸� UI Ȱ��ȭ
    public void EnableTimer()
    {
        timerUI.SetActive(true);
    }

    // Ÿ�̸� UI ��Ȱ��ȭ
    public void DisableTimer()
    {
        timerUI.SetActive(false);
    }

    // ��Ÿ�� Ÿ�̸� UI ������Ʈ
    public void UpdateTimerUI(float time)
    {
        timeText.text = "Score: " + time.ToString("F2");
    }

    // ��Ÿ�� Ÿ�̸� UI Ȱ��ȭ
    public void EnableHealthBar()
    {
        healthBar.SetActive(true);
    }

    // ��Ÿ�� Ÿ�̸� UI ��Ȱ��ȭ
    public void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }
}

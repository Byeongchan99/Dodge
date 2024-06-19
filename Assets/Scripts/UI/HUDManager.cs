using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // �ӽ� �ð� ǥ��
    [SerializeField] private GameObject timerUI; // Ÿ�̸� UI
    [SerializeField] private GameObject healthBar; // ü�¹� UI

    public void EnableTimer()
    {
        timerUI.SetActive(true);
    }

    public void DisableTimer()
    {
        timerUI.SetActive(false);
    }

    public void UpdateTimerUI(float time)
    {
        timeText.text = "Score: " + time.ToString("F2");
    }

    public void EnableHealthBar()
    {
        healthBar.SetActive(true);
    }

    public void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }
}

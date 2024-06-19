using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // 임시 시간 표시
    [SerializeField] private GameObject timerUI; // 타이머 UI
    [SerializeField] private GameObject healthBar; // 체력바 UI

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

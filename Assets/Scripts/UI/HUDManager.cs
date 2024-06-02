using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // 임시 시간 표시
    [SerializeField] private GameObject timerUI; // 타이머 UI
    [SerializeField] private GameObject healthBar; // 체력바 UI

    public void ActiveTimer()
    {
        timerUI.SetActive(true);
    }

    public void UpdateTimerUI(float time)
    {
        timeText.text = "Time: " + time.ToString("F2");
    }

    public void ActiveHealthBar()
    {
        healthBar.SetActive(true);
    }
}

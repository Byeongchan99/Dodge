using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // 임시 시간 표시
    [SerializeField] private GameObject timerUI; // 타이머 UI
    [SerializeField] private GameObject healthBar; // 체력바 UI
    [SerializeField] private GameObject cooldownTimer; // 쿨타임 타이머 UI

    // 타이머 UI 활성화
    public void EnableTimer()
    {
        timerUI.SetActive(true);
    }

    // 타이머 UI 비활성화
    public void DisableTimer()
    {
        timerUI.SetActive(false);
    }

    // 쿨타임 타이머 UI 업데이트
    public void UpdateTimerUI(float time)
    {
        timeText.text = "Score: " + time.ToString("F2");
    }

    // 쿨타임 타이머 UI 활성화
    public void EnableHealthBar()
    {
        healthBar.SetActive(true);
    }

    // 쿨타임 타이머 UI 비활성화
    public void DisableHealthBar()
    {
        healthBar.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // 임시 시간 표시
    [SerializeField] private GameObject timerUI; // 타이머 UI

    private IEnumerator timerCoroutine;
    private float timer = 0f;
    private bool isPaused = false;

    [SerializeField] private GameObject healthBar; // 체력바 UI

    public void ActiveTimer()
    {
        timerUI.SetActive(true);
        timerCoroutine = RunTimer();
        StartCoroutine(timerCoroutine);
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            if (!isPaused)
            {
                timer += Time.unscaledDeltaTime;
                timeText.text = "Time: " + timer.ToString();

            }
            yield return null;  // 다음 프레임까지 기다림
        }
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    public void ActiveHealthBar()
    {
        healthBar.SetActive(true);
    }
}

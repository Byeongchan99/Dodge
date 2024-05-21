using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // 임시 시간 표시
    [SerializeField] Text playerHealth; // 임시 플레이어 체력 표시

    private IEnumerator timerCoroutine;
    private float timer = 0f;
    private bool isPaused = false;

    // 임시 시간 표시
    private void Update()
    {
        playerHealth.text = "Player HP: " + PlayerStat.Instance.currentHealth.ToString();
    }

    public void ActiveTimer()
    {
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

    void ActiveHealthBar()
    {

    }
}

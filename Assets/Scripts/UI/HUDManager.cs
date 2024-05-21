using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // �ӽ� �ð� ǥ��
    [SerializeField] Text playerHealth; // �ӽ� �÷��̾� ü�� ǥ��

    private IEnumerator timerCoroutine;
    private float timer = 0f;
    private bool isPaused = false;

    // �ӽ� �ð� ǥ��
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
            yield return null;  // ���� �����ӱ��� ��ٸ�
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

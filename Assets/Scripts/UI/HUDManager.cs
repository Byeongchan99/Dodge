using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Text timeText; // �ӽ� �ð� ǥ��
    [SerializeField] private GameObject timerUI; // Ÿ�̸� UI
    [SerializeField] private GameObject healthBar; // ü�¹� UI

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

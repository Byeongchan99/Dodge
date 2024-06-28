using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUpdateUI
{
    void UpdateUIInfo(params object[] data);
}

public class PauseUI : MonoBehaviour, IUpdateUI
{
    public Text stageNameText;
    public Text currentScoreText;

    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageManager stageManager;

    public void OpenPauseUI()
    {
        HUDManager.DisableTimer();

        Time.timeScale = 0f;
        // 타이머 일시 정지
        ScoreManager.Instance.PauseTimer();

        if (stageManager != null)
        {
            UpdateUIInfo(stageManager.GetCurrentStageData());
        }
        else
        {
            Debug.Log("stageManager 할당 필요");
        }
    }

    public void ClosePauseUI()
    {
        HUDManager.EnableTimer();

        Time.timeScale = 1f;
        // 타이머 재개
        ScoreManager.Instance.ResumeTimer();
    }

    public void UpdateUIInfo(params object[] datas)
    {
        StageData stageData = null; // 초기화

        foreach (var data in datas)
        {
            if (data is StageData)
                stageData = data as StageData;
        }

        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            currentScoreText.text = "현재 점수: " + ScoreManager.Instance.GetCurrentScore();
        }
    }

    public void OnOptionClick()
    {

    }

    public void OnResumeClick()
    {
        ClosePauseUI();
    }

    public void OnRestartClick()
    {

    }

    public void OnExitClick()
    {

    }
}

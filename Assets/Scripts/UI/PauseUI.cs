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
        // Ÿ�̸� �Ͻ� ����
        ScoreManager.Instance.PauseTimer();

        if (stageManager != null)
        {
            UpdateUIInfo(stageManager.GetCurrentStageData());
        }
        else
        {
            Debug.Log("stageManager �Ҵ� �ʿ�");
        }
    }

    public void ClosePauseUI()
    {
        HUDManager.EnableTimer();

        Time.timeScale = 1f;
        // Ÿ�̸� �簳
        ScoreManager.Instance.ResumeTimer();
    }

    public void UpdateUIInfo(params object[] datas)
    {
        StageData stageData = null; // �ʱ�ȭ

        foreach (var data in datas)
        {
            if (data is StageData)
                stageData = data as StageData;
        }

        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            currentScoreText.text = "���� ����: " + ScoreManager.Instance.GetCurrentScore();
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

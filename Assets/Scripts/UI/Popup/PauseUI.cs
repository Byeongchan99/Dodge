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
    float _lastTimeScale;

    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageManager stageManager;

    // �Ͻ�����â ����
    public void OpenPauseUI()
    {
        HUDManager.DisableTimer();

        _lastTimeScale = Time.timeScale; // ���ο� ȿ�� ���� ���� ���� ����Ͽ� ����
        Time.timeScale = 0f;
        // Ÿ�̸� �Ͻ� ����
        ScoreManager.Instance.PauseTimer();

        GameManager.Instance.isPaused = true;

        if (stageManager != null)
        {
            UpdateUIInfo(stageManager.GetCurrentStageData());
        }
        else
        {
            Debug.Log("stageManager �Ҵ� �ʿ�");
        }
    }

    // �Ͻ�����â �ݱ�
    public void ClosePauseUI()
    {
        HUDManager.EnableTimer();

        Time.timeScale = _lastTimeScale;
        // Ÿ�̸� �簳
        ScoreManager.Instance.ResumeTimer();
        
        GameManager.Instance.isPaused = false;
    }

    // �Ͻ�����â�� �������� ���� ������Ʈ
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
            currentScoreText.text = "Current Score: " + Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore());
        }
    }

    public void OnOptionClick()
    {

    }

    // ����ϱ� ��ư Ŭ��
    public void OnResumeClick()
    {
        ClosePauseUI();
    }

    // �ٽ��ϱ� ��ư Ŭ��
    public void OnRestartClick()
    {
        ClosePauseUI();
        stageManager.RestartStage();
    }

    // ������ ��ư Ŭ��
    public void OnExitClick()
    {
        ClosePauseUI();
        stageManager.ExitStage();
    }
}

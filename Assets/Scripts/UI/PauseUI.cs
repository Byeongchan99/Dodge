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
}

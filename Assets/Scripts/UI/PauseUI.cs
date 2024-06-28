using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Text stageNameText;
    public Text currentScoreText;

    public void UpdatePauseUI(StageData stageData)
    {
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            currentScoreText.text = "현재 점수: " + ScoreManager.Instance.GetCurrentScore();
        }
    }
}

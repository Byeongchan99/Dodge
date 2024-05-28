using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    public Text stageNameText;
    public Text stageInformationText;

    public void UpdateStageInfo(StageData stageData)
    {
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            stageInformationText.text = "Stat Data: " + stageData.statDataName;
        }
    }
}

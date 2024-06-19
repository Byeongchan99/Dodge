using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    public Text stageNameText;
    public Text stageInformationText;
    public GameObject isGameClearImage;
    [SerializeField] private GameObject[] StarUnits; // 별 칸을 나타내는 GameObject 배열

    public void UpdateStageInfo(StageData stageData, UserData userData)
    {
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            stageInformationText.text = stageData.stageInformation;
        }

        // 임시 유저 데이터 사용
        if (userData != null)
        {
            if (userData.stageInfos[stageData.stageID].isCleared)
            {
                isGameClearImage.SetActive(true);
            }
            else
            {
                isGameClearImage.SetActive(false);
            }
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
        }
    }

    public void OnStarChanged(float score)
    {
        // 별 활성화
        for (int i = 0; i < 3; i++)
        {
            if (i < (score / 50) - 1)  
            {
                StarUnits[i].SetActive(true);
            }
            else
            {
                StarUnits[i].SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour
{
    public Image characterImage;
    public Text stageNameText;
    [SerializeField] private GameObject[] StarUnits; // 별 칸을 나타내는 GameObject 배열

    public void UpdateStageResult(StageData stageData, UserData userData)
    {

        characterImage.sprite = PlayerStat.Instance.currentCharacterData.characterSprite;
        
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
        }

        if (userData != null)
        {
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
        }
    }

    public void OnStarChanged(float score)
    {
        // 별 활성화
        for (int i = 0; i < 3; i++)
        {
            if (i < score / 30 - 1)
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

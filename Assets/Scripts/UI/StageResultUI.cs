using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour, IUpdateUI
{
    public Image characterImage;
    public Text stageNameText;
    public Text stageScore;
    [SerializeField] private GameObject[] StarUnits; // 별 칸을 나타내는 GameObject 배열

    public void UpdateUIInfo(params object[] datas)
    {
        StageData stageData = null;
        UserData userData = null;

        foreach (var data in datas)
        {
            if (data is StageData)
            {
                stageData = data as StageData;
            }
            else if (data is UserData)
            {
                userData = data as UserData;
            }
        }

        Debug.Log("PlayerStat.Instance.currentCharacterData: " + PlayerStat.Instance.currentCharacterData.characterTypeIndex);
        characterImage.sprite = PlayerStat.Instance.currentCharacterData.characterSprite;
        
        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
        }

        if (userData != null)
        {
            stageScore.text = "획득 점수: " + ScoreManager.Instance.GetCurrentScore();
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

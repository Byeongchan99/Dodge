using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour, IUpdateUI
{
    public Text stageNameText;
    public Text stageInformationText;
    public Text highScore;
    public Animator animator; // 애니메이터 컴포넌트
    public GameObject isGameClearImage;
    [SerializeField] private GameObject[] StarUnits; // 별 칸을 나타내는 GameObject 배열

    public LeaderboardsManager leaderboardsManager;

    public void UpdateUIInfo(params object[] datas)
    {
        StageData stageData = null;
        UserData userData = null;

        foreach (var data in datas)
        {
            if (data is StageData)
            {
                stageData = data as StageData;
                animator.runtimeAnimatorController = stageData.turretAnimatorController;
            }
            else if (data is UserData)
            {
                userData = data as UserData;
            }
        }

        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            stageInformationText.text = stageData.stageInformation;
            SetLeaderboardID(stageData.leaderboardID);
            //animator.Play("");
        }

        // 임시 유저 데이터 사용
        if (userData != null)
        {
            /*
            if (userData.stageInfos[stageData.stageID].isCleared)
            {
                isGameClearImage.SetActive(true);
            }
            else
            {
                isGameClearImage.SetActive(false);
            }
            */
            highScore.text = "High Score: " + userData.stageInfos[stageData.stageID].score.ToString();
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

    public void SetLeaderboardID(string leaderboardID)
    {
        leaderboardsManager.SetLeaderboardID(leaderboardID);
    }
}

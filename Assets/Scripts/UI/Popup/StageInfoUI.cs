using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour, IUpdateUI
{
    public Text stageNameText;
    public Text stageInformationText;
    public Text highScore;
    public Animator animator; // �ִϸ����� ������Ʈ
    public GameObject isGameClearImage;
    [SerializeField] private GameObject[] StarUnits; // �� ĭ�� ��Ÿ���� GameObject �迭

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

        // �ӽ� ���� ������ ���
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
            highScore.text = "My High Score: " + userData.stageInfos[stageData.stageID].score.ToString();
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
            // �񵿱� �۾��� ������ void �޼���� ����
            //GetAndDisplayPlayerScore(userData.playerID);
        }
    }

    /*
    // �񵿱� �۾��� �����ϰ� ����� ó���ϴ� �޼���
    private async void GetAndDisplayPlayerScore(string playerID)
    {
        int playerScore = await GetScoreByPlayerIDAsync(playerID);

        // UI ������Ʈ
        highScore.text = "High Score: " + playerScore;
        OnStarChanged(playerScore);
    }

    // �񵿱������� ������ �������� �޼���
    public async Task<int> GetScoreByPlayerIDAsync(string playerID)
    {
        return await leaderboardsManager.GetScoreByPlayerId(playerID);
    }
    */

    // ������ ���� �� Ȱ��ȭ
    public void OnStarChanged(int score)
    {
        // �� Ȱ��ȭ
        for (int i = 0; i < 3; i++)
        {
            if (i < (score / 50))  
            {
                StarUnits[i].SetActive(true);
            }
            else
            {
                StarUnits[i].SetActive(false);
            }
        }
    }

    // �������� ID ����
    public void SetLeaderboardID(string leaderboardID)
    {
        leaderboardsManager.SetLeaderboardID(leaderboardID);
    }
}

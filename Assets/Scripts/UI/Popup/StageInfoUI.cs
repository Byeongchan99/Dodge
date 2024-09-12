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
            highScore.text = "My High Score: " + userData.stageInfos[stageData.stageID].score.ToString();
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
            // 비동기 작업을 별도의 void 메서드로 실행
            //GetAndDisplayPlayerScore(userData.playerID);
        }
    }

    /*
    // 비동기 작업을 수행하고 결과를 처리하는 메서드
    private async void GetAndDisplayPlayerScore(string playerID)
    {
        int playerScore = await GetScoreByPlayerIDAsync(playerID);

        // UI 업데이트
        highScore.text = "High Score: " + playerScore;
        OnStarChanged(playerScore);
    }

    // 비동기적으로 점수를 가져오는 메서드
    public async Task<int> GetScoreByPlayerIDAsync(string playerID)
    {
        return await leaderboardsManager.GetScoreByPlayerId(playerID);
    }
    */

    // 점수에 따라 별 활성화
    public void OnStarChanged(int score)
    {
        // 별 활성화
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

    // 리더보드 ID 설정
    public void SetLeaderboardID(string leaderboardID)
    {
        leaderboardsManager.SetLeaderboardID(leaderboardID);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class UserDataManager : MonoBehaviour
{
    public UserData userData;
    [SerializeField] StageManager stageDataManager;
    [SerializeField] LeaderboardsManager leaderboardsManager;

    private void Awake()
    {

    }

    public void SaveUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 ScriptableObject 데이터를 저장
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    // 비동기적으로 점수를 가져오는 메서드
    public async Task<int> GetScoreByPlayerIDAsync(string playerID, string leaderboardID)
    {
        return await leaderboardsManager.GetScoreByPlayerId(playerID, leaderboardID);
    }

    // 비동기 작업을 수행하고 결과를 처리하는 메서드
    private async void LoadPlayerScore(int index, string playerID, string leaderboardID)
    {
        int playerScore = await GetScoreByPlayerIDAsync(playerID, leaderboardID);
        userData.stageInfos[index].score = playerScore;

        if (playerScore >= 100)
        {
            userData.stageInfos[index].isCleared = true;
        }
    }

    public void LoadUserData()
    {
        List<string> stageLeaderboardIDList = stageDataManager.returnLeaderboardID(); // 스테이지 리더보드 ID 리스트

        userData.playerID = AuthenticationService.Instance.PlayerId; // 리더보드에 사용되는 현재 유저의 ID 받아오기

        for (int i = 0; i < stageLeaderboardIDList.Count; i++)
        {
            LoadPlayerScore(i, userData.playerID, stageLeaderboardIDList[i]);
        }

#if UNITY_EDITOR
        // 에디터 모드에서는 로컬 데이터를 사용
#endif
    }
}

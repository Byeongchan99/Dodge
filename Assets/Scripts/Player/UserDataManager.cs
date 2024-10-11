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
        // ������ ��忡�� ScriptableObject �����͸� ����
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    // �񵿱������� ������ �������� �޼���
    public async Task<int> GetScoreByPlayerIDAsync(string playerID, string leaderboardID)
    {
        return await leaderboardsManager.GetScoreByPlayerId(playerID, leaderboardID);
    }

    // �񵿱� �۾��� �����ϰ� ����� ó���ϴ� �޼���
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
        List<string> stageLeaderboardIDList = stageDataManager.returnLeaderboardID(); // �������� �������� ID ����Ʈ

        userData.playerID = AuthenticationService.Instance.PlayerId; // �������忡 ���Ǵ� ���� ������ ID �޾ƿ���

        for (int i = 0; i < stageLeaderboardIDList.Count; i++)
        {
            LoadPlayerScore(i, userData.playerID, stageLeaderboardIDList[i]);
        }

#if UNITY_EDITOR
        // ������ ��忡���� ���� �����͸� ���
#endif
    }
}

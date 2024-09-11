using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardsManager : MonoBehaviour
{
    /****************************************************************************
                                protected Fields
    ****************************************************************************/
    string LeaderboardId = "DodgeLeaderboardBullet"; // �ʱ� �������� ID
    string VersionId { get; set; }
    int Offset { get; set; }
    int Limit { get; set; }
    int RangeLimit { get; set; }
    List<string> FriendIds { get; set; }

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    async void Awake()
    {
        // ����Ƽ ���� �ʱ�ȭ
        await UnityServices.InitializeAsync();
        // �͸� �α��� �õ�
        await SignInAnonymously();
    }

    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    /// <summary> �͸� �α��� ó�� </summary>
    async Task SignInAnonymously()
    {
        // �α��� ���� �� �÷��̾� ID ���
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        // �α��� ���� �� ���� �޽��� ���
        AuthenticationService.Instance.SignInFailed += s =>
        {
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> �������忡 ���� �߰� </summary>
    public async void AddScore(int score)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    /// <summary> ���������� ��� ���� ��ȸ </summary>
    public async void GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> ���������� ����¡�� ���� ��ȸ </summary>
    public async void GetPaginatedScores()
    {
        Offset = 10; // ��ȸ ���� ��ġ
        Limit = 10; // ��ȸ�� ���� ����
        // Ư�� ������ �������� ������
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> player Id�� ���� �޾ƿ���  </summary>
    public async Task<int> GetScoreByPlayerId(string playerId)
    {
        try
        {
            // �÷��̾� ID�� ���� ��ȸ
            var scoresResponse = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, new List<string> { playerId });

            if (scoresResponse.Results.Count > 0)
            {
                var scoreEntry = scoresResponse.Results[0]; // ù ��° ��� ���
                int playerScore = (int)scoreEntry.Score; // ������ ����
                return playerScore; // ������ ��ȯ
            }
            else
            {
                Debug.Log("�ش� �÷��̾� ID�� ���� ������ ã�� �� �����ϴ�.");
                return 0; // ������ ���� ���
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"�÷��̾� ID {playerId}�� ���� ���� ��ȸ ����: {ex.Message}");
            return 0; // ���� �߻� ��
        }
    }

    /// <summary> ���� �÷��̾��� ���� ��ȸ </summary>
    public async void GetPlayerScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    /// <summary> Ư�� ���� ���� �÷��̾� ���� ��ȸ </summary>
    public async void GetPlayerRange()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetPlayerRangeAsync(LeaderboardId, new GetPlayerRangeOptions { RangeLimit = RangeLimit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> Ư�� �÷��̾���� ���� ��ȸ </summary>
    public async void GetScoresByPlayerIds()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, FriendIds);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    // If the Leaderboard has been reset and the existing scores were archived,
    // this call will return the list of archived versions available to read from,
    // in reverse chronological order (so e.g. the first entry is the archived version
    // containing the most recent scores)
    /// <summary> ���������� ���� ���� ����Ʈ�� ��ȸ </summary>
    public async void GetVersions()
    {
        var versionResponse =
            await LeaderboardsService.Instance.GetVersionsAsync(LeaderboardId);

        // As an example, get the ID of the most recently archived Leaderboard version
        VersionId = versionResponse.Results[0].Id;
        Debug.Log(JsonConvert.SerializeObject(versionResponse));
    }

    /// <summary> Ư�� �������� ������ ���� ��ȸ </summary>
    public async void GetVersionScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> Ư�� �������忡�� ����¡�� ���� ��ȸ </summary>
    public async void GetPaginatedVersionScores()
    {
        Offset = 10;
        Limit = 10;
        var scoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId, new GetVersionScoresOptions { Offset = Offset, Limit = Limit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> Ư�� �������� �������� ���� �÷��̾� ���� ��ȸ </summary>
    public async void GetPlayerVersionScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetVersionPlayerScoreAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    //----------------------------------------------------------------------------------------------
    public LeaderboardsUIManager leaderboardsUIManager;

    /// <summary> �������� ID ���� </summary>
    public void SetLeaderboardID(string leaderboardId)
    {
        LeaderboardId = leaderboardId;
    }

    /// <summary> �̸��� ������ �޾� �������忡 �߰� </summary>
    public async void AddScore(string name, int score)
    {
        var metadata = new Dictionary<string, object>
        {
            { "PlayerName", name }
        };

        var options = new AddPlayerScoreOptions
        {
            Metadata = metadata // ��Ÿ�����͸� ���� ����
        };

        try
        {
            var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score, options);
            Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to add score: {ex.Message}");
        }
    }

    /// <summary> ���� limit ���� ���� ��ȸ </summary>
    public async void GetTopScores(int limit)
    {
        Limit = limit;
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = Limit, IncludeMetadata = true });
        leaderboardsUIManager.DisplayScores(scoresResponse);
    }

    /*
    void DisplayScores(LeaderboardScoresPage scoresResponse)
    {
        foreach (var scoreEntry in scoresResponse.Results)
        {
            string playerName = scoreEntry.PlayerName; // �⺻���� scoreEntry.PlayerName���� ����
            if (!string.IsNullOrEmpty(scoreEntry.Metadata))
            {
                try
                {
                    // Metadata�� Dictionary�� ������ȭ
                    var metadata = JsonConvert.DeserializeObject<Dictionary<string, string>>(scoreEntry.Metadata);
                    if (metadata != null && metadata.ContainsKey("PlayerName"))
                    {
                        playerName = metadata["PlayerName"];
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Failed to parse metadata: {ex.Message}");
                }
            }
            else
            {
                Debug.LogWarning("Metadata is empty.");
            }

            Debug.Log($"PlayerId: {scoreEntry.PlayerId}, PlayerName: {playerName}, Rank: {scoreEntry.Rank}, Score: {scoreEntry.Score}, Tier: {scoreEntry.Tier}");
        }
    }
    */
}

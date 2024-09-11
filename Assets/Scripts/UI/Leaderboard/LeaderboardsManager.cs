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
    string LeaderboardId = "DodgeLeaderboardBullet"; // 초기 리더보드 ID
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
        // 유니티 서비스 초기화
        await UnityServices.InitializeAsync();
        // 익명 로그인 시도
        await SignInAnonymously();
    }

    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    /// <summary> 익명 로그인 처리 </summary>
    async Task SignInAnonymously()
    {
        // 로그인 성공 시 플레이어 ID 출력
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        // 로그인 실패 시 에러 메시지 출력
        AuthenticationService.Instance.SignInFailed += s =>
        {
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> 리더보드에 점수 추가 </summary>
    public async void AddScore(int score)
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    /// <summary> 리더보드의 모든 점수 조회 </summary>
    public async void GetScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> 리더보드의 페이징된 점수 조회 </summary>
    public async void GetPaginatedScores()
    {
        Offset = 10; // 조회 시작 위치
        Limit = 10; // 조회할 점수 개수
        // 특정 범위의 점수들을 가져옴
        var scoresResponse =
            await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Offset = Offset, Limit = Limit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> player Id의 점수 받아오기  </summary>
    public async Task<int> GetScoreByPlayerId(string playerId)
    {
        try
        {
            // 플레이어 ID로 점수 조회
            var scoresResponse = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync(LeaderboardId, new List<string> { playerId });

            if (scoresResponse.Results.Count > 0)
            {
                var scoreEntry = scoresResponse.Results[0]; // 첫 번째 결과 사용
                int playerScore = (int)scoreEntry.Score; // 점수를 추출
                return playerScore; // 점수를 반환
            }
            else
            {
                Debug.Log("해당 플레이어 ID에 대한 점수를 찾을 수 없습니다.");
                return 0; // 점수가 없을 경우
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"플레이어 ID {playerId}에 대한 점수 조회 실패: {ex.Message}");
            return 0; // 오류 발생 시
        }
    }

    /// <summary> 현재 플레이어의 점수 조회 </summary>
    public async void GetPlayerScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    /// <summary> 특정 범위 내의 플레이어 점수 조회 </summary>
    public async void GetPlayerRange()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetPlayerRangeAsync(LeaderboardId, new GetPlayerRangeOptions { RangeLimit = RangeLimit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> 특정 플레이어들의 점수 조회 </summary>
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
    /// <summary> 리더보드의 이전 버전 리스트를 조회 </summary>
    public async void GetVersions()
    {
        var versionResponse =
            await LeaderboardsService.Instance.GetVersionsAsync(LeaderboardId);

        // As an example, get the ID of the most recently archived Leaderboard version
        VersionId = versionResponse.Results[0].Id;
        Debug.Log(JsonConvert.SerializeObject(versionResponse));
    }

    /// <summary> 특정 리더보드 버전의 점수 조회 </summary>
    public async void GetVersionScores()
    {
        var scoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> 특정 리더보드에서 페이징된 점수 조회 </summary>
    public async void GetPaginatedVersionScores()
    {
        Offset = 10;
        Limit = 10;
        var scoresResponse =
            await LeaderboardsService.Instance.GetVersionScoresAsync(LeaderboardId, VersionId, new GetVersionScoresOptions { Offset = Offset, Limit = Limit });
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));
    }

    /// <summary> 특정 리더보드 버전에서 현재 플레이어 점수 조회 </summary>
    public async void GetPlayerVersionScore()
    {
        var scoreResponse =
            await LeaderboardsService.Instance.GetVersionPlayerScoreAsync(LeaderboardId, VersionId);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    //----------------------------------------------------------------------------------------------
    public LeaderboardsUIManager leaderboardsUIManager;

    /// <summary> 리더보드 ID 설정 </summary>
    public void SetLeaderboardID(string leaderboardId)
    {
        LeaderboardId = leaderboardId;
    }

    /// <summary> 이름과 점수를 받아 리더보드에 추가 </summary>
    public async void AddScore(string name, int score)
    {
        var metadata = new Dictionary<string, object>
        {
            { "PlayerName", name }
        };

        var options = new AddPlayerScoreOptions
        {
            Metadata = metadata // 메타데이터를 직접 설정
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

    /// <summary> 상위 limit 명의 점수 조회 </summary>
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
            string playerName = scoreEntry.PlayerName; // 기본값을 scoreEntry.PlayerName으로 설정
            if (!string.IsNullOrEmpty(scoreEntry.Metadata))
            {
                try
                {
                    // Metadata를 Dictionary로 역직렬화
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

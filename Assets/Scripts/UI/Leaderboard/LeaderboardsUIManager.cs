using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System.Collections.Generic;
using System;
using Unity.Services.Leaderboards.Models;

public class LeaderboardsUIManager : MonoBehaviour
{
    public InputField playerNameField; // �÷��̾� �̸� �Է� �ʵ�
    //public InputField scoreInputField;
    public Button addScoreButton;

    public LeaderboardsManager leaderboardsManager;

    // UI ��Ҹ� ������ �ʵ� �߰�
    public GameObject leaderboardEntryPrefab; // �������� ��Ʈ�� ������
    public Transform leaderboardContent; // �������� �׸��� ��ġ�� �θ� ������Ʈ

    void Start()
    {
        //addScoreButton.onClick.AddListener(OnAddScoreButtonClicked);
    }

    /*
    void OnAddScoreButtonClicked()
    {
        int score;
        if (int.TryParse(scoreInputField.text, out score))
        {
            leaderboardsManager.AddScore(score);
        }
        else
        {
            Debug.LogError("Invalid score input.");
        }
    }
    */

    /*
    public void AddScore()
    {
        string playerName;
        if (string.IsNullOrEmpty(playerNameField.text))
        {
            playerName = "Anonymous";
        }
        else
        {
            playerName = playerNameField.text;
        }

        int score = stageManager.GetCurrentStageData().
        if (int.TryParse(scoreInputField.text, out score))
        {
            leaderboardsManager.AddScore(playerName, score);
        }
        else
        {
            Debug.LogError("Invalid score input.");
        }
    }
    */

    // limit ������ŭ�� �������� ������ ������
    public void GetTopScores(int limit)
    {
        leaderboardsManager.GetTopScores(limit);
    }

    // �������� ���� �ʱ�ȭ
    public void ClearLeaderboard()
    {
        // ���� �������� ����
        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }
    }

    // �������� ���� ǥ��
    public void DisplayScores(LeaderboardScoresPage scoresResponse)
    {
        foreach (var scoreEntry in scoresResponse.Results)
        {
            string playerName = scoreEntry.PlayerName;
            if (!string.IsNullOrEmpty(scoreEntry.Metadata))
            {
                try
                {
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

            // ���ο� �������� ��Ʈ�� ���� �� ����
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            //entry.transform.Find("Text PlayerId").GetComponent<Text>().text = scoreEntry.PlayerId;
            entry.transform.Find("Text Player Name").GetComponent<Text>().text = playerName;
            entry.transform.Find("Text Rank").GetComponent<Text>().text = scoreEntry.Rank.ToString();
            entry.transform.Find("Text Score").GetComponent<Text>().text = scoreEntry.Score.ToString();
            entry.transform.Find("Text Tier").GetComponent<Text>().text = scoreEntry.Tier.ToString();
        }
    }
}

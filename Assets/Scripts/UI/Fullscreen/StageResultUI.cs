using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour, IUpdateUI
{
    public Image characterImage; // ĳ���� �̹���
    public Text stageNameText; // �������� �̸�
    public Text stageScore; // �������� ����
    [SerializeField] private GameObject[] StarUnits; // �� ĭ�� ��Ÿ���� GameObject �迭

    public InputField playerNameField; // �÷��̾� �̸� �Է� �ʵ�
    public LeaderboardsManager leaderboardsManager;

    // �������� ���â UI ������Ʈ
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
            stageScore.text = "Score: " + ScoreManager.Instance.GetCurrentScore();
            OnStarChanged(userData.stageInfos[stageData.stageID].score);
        }
    }

    // ������ ���� �� UI ������Ʈ
    public void OnStarChanged(float score)
    {
        // �� Ȱ��ȭ
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

    // �������忡 �÷��̾� ���
    public void RegisterPlayer()
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

        leaderboardsManager.AddScore(playerName, Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore()));
    }
}

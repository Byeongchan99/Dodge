using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StageResultUI : MonoBehaviour, IUpdateUI
{
    public Image characterImage; // 캐릭터 이미지
    public Text stageNameText; // 스테이지 이름
    public Text stageScore; // 스테이지 점수
    [SerializeField] private GameObject[] StarUnits; // 별 칸을 나타내는 GameObject 배열

    public InputField playerNameField; // 플레이어 이름 입력 필드
    public LeaderboardsManager leaderboardsManager;

    // 스테이지 결과창 UI 업데이트
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

    // 점수에 따른 별 UI 업데이트
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

    // 리더보드에 플레이어 등록
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

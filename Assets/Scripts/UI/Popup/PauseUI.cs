using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IUpdateUI
{
    void UpdateUIInfo(params object[] data);
}

public class PauseUI : MonoBehaviour, IUpdateUI
{
    public Text stageNameText;
    public Text currentScoreText;
    float _lastTimeScale;

    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageManager stageManager;

    // 일시정지창 열기
    public void OpenPauseUI()
    {
        HUDManager.DisableTimer();

        _lastTimeScale = Time.timeScale; // 슬로우 효과 적용 중일 때를 대비하여 저장
        Time.timeScale = 0f;
        // 타이머 일시 정지
        ScoreManager.Instance.PauseTimer();

        GameManager.Instance.isPaused = true;

        if (stageManager != null)
        {
            UpdateUIInfo(stageManager.GetCurrentStageData());
        }
        else
        {
            Debug.Log("stageManager 할당 필요");
        }
    }

    // 일시정지창 닫기
    public void ClosePauseUI()
    {
        HUDManager.EnableTimer();

        Time.timeScale = _lastTimeScale;
        // 타이머 재개
        ScoreManager.Instance.ResumeTimer();
        
        GameManager.Instance.isPaused = false;
    }

    // 일시정지창의 스테이지 정보 업데이트
    public void UpdateUIInfo(params object[] datas)
    {
        StageData stageData = null; // 초기화

        foreach (var data in datas)
        {
            if (data is StageData)
                stageData = data as StageData;
        }

        if (stageData != null)
        {
            stageNameText.text = stageData.stageName;
            currentScoreText.text = "Current Score: " + Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore());
        }
    }

    public void OnOptionClick()
    {

    }

    // 계속하기 버튼 클릭
    public void OnResumeClick()
    {
        ClosePauseUI();
    }

    // 다시하기 버튼 클릭
    public void OnRestartClick()
    {
        ClosePauseUI();
        stageManager.RestartStage();
    }

    // 나가기 버튼 클릭
    public void OnExitClick()
    {
        ClosePauseUI();
        stageManager.ExitStage();
    }
}

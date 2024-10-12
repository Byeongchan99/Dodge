using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour, IHealthObserver
{
    /****************************************************************************
                                protected Fields
    ****************************************************************************/
    // 스포너 및 이벤트
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private FullscreenUIManager fullscreenUIManager;
    [SerializeField] private PopupUIManager popupUIManager;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private GameObject HUDContainer;
    [SerializeField] private StageResultUI stageResultUI;
    [SerializeField] private NotificationManager notificationManager;
    // 스탯 데이터
    [SerializeField] private StatDataManager statDataManager;
    // 스테이지 데이터
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // 현재 스테이지 데이터
    // 유저 데이터
    [SerializeField] private UserDataManager userDataManager;
    // 타일맵
    [SerializeField] private GameObject[] tilemaps;

    /****************************************************************************
                                    public Fields
    ****************************************************************************/
    public Eraser eraser; // 게임 종료 후 초기화

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void Start()
    {
        PlayerStat.Instance.RegisterObserver(this);
    }

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    // 현재 스테이지 데이터 반환
    public StageData GetCurrentStageData()
    {
        return currentStageData;
    }

    // 현재 스테이지 데이터 설정
    public void SetStageData(int stageID)
    {
        currentStageData = stageDataList[stageID];
    }

    public List<string> returnLeaderboardID()
    {
        List<string> stageLeaderboardIDList = new List<string>(); // 스테이지 리더보드 ID 리스트

        foreach (var stageData in stageDataList)
        {
            stageLeaderboardIDList.Add(stageData.leaderboardID);
        }

        return stageLeaderboardIDList;
    }

    // 플레이어 체력이 변경될 때 호출
    public void OnHealthChanged(float health)
    {
        if (health <= 0)
        {      
            ExitStage();
        }
    }

    // 퍼사드 메소드: 모든 초기화 작업을 시작
    // 스테이지 시작
    public void StartStage()
    {
        // 모든 오브젝트 초기화
        eraser.EraseAll();
        // 타일맵 활성화
        if (currentStageData != null)
        {
            tilemaps[currentStageData.stageID].SetActive(true);
            tilemaps[5].SetActive(true);
        }
        else
        {
            Debug.LogError("Stage data or tilemap is not set.");
            return;
        }

        // 스테이지 시작
        Time.timeScale = 1f;
        GameManager.Instance.isPlayingStage = true;
        PlayerStat.Instance.SetCharacter();
        PlayerStat.Instance.player.SetActive(true);
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        turretUpgradeHandler.StartRandomUpgrades(10);
        // UI
        HUDContainer.SetActive(true);
        //HUDManager.EnableTimer();
        //HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);
        // 음악 재생
        AudioManager.instance.PlayBGM(currentStageData.bgm);

        // 타이머 시작
        ScoreManager.Instance.StartTimer();
    }

    // 스테이지 재시작
    public void RestartStage()
    {
        // 스테이지 종료
        eraser.EraseAll();
        statDataManager.SetOriginalStatData(currentStageData.statDataName); // 스탯 초기화
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        AudioManager.instance.StopBGM();

        // UI
        popupUIManager.ClosePausePopup(); // 일시정지 창 닫기
        HUDContainer.SetActive(false);
        notificationManager.ClearNotifications();

        // 스테이지 재시작
        StartStage();
    }

    // 스테이지 종료
    public void ExitStage()
    {
        // 유저 데이터 갱신
        if (userDataManager.userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userDataManager.userData.stageInfos.Length)
        {
            if (ScoreManager.Instance.GetCurrentScore() > 100f)
                userDataManager.userData.stageInfos[currentStageData.stageID].isCleared = true;

            // 점수 갱신
            if (userDataManager.userData.stageInfos[currentStageData.stageID].score < Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore()))
                userDataManager.userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(ScoreManager.Instance.GetCurrentScore());

            userDataManager.SaveUserData();
        }

        // 모든 오브젝트 초기화
        eraser.EraseAll();
        // 맵 비활성화
        if (currentStageData != null)
        {
            tilemaps[currentStageData.stageID].SetActive(false);
            tilemaps[5].SetActive(true);
        }
        else
        {
            Debug.LogError("Stage data or tilemap is not set.");
            return;
        }
        // 스테이지 종료
        GameManager.Instance.isPlayingStage = false;
        PlayerStat.Instance.DisablePlayer();
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD 비활성화
        notificationManager.ClearNotifications();
        HUDContainer.SetActive(false);
        //HUDManager.DisableTimer();
        //HUDManager.DisableHealthBar();
        // 스테이지 결과창 활성화
        AudioManager.instance.StopBGM();
        AudioManager.instance.PlayMainBGM();
        stageResultUI.UpdateUIInfo(currentStageData, userDataManager.userData);
        fullscreenUIManager.OnPushFullscreenUI("Stage Result");
        fullscreenUIContainer.SetActive(true);
    }

    /*
    public void ExitStage()
    {
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        HUDManager.PauseTimer();
    }
    */
}

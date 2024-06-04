using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageManager : MonoBehaviour, IHealthObserver
{
    // 스포너 및 이벤트
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private FullscreenUIManager fullscreenUIManager;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageResultUI stageResultUI;
    // 스테이지 데이터
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // 현재 스테이지 데이터

    public UserData userData; // 유저 데이터(임시)

    private float timer = 0f;
    private bool isPaused = false;
    private Coroutine timerCoroutine;

    void Start()
    {
        PlayerStat.Instance.RegisterObserver(this);
    }

    public void SetStageData(int stageID)
    {
        currentStageData = stageDataList[stageID];
    }

    // 퍼사드 메소드: 모든 초기화 작업을 시작
    public void StartStage()
    {
        // 맵 프리팹 인스턴스화
        if (currentStageData != null && currentStageData.mapPrefab != null)
        {
            Instantiate(currentStageData.mapPrefab);
        }
        else
        {
            Debug.LogError("Stage data or map prefab is not set.");
            return;
        }

        // 스테이지 시작
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        turretUpgradeHandler.StartRandomUpgrades(10);
        // UI
        HUDManager.EnableTimer();
        HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);

        // 타이머 시작
        StartTimer();
    }

    public void OnHealthChanged(float health)
    {
        if (health <= 0)
        {
            ExitStage();
        }
    }

    public void ExitStage()
    {
        // 유저 데이터 갱신
        if (userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userData.stageInfos.Length)
        {
            if (timer > 90f) 
                userData.stageInfos[currentStageData.stageID].isCleared = true;

            // 점수 갱신
            if (userData.stageInfos[currentStageData.stageID].score < Mathf.FloorToInt(timer)) 
                userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(timer);

            SaveUserData();
        }

        // 스테이지 종료
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        turretUpgradeHandler.StopRandomUpgrades();
        // HUD 비활성화
        HUDManager.DisableTimer();
        HUDManager.DisableHealthBar();
        // 스테이지 결과창 활성화
        stageResultUI.UpdateStageResult(currentStageData, userData);
        fullscreenUIManager.OnPushFullscreenUI("Stage Result");
        fullscreenUIContainer.SetActive(true);
    }

    private void SaveUserData()
    {
#if UNITY_EDITOR
        // 에디터 모드에서 ScriptableObject 데이터를 저장
        UnityEditor.EditorUtility.SetDirty(userData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private void StartTimer()
    {
        timer = 0f;
        isPaused = false;
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        while (true)
        {
            if (!isPaused)
            {
                timer += Time.unscaledDeltaTime;
                HUDManager.UpdateTimerUI(timer); // HUDManager를 통해 UI 업데이트
            }
            yield return null;
        }
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
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

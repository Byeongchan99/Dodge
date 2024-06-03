using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    // 스포너 및 이벤트
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    // UI
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    // 스테이지 데이터
    [SerializeField] private List<StageData> stageDataList;
    [SerializeField] private StageData currentStageData; // 현재 스테이지 데이터
    private GameObject currentMap;

    public UserData userData; // 유저 데이터(임시)

    private float timer = 0f;
    private bool isPaused = false;
    private Coroutine timerCoroutine;

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
            currentMap = Instantiate(currentStageData.mapPrefab);
        }
        else
        {
            Debug.LogError("Stage data or map prefab is not set.");
            return;
        }

        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        HUDManager.EnableTimer();
        HUDManager.EnableHealthBar();
        fullscreenUIContainer.SetActive(false);
        turretUpgradeHandler.StartRandomUpgrades(10); // 파라미터가 있는 경우도 고려

        // 타이머 시작
        StartTimer();
    }

    public void ClearStage()
    {
        // 유저 데이터 갱신
        if (userData != null && currentStageData.stageID >= 0 && currentStageData.stageID < userData.stageInfos.Length)
        {
            if (timer > 90f) 
                userData.stageInfos[currentStageData.stageID].isCleared = true;
            userData.stageInfos[currentStageData.stageID].score = Mathf.FloorToInt(timer);
            SaveUserData();
        }

        // 스테이지 선택창으로 이동
        turretSpawner.StopSpawn();
        itemSpawner.StopSpawn();
        HUDManager.DisableTimer();
        HUDManager.DisableHealthBar();
        fullscreenUIContainer.SetActive(true);
        turretUpgradeHandler.StopRandomUpgrades();
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

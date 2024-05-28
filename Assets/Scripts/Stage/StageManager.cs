using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    [SerializeField] private GameObject fullscreenUIContainer;
    [SerializeField] private HUDManager HUDManager;
    [SerializeField] private StageData currentStageData;

    [SerializeField] private List<StageData> stageDataList;

    private GameObject currentMap;

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
        HUDManager.ActiveTimer();
        HUDManager.ActiveHealthBar();
        fullscreenUIContainer.SetActive(false);
        turretUpgradeHandler.StartRandomUpgrades(10); // 파라미터가 있는 경우도 고려
    }

    public void SetStageData(int stageID)
    {
        currentStageData = stageDataList[stageID];
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

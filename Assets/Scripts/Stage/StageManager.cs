using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    [SerializeField] private HUDManager HUDManager;

    // 퍼사드 메소드: 모든 초기화 작업을 시작
    public void StartStage()
    {
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        HUDManager.ActiveTimer();
        turretUpgradeHandler.StartRandomUpgrades(10); // 파라미터가 있는 경우도 고려
    }
}

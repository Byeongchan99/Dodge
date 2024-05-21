using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;
    [SerializeField] private HUDManager HUDManager;

    // �ۻ�� �޼ҵ�: ��� �ʱ�ȭ �۾��� ����
    public void StartStage()
    {
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        HUDManager.ActiveTimer();
        turretUpgradeHandler.StartRandomUpgrades(10); // �Ķ���Ͱ� �ִ� ��쵵 ���
    }
}

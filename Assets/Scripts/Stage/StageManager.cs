using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TurretSpawner turretSpawner;
    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TurretUpgradeHandler turretUpgradeHandler;

    // �ۻ�� �޼ҵ�: ��� �ʱ�ȭ �۾��� ����
    public void StartStage()
    {
        turretSpawner.StartSpawn();
        itemSpawner.StartSpawn();
        gameManager.StartTimer();
        turretUpgradeHandler.StartRandomUpgrades(10); // �Ķ���Ͱ� �ִ� ��쵵 ���
    }
}

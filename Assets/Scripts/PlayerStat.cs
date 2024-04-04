using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    // �ӽ�
    /// <summary> �Ѿ� �ͷ� �п� �Ѿ� Ȱ��ȭ �̺�Ʈ </summary>
    public void BulletTurretSplitEvent()
    {
        Debug.Log("�п� �Ѿ� ���� �̺�Ʈ ����");
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.ProjectileSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> �Ѿ� �ͷ� �п� �Ѿ� ��Ȱ��ȭ �̺�Ʈ </summary>
    public void BulletTurretRemoveSplitEvent()
    {
        Debug.Log("Remove Split");
        TurretUpgradeInfo bulletTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.RemoveSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }

    /// <summary> �Ѿ� �ͷ� ���� ���� �̺�Ʈ </summary>
    public void BulletTurretCountIncreaseEvent()
    {
        Debug.Log("Count Increase");
        TurretUpgradeInfo bulletTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountIncrease,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountIncrease);
    }

    /// <summary> �Ѿ� �ͷ� �ӵ� ���� �̺�Ʈ </summary>
    public void BulletTurretSpeedIncreaseEvent()
    {
        Debug.Log("Speed Increase");
        TurretUpgradeInfo bulletTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedIncrease,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedIncrease);
    }

    /// <summary> �Ѿ� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
    public void InitBulletTurretEvent()
    {
        Debug.Log("Init Bullet Turret");
        TurretUpgradeInfo bulletTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretInit);
    }
}

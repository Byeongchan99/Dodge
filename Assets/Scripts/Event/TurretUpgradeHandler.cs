using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeHandler : MonoBehaviour
{
    /// <summary> �Ѿ� �ͷ� �п� �Ѿ� Ȱ��ȭ �̺�Ʈ </summary>
    public void BulletTurretSplitEvent()
    {
        Debug.Log("�п� �Ѿ� ���� �̺�Ʈ ����");
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
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
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
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
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountIncrease);
    }

    /// <summary> �Ѿ� �ͷ� ���� ���� �̺�Ʈ </summary>
    public void BulletTurretCountDecreaseEvent()
    {
        Debug.Log("Count Decrease");
        TurretUpgradeInfo bulletTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountDecrease);
    }

    /// <summary> �Ѿ� �ͷ� �ӵ� ���� �̺�Ʈ </summary>
    public void BulletTurretSpeedIncreaseEvent()
    {
        Debug.Log("Speed Increase");
        TurretUpgradeInfo bulletTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedIncrease);
    }

    /// <summary> �Ѿ� �ͷ� �ӵ� ���� �̺�Ʈ </summary>
    public void BulletTurretSpeedDecreaseEvent()
    {
        Debug.Log("Speed Decrease");
        TurretUpgradeInfo bulletTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedDecrease);
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

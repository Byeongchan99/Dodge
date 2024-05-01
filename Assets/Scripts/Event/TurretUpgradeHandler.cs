using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeHandler : MonoBehaviour
{
    /****************************************************************************
                           Bullet Turret Enhancement Events
    ****************************************************************************/
    /// <summary> �Ѿ� �п� Ȱ��ȭ �̺�Ʈ </summary>
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

    /// <summary> �Ѿ� ���� ���� �̺�Ʈ </summary>
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

    /// <summary> �Ѿ� ���� ���� �̺�Ʈ </summary>
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

    /// <summary> �Ѿ� �ӵ� ���� �̺�Ʈ </summary>
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

    /// <summary> �Ѿ� �ӵ� ���� �̺�Ʈ </summary>
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

    /// <summary> �Ѿ� ũ�� ���� �̺�Ʈ </summary>
    public void BulletTurretSizeIncreaseEvent()
    {
        Debug.Log("Size Increase");
        TurretUpgradeInfo bulletTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeIncrease);
    }

    /// <summary> �Ѿ� ũ�� ���� �̺�Ʈ </summary>
    public void BulletTurretSizeDecreaseEvent()
    {
        Debug.Log("Size Decrease");
        TurretUpgradeInfo bulletTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 2.0f / 3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeDecrease);
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

    /****************************************************************************
                          Laser Turret Enhancement Events
    ****************************************************************************/
    /// <summary> ������ ���� �ð� ���� �̺�Ʈ </summary>
    public void LaserTurretLifeTimeIncreaseEvent()
    {
        Debug.Log("LifeTime Increase");
        TurretUpgradeInfo laserTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeIncrease);
    }

    /// <summary> ������ ���� �ð� ���� �̺�Ʈ </summary>
    public void LaserTurretLifeTimeDecreaseEvent()
    {
        Debug.Log("LifeTime Decrease");
        TurretUpgradeInfo laserTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeDecrease);
    }

    /// <summary> ������ ���� ���� �̺�Ʈ </summary>
    public void LaserTurretCountIncreaseEvent()
    {
        Debug.Log("Count Increase");
        TurretUpgradeInfo laserTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountIncrease);
    }

    /// <summary> ������ ���� ���� �̺�Ʈ </summary>
    public void LaserTurretCountDecreaseEvent()
    {
        Debug.Log("Count Decrease");
        TurretUpgradeInfo laserTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountDecrease);
    }

    /// <summary> ������ ũ�� ���� �̺�Ʈ </summary>
    public void LaserTurretSizeIncreaseEvent()
    {
        Debug.Log("Size Increase");
        TurretUpgradeInfo laserTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeIncrease);
    }

    /// <summary> ������ ũ�� ���� �̺�Ʈ </summary>
    public void LaserTurretSizeDecreaseEvent()
    {
        Debug.Log("Size Decrease");
        TurretUpgradeInfo laserTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 2.0f / 3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeDecrease);
    }

    /// <summary> ������ �ʱ�ȭ �̺�Ʈ </summary>
    public void InitLaserTurretEvent()
    {
        Debug.Log("Init Laser Turret");
        TurretUpgradeInfo laserTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretInit);
    }

    /****************************************************************************
                          Rocket Turret Enhancement Events
    ****************************************************************************/
    /// <summary> ���� ���� �ð� ���� �̺�Ʈ </summary>
    public void RocketTurretLifeTimeIncreaseEvent()
    {
        Debug.Log("LifeTime Increase");
        TurretUpgradeInfo rocketTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 1.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeIncrease);
    }

    /// <summary> ���� ���� �ð� ���� �̺�Ʈ </summary>
    public void RocketTurretLifeTimeDecreaseEvent()
    {
        Debug.Log("LifeTime Decrease");
        TurretUpgradeInfo rocketTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -1.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeDecrease);
    }

    /// <summary> ���� ���� ���� �̺�Ʈ </summary>
    public void RocketTurretCountIncreaseEvent()
    {
        Debug.Log("Count Increase");
        TurretUpgradeInfo rocketTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountIncrease);
    }

    /// <summary> ���� ���� ���� �̺�Ʈ </summary>
    public void RocketTurretCountDecreaseEvent()
    {
        Debug.Log("Count Decrease");
        TurretUpgradeInfo rocketTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountDecrease);
    }

    /// <summary> ���� �ӵ� ���� �̺�Ʈ </summary>
    public void RocketTurretSpeedIncreaseEvent()
    {
        Debug.Log("Speed Increase");
        TurretUpgradeInfo rocketTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 1.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedIncrease);
    }

    /// <summary> ���� �ӵ� ���� �̺�Ʈ </summary>
    public void RocketTurretSpeedDecreaseEvent()
    {
        Debug.Log("Speed Decrease");
        TurretUpgradeInfo rocketTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -1.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedDecrease);
    }

    /// <summary> ���� ũ�� ���� �̺�Ʈ </summary>
    public void RocketTurretSizeIncreaseEvent()
    {
        Debug.Log("Size Increase");
        TurretUpgradeInfo rocketTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeIncrease);
    }

    /// <summary> ���� ũ�� ���� �̺�Ʈ </summary>
    public void RocketTurretSizeDecreaseEvent()
    {
        Debug.Log("Size Decrease");
        TurretUpgradeInfo rocketTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 2.0f / 3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeDecrease);
    }

    /// <summary> ���� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
    public void InitRocketTurretEvent()
    {
        Debug.Log("Init Rocket Turret");
        TurretUpgradeInfo rocketTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretInit);
    }

    /****************************************************************************
                          Mortar Turret Enhancement Events
    ****************************************************************************/
    /// <summary> �ڰ���ź �п� Ȱ��ȭ �̺�Ʈ </summary>
    public void MortarTurretSplitEvent()
    {
        Debug.Log("�п� ���� ���� �̺�Ʈ ����");
        TurretUpgradeInfo mortarTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSplit);
    }

    /// <summary> �ڰ���ź �п� ��Ȱ��ȭ �̺�Ʈ </summary>
    public void MortarTurretRemoveSplitEvent()
    {
        Debug.Log("Remove Split");
        TurretUpgradeInfo mortarTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretRemoveSplit);
    }

    /// <summary> �ڰ���ź ���� ���� �̺�Ʈ </summary>
    public void MortarTurretCountIncreaseEvent()
    {
        Debug.Log("Count Increase");
        TurretUpgradeInfo mortarTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountIncrease);
    }

    /// <summary> �ڰ���ź ���� ���� �̺�Ʈ </summary>
    public void MortarTurretCountDecreaseEvent()
    {
        Debug.Log("Count Decrease");
        TurretUpgradeInfo mortarTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountDecrease);
    }

    /// <summary> �ڰ���ź �ӵ� ���� �̺�Ʈ </summary>
    public void MortarTurretSpeedIncreaseEvent()
    {
        Debug.Log("Speed Increase");
        TurretUpgradeInfo mortarTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedIncrease);
    }

    /// <summary> �ڰ���ź �ӵ� ���� �̺�Ʈ </summary>
    public void MortarTurretSpeedDecreaseEvent()
    {
        Debug.Log("Speed Decrease");
        TurretUpgradeInfo mortarTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedDecrease);
    }

    /// <summary> �ڰ��� ����Ʈ ���� ���� �̺�Ʈ </summary>
    public void MortarTurretSizeIncreaseEvent()
    {
        Debug.Log("Size Increase");
        TurretUpgradeInfo mortarTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeIncrease);
    }

    /// <summary> �ڰ��� ����Ʈ ���� ���� �̺�Ʈ </summary>
    public void MortarTurretSizeDecreaseEvent()
    {
        Debug.Log("Size Decrease");
        TurretUpgradeInfo mortarTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 2.0f / 3.0f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeDecrease);
    }

    /// <summary> �ڰ��� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
    public void InitMortarTurretEvent()
    {
        Debug.Log("Init mortar Turret");
        TurretUpgradeInfo mortarTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretInit);
    }
}

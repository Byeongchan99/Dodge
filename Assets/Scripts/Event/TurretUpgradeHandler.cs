using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeHandler : MonoBehaviour
{
    /****************************************************************************
                           Bullet Turret Enhancement Events
    ****************************************************************************/
    /// <summary> 총알 분열 활성화 이벤트 </summary>
    public void BulletTurretSplitEvent()
    {
        Debug.Log("분열 총알 적용 이벤트 시작");
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> 총알 터렛 분열 총알 비활성화 이벤트 </summary>
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

    /// <summary> 총알 개수 증가 이벤트 </summary>
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

    /// <summary> 총알 개수 감소 이벤트 </summary>
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

    /// <summary> 총알 속도 증가 이벤트 </summary>
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

    /// <summary> 총알 속도 감소 이벤트 </summary>
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

    /// <summary> 총알 크기 증가 이벤트 </summary>
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

    /// <summary> 총알 크기 감소 이벤트 </summary>
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

    /// <summary> 총알 터렛 초기화 이벤트 </summary>
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
    /// <summary> 레이저 지속 시간 증가 이벤트 </summary>
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

    /// <summary> 레이저 지속 시간 감소 이벤트 </summary>
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

    /// <summary> 레이저 개수 증가 이벤트 </summary>
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

    /// <summary> 레이저 개수 감소 이벤트 </summary>
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

    /// <summary> 레이저 크기 증가 이벤트 </summary>
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

    /// <summary> 레이저 크기 감소 이벤트 </summary>
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

    /// <summary> 레이저 초기화 이벤트 </summary>
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
    /// <summary> 로켓 지속 시간 증가 이벤트 </summary>
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

    /// <summary> 로켓 지속 시간 감소 이벤트 </summary>
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

    /// <summary> 로켓 개수 증가 이벤트 </summary>
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

    /// <summary> 로켓 개수 감소 이벤트 </summary>
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

    /// <summary> 로켓 속도 증가 이벤트 </summary>
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

    /// <summary> 로켓 속도 감소 이벤트 </summary>
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

    /// <summary> 로켓 크기 증가 이벤트 </summary>
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

    /// <summary> 로켓 크기 감소 이벤트 </summary>
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

    /// <summary> 로켓 터렛 초기화 이벤트 </summary>
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
    /// <summary> 박격포탄 분열 활성화 이벤트 </summary>
    public void MortarTurretSplitEvent()
    {
        Debug.Log("분열 로켓 적용 이벤트 시작");
        TurretUpgradeInfo mortarTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSplit);
    }

    /// <summary> 박격포탄 분열 비활성화 이벤트 </summary>
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

    /// <summary> 박격포탄 개수 증가 이벤트 </summary>
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

    /// <summary> 박격포탄 개수 감소 이벤트 </summary>
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

    /// <summary> 박격포탄 속도 증가 이벤트 </summary>
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

    /// <summary> 박격포탄 속도 감소 이벤트 </summary>
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

    /// <summary> 박격포 이펙트 범위 증가 이벤트 </summary>
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

    /// <summary> 박격포 이펙트 범위 감소 이벤트 </summary>
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

    /// <summary> 박격포 터렛 초기화 이벤트 </summary>
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

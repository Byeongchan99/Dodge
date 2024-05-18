using UnityEngine;

public interface ITurretUpgradeEvent
{
    void ExecuteEvent();
}

/****************************************************************************
                       Bullet Turret Enhancement Events
****************************************************************************/
/// <summary> 총알 분열 활성화 이벤트 </summary>
public class BulletTurretSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 분열 활성화");
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }
}

/// <summary> 총알 터렛 분열 총알 비활성화 이벤트 </summary>
public class BulletTurretRemoveSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 분열 비활성화");
        TurretUpgradeInfo bulletTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }
}

/// <summary> 총알 개수 증가 이벤트 </summary>
public class BulletTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 개수 증가");
        TurretUpgradeInfo bulletTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountIncrease);
    }
}

/// <summary> 총알 개수 감소 이벤트 </summary>
public class BulletTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 개수 감소");
        TurretUpgradeInfo bulletTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountDecrease);
    }
}

/// <summary> 총알 속도 증가 이벤트 </summary>
public class BulletTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 속도 증가");
        TurretUpgradeInfo bulletTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedIncrease);
    }
}

/// <summary> 총알 속도 감소 이벤트 </summary>
public class BulletTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 속도 감소");
        TurretUpgradeInfo bulletTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedDecrease);
    }
}

/// <summary> 총알 크기 증가 이벤트 </summary>
public class BulletTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 크기 증가");
        TurretUpgradeInfo bulletTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeIncrease);
    }
}

/// <summary> 총알 크기 감소 이벤트 </summary>
public class BulletTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 크기 감소");
        TurretUpgradeInfo bulletTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeDecrease);
    }
}

/// <summary> 총알 터렛 초기화 이벤트 </summary>
public class InitBulletTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("총알 터렛 초기화");
        TurretUpgradeInfo bulletTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretInit);
    }
}

/****************************************************************************
                      Laser Turret Enhancement Events
****************************************************************************/
/// <summary> 레이저 지속 시간 증가 이벤트 </summary>
public class LaserTurretLifeTimeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 지속 시간 증가");
        TurretUpgradeInfo laserTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeIncrease);
    }
}

/// <summary> 레이저 지속 시간 감소 이벤트 </summary>
public class LaserTurretLifeTimeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 지속 시간 감소");
        TurretUpgradeInfo laserTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeDecrease);
    }
}

/// <summary> 레이저 개수 증가 이벤트 </summary>
public class LaserTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 개수 증가");
        TurretUpgradeInfo laserTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountIncrease);
    }
}

/// <summary> 레이저 개수 감소 이벤트 </summary>
public class LaserTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 개수 감소");
        TurretUpgradeInfo laserTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountDecrease);
    }
}

/// <summary> 레이저 크기 증가 이벤트 </summary>
public class LaserTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 크기 증가");
        TurretUpgradeInfo laserTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeIncrease);
    }
}

/// <summary> 레이저 크기 감소 이벤트 </summary>
public class LaserTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 크기 감소");
        TurretUpgradeInfo laserTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeDecrease);
    }
}

/// <summary> 레이저 초기화 이벤트 </summary>
public class InitLaserTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("레이저 터렛 초기화");
        TurretUpgradeInfo laserTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretInit);
    }
}

/****************************************************************************
                      Rocket Turret Enhancement Events
****************************************************************************/
/// <summary> 로켓 지속 시간 증가 이벤트 </summary>
public class RocketTurretLifeTimeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 지속 시간 증가");
        TurretUpgradeInfo rocketTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeIncrease);
    }
}

/// <summary> 로켓 지속 시간 감소 이벤트 </summary>
public class RocketTurretLifeTimeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 지속 시간 감소");
        TurretUpgradeInfo rocketTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeDecrease);
    }
}

/// <summary> 로켓 개수 증가 이벤트 </summary>
public class RocketTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 개수 증가");
        TurretUpgradeInfo rocketTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountIncrease);
    }
}

/// <summary> 로켓 개수 감소 이벤트 </summary>
public class RocketTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 개수 감소");
        TurretUpgradeInfo rocketTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountDecrease);
    }
}

/// <summary> 로켓 속도 증가 이벤트 </summary>
public class RocketTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 속도 증가");
        TurretUpgradeInfo rocketTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedIncrease);
    }
}

/// <summary> 로켓 속도 감소 이벤트 </summary>
public class RocketTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 속도 감소");
        TurretUpgradeInfo rocketTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedDecrease);
    }
}

/// <summary> 로켓 크기 증가 이벤트 </summary>
public class RocketTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 크기 증가");
        TurretUpgradeInfo rocketTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeIncrease);
    }
}

/// <summary> 로켓 크기 감소 이벤트 </summary>
public class RocketTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 크기 감소");
        TurretUpgradeInfo rocketTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeDecrease);
    }
}

/// <summary> 로켓 터렛 초기화 이벤트 </summary>
public class InitRocketTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("로켓 터렛 초기화");
        TurretUpgradeInfo rocketTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretInit);
    }
}

/****************************************************************************
                      Mortar Turret Enhancement Events
****************************************************************************/
/// <summary> 박격포탄 분열 활성화 이벤트 </summary>
public class MortarTurretSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 분열 활성화");
        TurretUpgradeInfo mortarTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSplit);
    }
}

/// <summary> 박격포탄 분열 비활성화 이벤트 </summary>
public class MortarTurretRemoveSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 분열 비활성화");
        TurretUpgradeInfo mortarTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretRemoveSplit);
    }
}

/// <summary> 박격포탄 개수 증가 이벤트 </summary>
public class MortarTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 개수 증가");
        TurretUpgradeInfo mortarTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountIncrease);
    }
}

/// <summary> 박격포탄 개수 감소 이벤트 </summary>
public class MortarTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 개수 감소");
        TurretUpgradeInfo mortarTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountDecrease);
    }
}

/// <summary> 박격포탄 속도 증가 이벤트 </summary>
public class MortarTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 속도 증가");
        TurretUpgradeInfo mortarTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedIncrease);
    }
}

/// <summary> 박격포탄 속도 감소 이벤트 </summary>
public class MortarTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 속도 감소");
        TurretUpgradeInfo mortarTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedDecrease);
    }
}

/// <summary> 박격포 이펙트 범위 증가 이벤트 </summary>
public class MortarTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 폭발 범위 증가");
        TurretUpgradeInfo mortarTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeIncrease);
    }
}

/// <summary> 박격포 이펙트 범위 감소 이벤트 </summary>
public class MortarTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포탄 폭발 범위 감소");
        TurretUpgradeInfo mortarTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeDecrease);
    }
}

/// <summary> 박격포 터렛 초기화 이벤트 </summary>
public class InitMortarTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("박격포 터렛 초기화");
        TurretUpgradeInfo mortarTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretInit);
    }
}
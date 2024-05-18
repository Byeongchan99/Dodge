using UnityEngine;

public interface ITurretUpgradeEvent
{
    void ExecuteEvent();
}

/****************************************************************************
                       Bullet Turret Enhancement Events
****************************************************************************/
/// <summary> �Ѿ� �п� Ȱ��ȭ �̺�Ʈ </summary>
public class BulletTurretSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� �п� Ȱ��ȭ");
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }
}

/// <summary> �Ѿ� �ͷ� �п� �Ѿ� ��Ȱ��ȭ �̺�Ʈ </summary>
public class BulletTurretRemoveSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� �п� ��Ȱ��ȭ");
        TurretUpgradeInfo bulletTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }
}

/// <summary> �Ѿ� ���� ���� �̺�Ʈ </summary>
public class BulletTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� ���� ����");
        TurretUpgradeInfo bulletTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountIncrease);
    }
}

/// <summary> �Ѿ� ���� ���� �̺�Ʈ </summary>
public class BulletTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� ���� ����");
        TurretUpgradeInfo bulletTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretCountDecrease);
    }
}

/// <summary> �Ѿ� �ӵ� ���� �̺�Ʈ </summary>
public class BulletTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� �ӵ� ����");
        TurretUpgradeInfo bulletTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedIncrease);
    }
}

/// <summary> �Ѿ� �ӵ� ���� �̺�Ʈ </summary>
public class BulletTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� �ӵ� ����");
        TurretUpgradeInfo bulletTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSpeedDecrease);
    }
}

/// <summary> �Ѿ� ũ�� ���� �̺�Ʈ </summary>
public class BulletTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� ũ�� ����");
        TurretUpgradeInfo bulletTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeIncrease);
    }
}

/// <summary> �Ѿ� ũ�� ���� �̺�Ʈ </summary>
public class BulletTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� ũ�� ����");
        TurretUpgradeInfo bulletTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSizeDecrease);
    }
}

/// <summary> �Ѿ� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
public class InitBulletTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�Ѿ� �ͷ� �ʱ�ȭ");
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
/// <summary> ������ ���� �ð� ���� �̺�Ʈ </summary>
public class LaserTurretLifeTimeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ���� �ð� ����");
        TurretUpgradeInfo laserTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeIncrease);
    }
}

/// <summary> ������ ���� �ð� ���� �̺�Ʈ </summary>
public class LaserTurretLifeTimeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ���� �ð� ����");
        TurretUpgradeInfo laserTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretLifeTimeDecrease);
    }
}

/// <summary> ������ ���� ���� �̺�Ʈ </summary>
public class LaserTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ���� ����");
        TurretUpgradeInfo laserTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountIncrease);
    }
}

/// <summary> ������ ���� ���� �̺�Ʈ </summary>
public class LaserTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ���� ����");
        TurretUpgradeInfo laserTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretCountDecrease);
    }
}

/// <summary> ������ ũ�� ���� �̺�Ʈ </summary>
public class LaserTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ũ�� ����");
        TurretUpgradeInfo laserTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeIncrease);
    }
}

/// <summary> ������ ũ�� ���� �̺�Ʈ </summary>
public class LaserTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ ũ�� ����");
        TurretUpgradeInfo laserTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Laser,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", laserTurretSizeDecrease);
    }
}

/// <summary> ������ �ʱ�ȭ �̺�Ʈ </summary>
public class InitLaserTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("������ �ͷ� �ʱ�ȭ");
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
/// <summary> ���� ���� �ð� ���� �̺�Ʈ </summary>
public class RocketTurretLifeTimeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ���� �ð� ����");
        TurretUpgradeInfo rocketTurretLifeTimeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeIncrease);
    }
}

/// <summary> ���� ���� �ð� ���� �̺�Ʈ </summary>
public class RocketTurretLifeTimeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ���� �ð� ����");
        TurretUpgradeInfo rocketTurretLifeTimeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.LifeTimeChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretLifeTimeDecrease);
    }
}

/// <summary> ���� ���� ���� �̺�Ʈ </summary>
public class RocketTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ���� ����");
        TurretUpgradeInfo rocketTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountIncrease);
    }
}

/// <summary> ���� ���� ���� �̺�Ʈ </summary>
public class RocketTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ���� ����");
        TurretUpgradeInfo rocketTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretCountDecrease);
    }
}

/// <summary> ���� �ӵ� ���� �̺�Ʈ </summary>
public class RocketTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� �ӵ� ����");
        TurretUpgradeInfo rocketTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedIncrease);
    }
}

/// <summary> ���� �ӵ� ���� �̺�Ʈ </summary>
public class RocketTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� �ӵ� ����");
        TurretUpgradeInfo rocketTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.5f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSpeedDecrease);
    }
}

/// <summary> ���� ũ�� ���� �̺�Ʈ </summary>
public class RocketTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ũ�� ����");
        TurretUpgradeInfo rocketTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeIncrease);
    }
}

/// <summary> ���� ũ�� ���� �̺�Ʈ </summary>
public class RocketTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� ũ�� ����");
        TurretUpgradeInfo rocketTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Rocket,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", rocketTurretSizeDecrease);
    }
}

/// <summary> ���� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
public class InitRocketTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("���� �ͷ� �ʱ�ȭ");
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
/// <summary> �ڰ���ź �п� Ȱ��ȭ �̺�Ʈ </summary>
public class MortarTurretSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź �п� Ȱ��ȭ");
        TurretUpgradeInfo mortarTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSplit);
    }
}

/// <summary> �ڰ���ź �п� ��Ȱ��ȭ �̺�Ʈ </summary>
public class MortarTurretRemoveSplit : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź �п� ��Ȱ��ȭ");
        TurretUpgradeInfo mortarTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.IsProjectileSplit,
            value = 0,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretRemoveSplit);
    }
}

/// <summary> �ڰ���ź ���� ���� �̺�Ʈ </summary>
public class MortarTurretCountIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź ���� ����");
        TurretUpgradeInfo mortarTurretCountIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = 1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountIncrease);
    }
}

/// <summary> �ڰ���ź ���� ���� �̺�Ʈ </summary>
public class MortarTurretCountDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź ���� ����");
        TurretUpgradeInfo mortarTurretCountDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.CountChange,
            value = -1,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretCountDecrease);
    }
}

/// <summary> �ڰ���ź �ӵ� ���� �̺�Ʈ </summary>
public class MortarTurretSpeedIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź �ӵ� ����");
        TurretUpgradeInfo mortarTurretSpeedIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = -0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedIncrease);
    }
}

/// <summary> �ڰ���ź �ӵ� ���� �̺�Ʈ </summary>
public class MortarTurretSpeedDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź �ӵ� ����");
        TurretUpgradeInfo mortarTurretSpeedDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SpeedChange,
            value = 0.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSpeedDecrease);
    }
}

/// <summary> �ڰ��� ����Ʈ ���� ���� �̺�Ʈ </summary>
public class MortarTurretSizeIncrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź ���� ���� ����");
        TurretUpgradeInfo mortarTurretSizeIncrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 1.1f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeIncrease);
    }
}

/// <summary> �ڰ��� ����Ʈ ���� ���� �̺�Ʈ </summary>
public class MortarTurretSizeDecrease : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ���ź ���� ���� ����");
        TurretUpgradeInfo mortarTurretSizeDecrease = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.SizeChange,
            value = 10f / 11f,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretSizeDecrease);
    }
}

/// <summary> �ڰ��� �ͷ� �ʱ�ȭ �̺�Ʈ </summary>
public class InitMortarTurret : ITurretUpgradeEvent
{
    public void ExecuteEvent()
    {
        Debug.Log("�ڰ��� �ͷ� �ʱ�ȭ");
        TurretUpgradeInfo mortarTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Mortar,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", mortarTurretInit);
    }
}
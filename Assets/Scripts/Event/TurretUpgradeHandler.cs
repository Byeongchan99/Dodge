using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeHandler : MonoBehaviour
{
    /// <summary> 총알 터렛 분열 총알 활성화 이벤트 </summary>
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

    /// <summary> 총알 터렛 개수 증가 이벤트 </summary>
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

    /// <summary> 총알 터렛 개수 감소 이벤트 </summary>
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

    /// <summary> 총알 터렛 속도 증가 이벤트 </summary>
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

    /// <summary> 총알 터렛 속도 감소 이벤트 </summary>
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
}

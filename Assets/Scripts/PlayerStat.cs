using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // 싱글톤 인스턴스

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    // 임시로 이벤트 테스트를 위한 메서드
    /// <summary> 총알 터렛 분열 총알 활성화 이벤트 </summary>
    public void BulletTurretSplitEvent()
    {
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.ProjectileSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> 총알 터렛 분열 총알 비활성화 이벤트 </summary>
    public void BulletTurretRemoveSplitEvent()
    {
        TurretUpgradeInfo bulletTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.RemoveSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }

    /// <summary> 총알 터렛 초기화 이벤트 </summary>
    public void InitBulletTurretEvent()
    {
        TurretUpgradeInfo bulletTurretInit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.Init,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretInit);
    }
}

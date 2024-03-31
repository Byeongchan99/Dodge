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
    /// <summary> 터렛 분열 총알 활성화 이벤트 </summary>
    public void TurretSplitEvent()
    {
        TurretUpgrade bulletTurretSplit = new TurretUpgrade
        {
            turretType = TurretUpgrade.TurretType.Bullet,
            enhancementType = TurretUpgrade.EnhancementType.ProjectileSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> 터렛 분열 총알 활성화 이벤트 </summary>
    public void TurretRemoveSplitEvent()
    {
        TurretUpgrade bulletTurretRemoveSplit = new TurretUpgrade
        {
            turretType = TurretUpgrade.TurretType.Bullet,
            enhancementType = TurretUpgrade.EnhancementType.RemoveSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }
}

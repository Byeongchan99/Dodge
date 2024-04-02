using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventProcessor : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    public void HandleEvent(string eventName)
    {
        // 이벤트에 따른 로직 처리

        // currentStatData 변경 예시
        /*
        CopyedStatData updatedStatData = StatDataManager.Instance.GetDataForEvent(eventName);
        StatDataManager.Instance.currentStatData = updatedStatData;
        */
    }

    /// <summary> 이벤트 적용 </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        switch (enhancement.turretType)
        {
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret의 업그레이드 처리를 위한 내부 switch 문
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret 분열 총알 업그레이드 처리
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = 1;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.RemoveSplit:
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = 0;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화 로직

                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeIncrease:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.InductionUpgrade:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }
    }
}

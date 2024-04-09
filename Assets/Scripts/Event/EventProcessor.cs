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
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // 분열 총알 업그레이드 처리
                        Debug.Log("분열 총알 업그레이드 " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가 처리
                        Debug.Log("총알 개수 변경 " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount += (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가 처리
                        Debug.Log("총알 속도 변경 " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화 로직
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeChange:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsInductionUpgrade:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }
    }
}

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
            // 총알 터렛
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // 분열 총알 업그레이드
                        Debug.Log("분열 총알 업그레이드 " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        Debug.Log("총알 개수 변경 " + enhancement.value);
                        // 범위 설정
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 변경
                        Debug.Log("총알 속도 변경 " + enhancement.value);
                        // 범위 설정
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("총알 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;  
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                }
                break;

            // 레이저 터렛
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // 지속 시간
                        Debug.Log("레이저 지속 시간 변경 " + enhancement.value);
                        // 범위 설정
                        float newLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime = Mathf.Max(0.5f, newLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        Debug.Log("레이저 개수 변경 " + enhancement.value);
                        // 범위 설정
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 속도 변경
                        Debug.Log("레이저 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                        // 기타 필요한 경우 추가
                }
                break;

            // 로켓 터렛
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // 지속 시간 증가
                        Debug.Log("로켓 지속 시간 변경 " + enhancement.value);
                        // 범위 설정
                        float newLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime = Mathf.Max(0.5f, newLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가
                        Debug.Log("로켓 개수 변경 " + enhancement.value);
                        // 범위 설정
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[2].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[2].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가
                        Debug.Log("로켓 속도 변경 " + enhancement.value);
                        // 범위 설정
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("로켓 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                    // 기타 필요한 경우 추가
                }
                break;

            // 박격포 터렛
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // 분열 박격포탄 업그레이드
                        Debug.Log("분열 박격포탄 업그레이드 " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가
                        Debug.Log("박격포탄 개수 변경 " + enhancement.value);
                        // 범위 설정
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[3].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가
                        Debug.Log("박격포탄 속도 변경 " + enhancement.value);
                        // 범위 설정
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("폭발 범위 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }
    }
}

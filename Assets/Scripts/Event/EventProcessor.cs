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
                        Debug.Log("projectileCount " + StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 변경
                        Debug.Log("총알 속도 변경 " + enhancement.value);
                        // 범위 설정
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 0.5f, 10f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("총알 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.z *= enhancement.value
                            );
                        // 적용할 최소 및 최대 크기 제한값 설정
                        Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
                        Vector3 maxSize = new Vector3(1f, 1f, 1f);

                        // Vector3의 각 요소에 대해 Clamp 적용
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;  
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.InitStatData();
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
                        float newProjectileLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime + enhancement.value;
                        // projectileLifeTime을 최소 0.5초, 최대 5초 사이로 제한
                        float clampedProjectileLifeTime = Mathf.Clamp(newProjectileLifeTime, 0.5f, 5f);
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime = clampedProjectileLifeTime;
                        // newProjectileLifeTime 5보다 컸을 경우에만 터렛의 유지 시간 조정
                        if (newProjectileLifeTime > 5f)
                        {
                            float adjustedTurretLifeTime = StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime + (enhancement.value * StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount);
                            StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime = Mathf.Max(3f, adjustedTurretLifeTime);
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        Debug.Log("레이저 개수 변경 " + enhancement.value);
                        // 범위 설정
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount = Mathf.Max(1, newProjectileCount);
                        // 터렛의 유지 시간 또한 같이 변경
                        float newTurretLifeTime = (StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime * StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount);
                        StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime = Mathf.Max(3f, newTurretLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 속도 변경
                        Debug.Log("레이저 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.y = 25f, // 고정값
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.z *= enhancement.value
                            );

                        // 적용할 최소 및 최대 크기 제한값 설정
                        Vector3 minSize = new Vector3(0.2f, 25f, 0.2f);
                        Vector3 maxSize = new Vector3(2.5f, 25f, 2.5f);

                        // Vector3의 각 요소에 대해 Clamp 적용
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = 25f; // 고정값
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.InitStatData();
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
                        float newProjectileLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime = Mathf.Clamp(newProjectileLifeTime, 5f, 20f);
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
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 0.5f, 10f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("로켓 크기 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.z *= enhancement.value
                            );
                        // 적용할 최소 및 최대 크기 제한값 설정
                        Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
                        Vector3 maxSize = new Vector3(1f, 1f, 1f);

                        // Vector3의 각 요소에 대해 Clamp 적용
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.InitStatData();
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
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 1f, 5f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        Debug.Log("폭발 범위 변경 " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.z *= enhancement.value
                            );
                        // 적용할 최소 및 최대 크기 제한값 설정
                        Vector3 minSize = new Vector3(1.666667f, 1f, 1.666667f);
                        Vector3 maxSize = new Vector3(3.75f, 2.25f, 3.75f);

                        // Vector3의 각 요소에 대해 Clamp 적용
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        Debug.Log("터렛 초기화");
                        StatDataManager.Instance.InitStatData();
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }
    }
}

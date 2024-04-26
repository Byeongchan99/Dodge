using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTurret : BaseTurret
{
    /// <summary> 발사 </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> 터렛 회전 </summary>
    protected override void RotateTurret()
    {
        ShootProjectile();
        _timeSinceLastShot = 0f;
    }

    /// <summary> 발사체 생성 </summary>
    private void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // 투사체 오브젝트 풀에서 총알 가져오기
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex;
        MortarBomb bomb = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as MortarBomb;

        // 박격포탄의 데미지는 이펙트에서 플레이어에게 적용
        // 이펙트 투사체 풀에서 위험 범위 이펙트 가져오기
        MortarBombEffect bombEffect = EffectPoolManager.Instance.Get("MortarBombEffect") as MortarBombEffect;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize;

        if (bomb != null && bombEffect != null) 
        {
            // 총알 위치와 회전 설정
            bomb.transform.position = firePoint.position;
            bomb.SetDirection(targetPosition.position - firePoint.position);
            bomb.gameObject.SetActive(true);

            // 위험 범위 이펙트 위치 설정
            bombEffect.transform.position = targetPosition.position;
            // 위험 범위 이펙트 크기 설정
            bombEffect.transform.localScale = effectSize;
            bombEffect.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }
    }

    protected override void DisableTurret()
    {
        base.DisableTurret();
    }
}

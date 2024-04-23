using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class LaserTurret : BaseTurret
{
    [SerializeField] Transform rotatePoint; // 회전시킬 자식 오브젝트
    [SerializeField] Vector2 _direction;
    quaternion rotation;
    float _angle;

    protected override bool ShouldShoot()
    {
        return !isLastProjectileShot;
    }

    /// <summary> 발사 </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> 터렛 회전 </summary>
    protected override void RotateTurret()
    {
        if (targetPosition != null && rotatePoint != null)
        {
            // 플레이어를 향한 방향 벡터 계산
            _direction = targetPosition.position - rotatePoint.position;
            // atan2를 사용하여 라디안으로 방향 각도를 계산한 다음, 도(degree)로 변환
            _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            if (spawnPointIndex == 0)
            {
                _angle += 90;
            }
            // 1번부터 3번 스폰 포인트에서는 터렛 스프라이트를 반대로 설정
            else if (spawnPointIndex >= 1 && spawnPointIndex <= 3)
            {
                _angle += 180;
            }
            else if (spawnPointIndex == 4)
            {
                _angle -= 90;
            }

            // Z축 회전
            Vector3 rotation = new Vector3(0, 0, _angle);
            // 회전 애니메이션이 완료된 후 ShootProjectile 메서드 호출
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).OnComplete(activeAttackArea);
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> 공격 범위 활성화 </summary>
    private void activeAttackArea()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // 이펙트 투사체 풀에서 위험 범위 이펙트 가져오기
        LaserAttackAreaEffect laserEffect = EffectPoolManager.Instance.Get("LaserAttackAreaEffect") as LaserAttackAreaEffect;

        if (laserEffect != null)
        {
            // 위치 설정
            laserEffect.transform.position = firePoint.position;
            laserEffect.transform.rotation = rotation;
            laserEffect.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get effect from pool.");
        }

        // 1초 지연 후 ShootProjectile 호출
        StartCoroutine(ShootProjectileAfterDelay(1f));
    }

    private IEnumerator ShootProjectileAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShootProjectile();
    }
    /// <summary> 발사체 생성 </summary>
    private void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // direction 벡터를 바탕으로 Quaternion 생성
        rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // 오브젝트 풀에서 총알 가져오기
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex;
        Laser laser = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Laser;

        if (laser != null)
        {
            // 총알 위치와 회전 설정
            laser.transform.position = firePoint.position;
            laser.transform.rotation = rotation;
            laser.SetDirection(_direction);
            laser.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }
    }

    protected override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

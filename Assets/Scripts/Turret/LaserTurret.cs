using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class LaserTurret : BaseTurret
{
    [SerializeField] Transform rotatePoint; // 회전시킬 자식 오브젝트
    [SerializeField] Vector2 _direction; // 플레이어를 향한 방향 벡터
    float _angle; // 플레이어를 향한 각도
    quaternion _rotation;
    [SerializeField] bool isShooting = false;

    protected override bool ShouldShoot()
    {
        return (_timeSinceLastShot >= _attackSpeed && _currentProjectileCount > 0 && isShooting == false);
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
            else if (spawnPointIndex >= 1 && spawnPointIndex <= 5)
            {
                _angle += 180;
            }
            else if (spawnPointIndex == 6)
            {
                _angle -= 90;
            }

            // Z축 회전
            Vector3 rotation = new Vector3(0, 0, _angle);
            // 회전 애니메이션이 완료된 후 ShootProjectile 메서드 호출
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() => StartCoroutine(shootLaser()));
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> 공격 범위 활성화 후 레이저 생성 </summary>
    private IEnumerator shootLaser()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            yield break;
        }

        _rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // 이펙트 투사체 풀에서 위험 범위 이펙트 가져오기
        LaserAttackAreaEffect laserEffect = EffectPoolManager.Instance.Get("LaserAttackAreaEffect") as LaserAttackAreaEffect;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize;

        if (laserEffect != null)
        {
            // 위치 설정
            laserEffect.transform.position = firePoint.position;
            laserEffect.transform.rotation = _rotation;
            // 크기 설정
            laserEffect.transform.localScale = effectSize;
            // 위험 범위 활성화
            laserEffect.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get effect from pool.");
        }

        // 레이저 발사 속도만큼 지연 후 레이저 생성
        yield return new WaitForSeconds(StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSpeed);
        isShooting = true;

        // 오브젝트 풀에서 레이저 가져오기
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[1].projectileIndex;
        Laser laser = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Laser;
        Vector3 laserSize = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize;

        if (laser != null)
        {
            // 레이저 위치와 회전 설정
            laser.transform.position = firePoint.position;
            laser.transform.rotation = _rotation;
            // 크기 설정
            laser.transform.localScale = laserSize;
            // 레이저 활성화
            laser.gameObject.SetActive(true);
            AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }

        yield return new WaitForSeconds(StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime);
        isShooting = false;
    }

    /// <summary> 터렛 비활성화 </summary>
    public override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

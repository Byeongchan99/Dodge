using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;


public class BulletTurret : BaseTurret
{
    //float rotationSpeed = 2f; // 터렛이 회전하는 속도
    [SerializeField] Transform rotatePoint; // 회전시킬 자식 오브젝트
    [SerializeField] Vector2 _direction;
    float _angle;

    [SerializeField] private ParticleSystem fireParticle; // 파티클 시스템 참조 추가

    /// <summary> 발사 </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> 터렛 회전 </summary>
    protected override void RotateTurret()
    {
        Debug.Log("RotateTurret()");
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
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(ShootProjectile);
            Debug.Log("회전 애니메이션 완료");
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> 총알 생성 </summary>
    private void ShootProjectile()
    {
        Debug.Log("ShootProjectile()");
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // direction 벡터를 바탕으로 Quaternion 생성
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // 오브젝트 풀에서 총알 가져오기
        int currentProjectileIndex= StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex;
        Bullet bullet = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Bullet;
        Vector3 bulletSize = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize;

        if (bullet != null)
        { 
            // 총알 위치와 회전 설정
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = rotation;
            // 크기 설정
            bullet.transform.localScale = bulletSize;
            // 총알 활성화
            bullet.gameObject.SetActive(true);
            // 방향 설정
            bullet.SetDirection(_direction);

            // 파티클 시스템 재생
            if (fireParticle != null)
            {
                fireParticle.Play(); // 파티클 재생
            }
            else
            {
                Debug.LogWarning("Fire particle system is not set.");
            }

            AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }
    }

    /// <summary> 터렛 비활성화 </summary>
    public override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

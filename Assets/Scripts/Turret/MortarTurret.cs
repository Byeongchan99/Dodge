using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTurret : BaseTurret
{
    private PlayerMovement playerMovement; // 플레이어 움직임 참조
    private float predictionFactor = 1f; // 예측 정도를 조절하는 변수
    private float randomFactor = 1f; // 랜덤 정도를 조절하는 변수

    [SerializeField] private ParticleSystem fireParticle; // 파티클 시스템 참조 추가

    protected override void OnEnable()
    {
        playerMovement = PlayerStat.Instance.player.GetComponent<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
        }
        base.OnEnable();
    }

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
            // 플레이어의 진행 벡터와 랜덤 벡터 계산
            Vector3 playerMovementVector = playerMovement.InputVec * predictionFactor;
            Vector3 randomOffset = new Vector3(Random.Range(-randomFactor, randomFactor), 0, Random.Range(-randomFactor, randomFactor));

            // 최종 목표 위치 계산
            Vector3 finalTargetPosition = targetPosition.position + playerMovementVector + randomOffset;

            // 박격포탄 위치와 설정
            bomb.transform.position = firePoint.position;         
            // 이펙트 참조 설정
            bomb.SetBombEffect(bombEffect);
            // 활성화
            bomb.gameObject.SetActive(true);
            // 방향 설정(박격포탄의 경우 Move() 메서드가 코루틴이라 활성화 후 실행)
            bomb.SetDirection(finalTargetPosition - firePoint.position);

            // 위험 범위 이펙트 위치 설정
            bombEffect.transform.position = finalTargetPosition;
            // 위험 범위 이펙트 크기 설정
            bombEffect.transform.localScale = effectSize;
            bombEffect.gameObject.SetActive(true);

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

    public override void DisableTurret()
    {
        base.DisableTurret();
    }
}

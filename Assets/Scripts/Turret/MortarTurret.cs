using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTurret : BaseTurret
{
    private PlayerMovement playerMovement;
    private float predictionFactor = 1f; // ���� ������ �����ϴ� ����
    private float randomFactor = 1f; // ���� ������ �����ϴ� ����

    protected override void OnEnable()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement script not found in the scene.");
        }
        base.OnEnable();
    }

    /// <summary> �߻� </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> �ͷ� ȸ�� </summary>
    protected override void RotateTurret()
    {
        ShootProjectile();
        _timeSinceLastShot = 0f;
    }

    /// <summary> �߻�ü ���� </summary>
    private void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // ����ü ������Ʈ Ǯ���� �Ѿ� ��������
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex;
        MortarBomb bomb = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as MortarBomb;

        // �ڰ���ź�� �������� ����Ʈ���� �÷��̾�� ����
        // ����Ʈ ����ü Ǯ���� ���� ���� ����Ʈ ��������
        MortarBombEffect bombEffect = EffectPoolManager.Instance.Get("MortarBombEffect") as MortarBombEffect;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize;

        if (bomb != null && bombEffect != null)
        {
            // �÷��̾��� ���� ���Ϳ� ���� ���� ���
            Vector3 playerMovementVector = playerMovement.InputVec * predictionFactor;
            Vector3 randomOffset = new Vector3(Random.Range(-randomFactor, randomFactor), 0, Random.Range(-randomFactor, randomFactor));

            // ���� ��ǥ ��ġ ���
            Vector3 finalTargetPosition = targetPosition.position + playerMovementVector + randomOffset;

            // �Ѿ� ��ġ�� ȸ�� ����
            bomb.transform.position = firePoint.position;
            bomb.SetDirection(finalTargetPosition - firePoint.position);
            // ����Ʈ ���� ����
            bomb.SetBombEffect(bombEffect);
            bomb.gameObject.SetActive(true);

            // ���� ���� ����Ʈ ��ġ ����
            bombEffect.transform.position = finalTargetPosition;
            // ���� ���� ����Ʈ ũ�� ����
            bombEffect.transform.localScale = effectSize;
            bombEffect.gameObject.SetActive(true);
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

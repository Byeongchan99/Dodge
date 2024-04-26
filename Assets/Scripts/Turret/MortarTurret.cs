using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class MortarTurret : BaseTurret
{
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
            // �Ѿ� ��ġ�� ȸ�� ����
            bomb.transform.position = firePoint.position;
            bomb.SetDirection(targetPosition.position - firePoint.position);
            bomb.gameObject.SetActive(true);

            // ���� ���� ����Ʈ ��ġ ����
            bombEffect.transform.position = targetPosition.position;
            // ���� ���� ����Ʈ ũ�� ����
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

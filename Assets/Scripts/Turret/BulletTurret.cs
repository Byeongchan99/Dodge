using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;


public class BulletTurret : BaseTurret
{
    //float rotationSpeed = 2f; // �ͷ��� ȸ���ϴ� �ӵ�
    [SerializeField] Transform rotatePoint; // ȸ����ų �ڽ� ������Ʈ
    [SerializeField] Vector2 _direction;
    float _angle;

    /// <summary> �߻� </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> �ͷ� ȸ�� </summary>
    protected override void RotateTurret()
    {
        if (targetPosition != null && rotatePoint != null)
        {
            // �÷��̾ ���� ���� ���� ���
            _direction = targetPosition.position - rotatePoint.position;
            // atan2�� ����Ͽ� �������� ���� ������ ����� ����, ��(degree)�� ��ȯ
            _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

            if (spawnPointIndex == 0)
            {
                _angle += 90;
            }
            // 1������ 3�� ���� ����Ʈ������ �ͷ� ��������Ʈ�� �ݴ�� ����
            else if (spawnPointIndex >= 1 && spawnPointIndex <= 3)
            {
                _angle += 180;
            }
            else if (spawnPointIndex == 4)
            {
                _angle -= 90;
            }

            // Z�� ȸ��
            Vector3 rotation = new Vector3(0, 0, _angle);
            // ȸ�� �ִϸ��̼��� �Ϸ�� �� ShootProjectile �޼��� ȣ��
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).OnComplete(ShootProjectile);
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> �߻�ü ���� </summary>
    private void ShootProjectile()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // direction ���͸� �������� Quaternion ����
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // ������Ʈ Ǯ���� �Ѿ� ��������
        int currentProjectileIndex= StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex;
        Bullet bullet = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Bullet;
        Vector3 bulletSize = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize;

        if (bullet != null)
        {
            // �Ѿ� ��ġ�� ȸ�� ����
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = rotation;
            // ũ�� ����
            bullet.transform.localScale = bulletSize;
            // ���� ����
            bullet.SetDirection(_direction);
            bullet.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }
    }

    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    protected override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


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
            // ��������Ʈ�� ������ ������ ���� ����
            if (transform.localScale.x < 0)
            {
                _angle = 180f - _angle; // ������ ��������Ʈ�� ���� ������ ����
            }
            // Z�� ȸ��
            Vector3 rotation = new Vector3(0, 0, _angle);
            // ȸ�� �ִϸ��̼��� �Ϸ�� �� ShootProjectile �޼��� ȣ��
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).OnComplete(ShootProjectile);
        }
    }

    /// <summary> �߻�ü ���� </summary>
    private void ShootProjectile()
    {
        if (currentProjectilePrefabs == null || firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            return;
        }

        // direction ���͸� �ݽð� �������� 90�� ȸ��
        //Vector2 shootingDirection = new Vector2(-direction.y, direction.x);
        // direction ���͸� �������� Quaternion ����
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // ������Ʈ Ǯ���� �Ѿ� ��������
        BaseProjectile projectile = ProjectilePoolManager.Instance.Get(currentProjectilePrefabs.name);

        if (projectile != null)
        {
            // �Ѿ� ��ġ�� ȸ�� ����
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = rotation;
            projectile.SetDirection(_direction);
            projectile.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get projectile from pool.");
        }
    }
}

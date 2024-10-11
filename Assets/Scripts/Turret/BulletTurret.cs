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

    [SerializeField] private ParticleSystem fireParticle; // ��ƼŬ �ý��� ���� �߰�

    /// <summary> �߻� </summary>
    protected override void Shoot()
    {
        RotateTurret();
    }

    /// <summary> �ͷ� ȸ�� </summary>
    protected override void RotateTurret()
    {
        Debug.Log("RotateTurret()");
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
            else if (spawnPointIndex >= 1 && spawnPointIndex <= 5)
            {
                _angle += 180;
            }
            else if (spawnPointIndex == 6)
            {
                _angle -= 90;
            }

            // Z�� ȸ��
            Vector3 rotation = new Vector3(0, 0, _angle);
            // ȸ�� �ִϸ��̼��� �Ϸ�� �� ShootProjectile �޼��� ȣ��
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(ShootProjectile);
            Debug.Log("ȸ�� �ִϸ��̼� �Ϸ�");
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> �Ѿ� ���� </summary>
    private void ShootProjectile()
    {
        Debug.Log("ShootProjectile()");
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
            // �Ѿ� Ȱ��ȭ
            bullet.gameObject.SetActive(true);
            // ���� ����
            bullet.SetDirection(_direction);

            // ��ƼŬ �ý��� ���
            if (fireParticle != null)
            {
                fireParticle.Play(); // ��ƼŬ ���
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

    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    public override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

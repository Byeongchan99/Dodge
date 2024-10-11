using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : BaseTurret
{
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
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[2].projectileIndex;
        Rocket rocket = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Rocket;
        Vector3 rocketSize = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize;

        if (rocket != null)
        {
            // ���� ��ġ�� ȸ�� ����
            rocket.transform.position = firePoint.position;
            rocket.transform.rotation = rotation;
            // ũ�� ����
            rocket.transform.localScale = rocketSize;
            // ��ǥ ����
            rocket.target = targetPosition;
            rocket.gameObject.SetActive(true);

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

    /// <summary> ���� ��Ȱ��ȭ </summary>
    public override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

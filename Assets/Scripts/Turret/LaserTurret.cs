using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class LaserTurret : BaseTurret
{
    [SerializeField] Transform rotatePoint; // ȸ����ų �ڽ� ������Ʈ
    [SerializeField] Vector2 _direction; // �÷��̾ ���� ���� ����
    float _angle; // �÷��̾ ���� ����
    quaternion _rotation;
    [SerializeField] bool isShooting = false;

    protected override bool ShouldShoot()
    {
        return (_timeSinceLastShot >= _attackSpeed && _currentProjectileCount > 0 && isShooting == false);
    }

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
            rotatePoint.DOLocalRotate(rotation, 0.5f).SetEase(Ease.OutSine).SetUpdate(true).OnComplete(() => StartCoroutine(shootLaser()));
            _timeSinceLastShot = 0f;
        }
    }

    /// <summary> ���� ���� Ȱ��ȭ �� ������ ���� </summary>
    private IEnumerator shootLaser()
    {
        if (firePoint == null)
        {
            Debug.LogError("Projectile prefab or fire point is not set.");
            yield break;
        }

        _rotation = Quaternion.LookRotation(Vector3.forward, _direction);

        // ����Ʈ ����ü Ǯ���� ���� ���� ����Ʈ ��������
        LaserAttackAreaEffect laserEffect = EffectPoolManager.Instance.Get("LaserAttackAreaEffect") as LaserAttackAreaEffect;
        Vector3 effectSize = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize;

        if (laserEffect != null)
        {
            // ��ġ ����
            laserEffect.transform.position = firePoint.position;
            laserEffect.transform.rotation = _rotation;
            // ũ�� ����
            laserEffect.transform.localScale = effectSize;
            // ���� ���� Ȱ��ȭ
            laserEffect.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Failed to get effect from pool.");
        }

        // ������ �߻� �ӵ���ŭ ���� �� ������ ����
        yield return new WaitForSeconds(StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSpeed);
        isShooting = true;

        // ������Ʈ Ǯ���� ������ ��������
        int currentProjectileIndex = StatDataManager.Instance.currentStatData.turretDatas[1].projectileIndex;
        Laser laser = ProjectilePoolManager.Instance.Get(projectilePrefabs[currentProjectileIndex].name) as Laser;
        Vector3 laserSize = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize;

        if (laser != null)
        {
            // ������ ��ġ�� ȸ�� ����
            laser.transform.position = firePoint.position;
            laser.transform.rotation = _rotation;
            // ũ�� ����
            laser.transform.localScale = laserSize;
            // ������ Ȱ��ȭ
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

    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    public override void DisableTurret()
    {
        rotatePoint.localRotation = Quaternion.identity;
        base.DisableTurret();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject splitBulletPrefab;
    public GameObject laserPrefab;
    public GameObject rocketPrefab;
    public GameObject mortarBombPrefab;
    public GameObject splitMortarBomb;
    public GameObject splitedMortarBomb;
    // �ٸ� �߻�ü �����յ� �߰�

    public Transform projectileContainer;

    private void Start()
    {
        ProjectilePoolManager.Instance.CreatePool(bulletPrefab.GetComponent<Bullet>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(splitBulletPrefab.GetComponent<SplitBullet>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(laserPrefab.GetComponent<Laser>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(rocketPrefab.GetComponent<Rocket>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(mortarBombPrefab.GetComponent<MortarBomb>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(splitMortarBomb.GetComponent<SplitMortarBomb>(), 20, projectileContainer);
        ProjectilePoolManager.Instance.CreatePool(splitedMortarBomb.GetComponent<SplitedMortarBomb>(), 20, projectileContainer);
        // �ٸ� �߻�ü Ǯ�� ����
    }
}

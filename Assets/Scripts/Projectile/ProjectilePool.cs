using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject bulletPrefab; // �Ѿ�
    public GameObject splitBulletPrefab; // �п��ϴ� �Ѿ�
    public GameObject laserPrefab; // ������
    public GameObject rocketPrefab; // ����
    public GameObject mortarBombPrefab; // �ڰ���ź
    public GameObject splitMortarBomb; // �п��ϴ� �ڰ���ź
    public GameObject splitedMortarBomb; // �п��� �ڰ���ź
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

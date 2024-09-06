using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알
    public GameObject splitBulletPrefab; // 분열하는 총알
    public GameObject laserPrefab; // 레이저
    public GameObject rocketPrefab; // 로켓
    public GameObject mortarBombPrefab; // 박격포탄
    public GameObject splitMortarBomb; // 분열하는 박격포탄
    public GameObject splitedMortarBomb; // 분열된 박격포탄
    // 다른 발사체 프리팹도 추가

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
        // 다른 발사체 풀도 생성
    }
}

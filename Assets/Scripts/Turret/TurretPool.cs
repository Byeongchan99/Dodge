using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPool : MonoBehaviour
{
    public GameObject bulletTurret;
    public GameObject laserTurret;
    public GameObject rocketTurret;
    public GameObject mortarTurret;
    // 다른 타워 프리팹도 추가

    public Transform turretContainer;

    private void Start()
    {
        TurretPoolManager.Instance.CreatePool(bulletTurret.GetComponent<BulletTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(laserTurret.GetComponent<LaserTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(rocketTurret.GetComponent<RocketTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(mortarTurret.GetComponent<MortarTurret>(), 12, turretContainer);
        // 다른 발사체 풀도 생성
    }
}

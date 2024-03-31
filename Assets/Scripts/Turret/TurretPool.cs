using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPool : MonoBehaviour
{
    public GameObject bulletTurret;
    public GameObject laserTurret;
    public GameObject rocketTurret;
    // �ٸ� Ÿ�� �����յ� �߰�

    public Transform turretContainer;

    private void Start()
    {
        TurretPoolManager.Instance.CreatePool(bulletTurret.GetComponent<BulletTurret>(), 10, turretContainer);
        // �ٸ� �߻�ü Ǯ�� ����
    }
}

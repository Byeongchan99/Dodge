using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPool : MonoBehaviour
{
    public GameObject bulletTurret; // �Ѿ� �ͷ�
    public GameObject laserTurret; // ������ �ͷ�
    public GameObject rocketTurret; // ���� �ͷ�
    public GameObject mortarTurret; // �ڰ��� �ͷ�
    // �ٸ� Ÿ�� �����յ� �߰�

    public Transform turretContainer;

    private void Start()
    {
        TurretPoolManager.Instance.CreatePool(bulletTurret.GetComponent<BulletTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(laserTurret.GetComponent<LaserTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(rocketTurret.GetComponent<RocketTurret>(), 12, turretContainer);
        TurretPoolManager.Instance.CreatePool(mortarTurret.GetComponent<MortarTurret>(), 12, turretContainer);
        // �ٸ� �߻�ü Ǯ�� ����
    }
}

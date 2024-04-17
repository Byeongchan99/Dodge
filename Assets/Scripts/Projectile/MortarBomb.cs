using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBomb : BaseProjectile
{
    public Transform target;  // ��ǥ ����
    [SerializeField] private float firingAngle = 45.0f;  // �߻� ����, �⺻�� 45��

    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
    }

    /// <summary> ������ ���� ��� �� ���� </summary>
    protected override void Move()
    {
        Vector3 targetDir = target.position - transform.position;  // ��ǥ ���� ����
        float x = targetDir.x;  // ���� �Ÿ�
        float y = targetDir.y;  // ���� �Ÿ�

        float gravity = Physics2D.gravity.magnitude;  // �߷°��ӵ�
        float radianAngle = firingAngle * Mathf.Deg2Rad;  // �߻� ������ �������� ��ȯ

        float projectileVelocity = Mathf.Sqrt((gravity * x * x) / (2 * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle) * (y - Mathf.Tan(radianAngle) * x)));  // �ʱ� �ӵ� ���

        Vector2 velocity = new Vector2(projectileVelocity * Mathf.Cos(radianAngle), projectileVelocity * Mathf.Sin(radianAngle));  // X, Y ���� �ʱ� �ӵ�

        SetDirection(velocity);  // ���� �ӵ��� ���� ����
    }
}

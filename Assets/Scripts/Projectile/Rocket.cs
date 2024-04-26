using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BaseProjectile
{
    public Transform target;  // ��ǥ (�÷��̾�)
    public float rotateSpeed = 10f;

    /// <summary> ���� ���� �������� </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime;

        // _lifeTime ���� ����
        StartCoroutine(LifecycleCoroutine());
    }

    /// <summary> ���� ������ ���� </summary>
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)(target.position - transform.position);
            direction.Normalize();

            // ���� ȸ�� ����
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            // -0.4f ~ 0.4f ������ ������ ����
            rotateAmount = Mathf.Clamp(rotateAmount, -0.4f, 0.4f);

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * _speed;
        }
    }
}

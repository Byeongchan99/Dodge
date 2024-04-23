using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BaseProjectile
{
    public Transform target;  // 목표 (플레이어)
    public float rotateSpeed = 10f;

    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime;
        Invoke("DestroyProjectile", _lifeTime);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)(target.position - transform.position);
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            rotateAmount = Mathf.Clamp(rotateAmount, -0.4f, 0.4f);

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * _speed;  // 'transform.forward' 대신 'transform.up' 사용
        }
    }
}

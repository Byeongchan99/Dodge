using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BaseProjectile
{
    public Transform target;  // 목표 (플레이어)
    public float rotateSpeed = 10f;

    /// <summary> 로켓 스탯 가져오기 </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime;

        // _lifeTime 동안 유지
        StartCoroutine(LifecycleCoroutine());
    }

    /// <summary> 로켓 움직임 구현 </summary>
    void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 direction = (Vector2)(target.position - transform.position);
            direction.Normalize();

            // 로켓 회전 정도
            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            // -0.4f ~ 0.4f 사이의 값으로 제한
            rotateAmount = Mathf.Clamp(rotateAmount, -0.4f, 0.4f);

            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * _speed;
        }
    }
}

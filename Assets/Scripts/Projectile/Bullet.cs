using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
    }

    protected override void Move()
    {
        // �߻�ü Ư���� ������ ����
        //rb.velocity = transform.right * speed;
        base.Move();
    }
}

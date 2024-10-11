using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    /// <summary> �Ѿ� ���� �������� </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
    }

    /// <summary> �Ѿ� ������ ���� </summary>
    protected override void Move()
    {
        // rb.velocity = moveDirection.normalized * _speed;
        base.Move();
    }
}

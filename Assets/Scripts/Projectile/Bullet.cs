using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    /// <summary> 총알 스탯 가져오기 </summary>
    protected override void OnEnable()
    {
        base.OnEnable();
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed;
    }

    /// <summary> 총알 움직임 구현 </summary>
    protected override void Move()
    {
        // rb.velocity = moveDirection.normalized * _speed;
        base.Move();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile
{
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed;
    }

    protected override void Move()
    {
        StartCoroutine(ActiveLaser());
    }

    /// <summary> _lifeTime 동안 레이저 유지 </summary>
    private IEnumerator ActiveLaser()
    {
        yield return new WaitForSeconds(_lifeTime);
        DestroyProjectile();
    }

    /// <summary> 충돌 검사 </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            PlayerStat.Instance.TakeDamage();
        }
    }
}

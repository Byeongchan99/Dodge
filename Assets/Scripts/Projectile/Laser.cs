using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseProjectile
{
    /// <summary> 레이저 스탯 가져오기 </summary>
    protected override void OnEnable()
    {
        _speed = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSpeed;
        _lifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime;
        
        // _lifeTime 동안 유지
        StartCoroutine(LifecycleCoroutine());
    }

    /// <summary> 충돌 검사 </summary>
    // baseProjectile의 OnTriggerEnter2D를 오버라이딩하여 EMP에 파괴당하는 기능 제거 및 플레이어와 충돌 시 데미지를 입히는 기능 추가
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

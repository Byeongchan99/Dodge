using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBombEffect : BaseEffect
{
    PolygonCollider2D effectCollider;
    float delayTime;

    private void Awake()
    {
        effectCollider = GetComponent<PolygonCollider2D>();
        effectCollider.enabled = false;
        delayTime = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;
    }

    /// <summary> 초기화 /// </summary>
    private void OnEnable()
    {
        StartCoroutine(ActiveMortarBombEffect());
    }

    // MortarBomb이 터질 때 콜라이더 활성화 그 후 이펙트 비활성화
    IEnumerator ActiveMortarBombEffect()
    {
        yield return new WaitForSeconds(delayTime);  // 포탄이 터질 때까지 대기
        effectCollider.enabled = true;
        yield return new WaitForSeconds(0.1f); // 콜라이더 활성화 후 0.1초 대기
        effectCollider.enabled = false;
        DestroyEffect();
    }

    /// <summary> 충돌 검사 </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            PlayerStat.Instance.TakeDamage();
        }
    }
}

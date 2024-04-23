using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackAreaEffect : BaseEffect
{
    /// <summary> 초기화 /// </summary>
    private void OnEnable()
    {
        StartCoroutine(ActiveLaserAttackAreaEffect());
    }

    // MortarBomb이 터질 때 콜라이더 활성화 그 후 이펙트 비활성화
    IEnumerator ActiveLaserAttackAreaEffect()
    {
        yield return new WaitForSeconds(1f); // 1초 대기 
        DestroyEffect();
    }
}

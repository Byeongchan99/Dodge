using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBombEffect : ItemEffect
{
    private BaseEffect EMPEffect;
    public EMPBombEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        Debug.Log("EMP 아이템 효과 적용");
        EMPEffect = EffectPoolManager.Instance.Get("EMPEffect");
        EMPEffect.transform.position = PlayerStat.Instance.currentPosition.position;
        EMPEffect.gameObject.SetActive(true);
    }

    public override void RemoveEffect()
    {
        Debug.Log("EMP 아이템 효과 종료");
        if (EMPEffect != null)
        {
            EffectPoolManager.Instance.Return("EMPEffect", EMPEffect);  // 캐시된 효과 인스턴스를 풀로 반환
            EMPEffect = null;  // 참조 해제
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    public InvincibilityEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        // 무적 적용
        Debug.Log("무적 아이템 효과 적용");
        PlayerStat.Instance.isInvincibility = true;
    }

    public override void RemoveEffect()
    {
        // 무적 해제
        Debug.Log("무적 아이템 효과 종료");
        PlayerStat.Instance.isInvincibility = false;
    }
}

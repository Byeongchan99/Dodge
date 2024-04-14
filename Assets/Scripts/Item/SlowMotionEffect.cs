using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : ItemEffect
{
    private float _slowDownFactor = 0.05f; // 시간을 느리게 하는 요소

    public SlowMotionEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        Debug.Log("슬로 모션 아이템 효과 적용");
        GameManager.Instance.StartSlowEffect(_duration); // 슬로우 모션 효과 적용

        if (!GameManager.Instance.isItemSlowMotion) // 아이템에 의한 슬로우 모션 효과가 적용 중이 아니라면
        {
            PlayerStat.Instance.currentMoveSpeed *= 200; // 속도를 증가
            GameManager.Instance.isItemSlowMotion = true;
        }
        GameManager.Instance.slowMotionItemCount++;
    }

    public override void RemoveEffect()
    {
        Debug.Log("슬로 모션 아이템 효과 종료");

        // 아이템에 의한 슬로우 모션 효과가 적용 중일 때
        if (GameManager.Instance.isItemSlowMotion && GameManager.Instance.slowMotionItemCount == 1)
        {
            PlayerStat.Instance.currentMoveSpeed /= 200;  // 원래 속도로 복구
            GameManager.Instance.isItemSlowMotion = false;
        }
        GameManager.Instance.slowMotionItemCount--;
    }
}

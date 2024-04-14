using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpEffect : ItemEffect
{
    private float _moveSpeedIncrease;

    public MoveSpeedUpEffect(float duration, GameObject target, float moveSpeedIncrease) : base(duration, target)
    {
        this._moveSpeedIncrease = moveSpeedIncrease;
    }

    public override void ApplyEffect()
    {
        // 이동 속도를 증가시킴
        Debug.Log("이동 속도 증가 아이템 효과 적용" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed *= _moveSpeedIncrease;
    }

    public override void RemoveEffect()
    {
        // 이동 속도 감소
        Debug.Log("이동 속도 증가 아이템 효과 종료" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed /= _moveSpeedIncrease;
    }
}

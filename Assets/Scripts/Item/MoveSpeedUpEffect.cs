using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpEffect : ItemEffect
{
    private float _moveSpeedIncrease;
    private GhostEffect ghostEffect;  // 잔상 효과

    public MoveSpeedUpEffect(float duration, GameObject target, float moveSpeedIncrease) : base(duration, target)
    {
        this._moveSpeedIncrease = moveSpeedIncrease;
        // Target 오브젝트에서 GhostEffect 컴포넌트를 가져옴
        this.ghostEffect = target.GetComponent<GhostEffect>();
    }

    public override void ApplyEffect()
    {
        // 이동 속도를 증가시킴
        Debug.Log("이동 속도 증가 아이템 효과 적용" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed *= _moveSpeedIncrease; // 속도 증가
        ghostEffect.moveSpeedUpItemCount++;
        ghostEffect.isMakeGhost = true;  // 잔상 생성 시작
    }

    public override void RemoveEffect()
    {
        // 이동 속도 감소
        Debug.Log("이동 속도 증가 아이템 효과 종료" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed /= _moveSpeedIncrease; // 속도 감소

        if (ghostEffect.moveSpeedUpItemCount == 1)// 잔상 효과가 적용 중이라면
        {
            ghostEffect.isMakeGhost = false;  // 잔상 생성 중단
        }
        ghostEffect.moveSpeedUpItemCount--;
    }
}

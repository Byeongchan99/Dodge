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
        // �̵� �ӵ��� ������Ŵ
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed *= _moveSpeedIncrease;
    }

    public override void RemoveEffect()
    {
        // �̵� �ӵ� ����
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + _moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed /= _moveSpeedIncrease;
    }
}

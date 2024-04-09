using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpEffect : ItemEffect
{
    private float moveSpeedIncrease;

    public MoveSpeedUpEffect(float duration, GameObject target, float moveSpeedIncrease) : base(duration, target)
    {
        this.moveSpeedIncrease = moveSpeedIncrease;
    }

    public override void ApplyEffect()
    {
        // �̵� �ӵ��� ������Ŵ
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed += moveSpeedIncrease;
    }

    public override void RemoveEffect()
    {
        // �̵� �ӵ� ����
        Debug.Log("�̵� �ӵ� ���� ������ ȿ�� ����" + moveSpeedIncrease);
        PlayerStat.Instance.currentMoveSpeed -= moveSpeedIncrease;
    }
}

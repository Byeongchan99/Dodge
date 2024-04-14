using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    public InvincibilityEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        PlayerStat.Instance.isInvincibility = true;
    }

    public override void RemoveEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        PlayerStat.Instance.isInvincibility = false;
    }
}

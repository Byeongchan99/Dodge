using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackAreaEffect : BaseEffect
{
    /// <summary> �ʱ�ȭ /// </summary>
    private void OnEnable()
    {
        StartCoroutine(ActiveLaserAttackAreaEffect());
    }

    // MortarBomb�� ���� �� �ݶ��̴� Ȱ��ȭ �� �� ����Ʈ ��Ȱ��ȭ
    IEnumerator ActiveLaserAttackAreaEffect()
    {
        yield return new WaitForSeconds(1f); // 1�� ��� 
        DestroyEffect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public GameObject EMPPrefab;
    public GameObject laserAttackAreaEffectPrefab;
    public GameObject mortarBombEffectPrefab;
    // �ٸ� ����Ʈ �����յ� �߰�

    public Transform effectContainer;

    private void Start()
    {
        EffectPoolManager.Instance.CreatePool(EMPPrefab.GetComponent<EMPEffect>(), 10, effectContainer);
        EffectPoolManager.Instance.CreatePool(laserAttackAreaEffectPrefab.GetComponent<LaserAttackAreaEffect>(), 10, effectContainer);
        EffectPoolManager.Instance.CreatePool(mortarBombEffectPrefab.GetComponent<MortarBombEffect>(), 10, effectContainer);
        // �ٸ� ����Ʈ Ǯ�� ����
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public GameObject EMPPrefab;
    public GameObject MortarBombEffectPrefab;
    // �ٸ� ����Ʈ �����յ� �߰�

    public Transform effectContainer;

    private void Start()
    {
        EffectPoolManager.Instance.CreatePool(EMPPrefab.GetComponent<EMPEffect>(), 10, effectContainer);
        EffectPoolManager.Instance.CreatePool(MortarBombEffectPrefab.GetComponent<MortarBombEffect>(), 10, effectContainer);
        // �ٸ� ����Ʈ Ǯ�� ����
    }
}

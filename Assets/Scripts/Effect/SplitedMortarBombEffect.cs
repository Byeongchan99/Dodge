using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedMortarBombEffect : MortarBombEffect
{
    protected override void OnEnable()
    {
        _delayTime = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed / 2;

        StartCoroutine(ActiveMortarBombEffect());
    }
}

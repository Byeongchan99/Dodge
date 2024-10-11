using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedMortarBomb : MortarBomb
{
    /// <summary> �ڰ���ź ���� ���� </summary>
    protected override void OnEnable()
    {
        isDestroyed = false;
    }

    public void setMortarBomb(float duration, float height)
    {
        _flightDuration = duration;
        _hoverHeight = height;
    }
}

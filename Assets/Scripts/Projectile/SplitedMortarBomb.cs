using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedMortarBomb : MortarBomb
{
    /// <summary> �ڰ���ź ���� ���� </summary>
    protected override void OnEnable()
    {
        
    }

    public void setMortarBomb(float duration, float height)
    {
        flightDuration = duration;
        hoverHeight = height;
    }
}

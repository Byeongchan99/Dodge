using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitedMortarBomb : MortarBomb
{
    /// <summary> ¹Ú°ÝÆ÷Åº ½ºÅÈ ¼³Á¤ </summary>
    protected override void OnEnable()
    {
        
    }

    public void setMortarBomb(float duration, float height)
    {
        _flightDuration = duration;
        _hoverHeight = height;
    }
}

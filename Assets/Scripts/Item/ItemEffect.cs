using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect
{
    protected float _duration;
    protected GameObject target;

    public ItemEffect(float duration, GameObject target)
    {
        this._duration = duration;
        this.target = target;
    }

    public float GetDuration()
    {
        return _duration;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}

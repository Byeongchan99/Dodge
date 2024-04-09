using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect
{
    protected float duration;
    protected GameObject target;

    public ItemEffect(float duration, GameObject target)
    {
        this.duration = duration;
        this.target = target;
    }

    public float GetDuration()
    {
        return duration;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    /// <summary> ÀÌÆåÆ® ÆÄ±« </summary>
    public void DestroyEffect()
    {
        //Debug.Log("ÀÌÆåÆ® Ç®¿¡ ¹ÝÈ¯");
        EffectPoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }
}

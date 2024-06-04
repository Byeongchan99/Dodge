using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary> ÀÌÆåÆ® ÆÄ±« </summary>
    public void DestroyEffect()
    {
        //Debug.Log("ÀÌÆåÆ® Ç®¿¡ ¹ÝÈ¯");
        EffectPoolManager.Instance.Return(this.GetType().Name, this);
        StopAllCoroutines();
    }
}

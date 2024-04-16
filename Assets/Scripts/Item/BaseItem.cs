using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected ItemEffect itemEffect;
    [SerializeField] protected float itemRemainTime = 10f;

    private void OnEnable()
    {
        InitItem();
        Invoke("DestroyItem", itemRemainTime); // 10초 후 DestroyItem 메소드 호출
    }

    /// <summary> 아이템 초기화 </summary>
    protected virtual void InitItem()
    {
        // 아이템 효과 초기화    
    }

    /// <summary> 아이템 파괴 </summary>
    protected void DestroyItem()
    {
        CancelInvoke("DestroyItem"); // 중복 호출 방지
        ItemPoolManager.Instance.Return(this.GetType().Name, this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 충돌 감지
        if (collision.gameObject.CompareTag("Player"))
        {
            // 아이템 획득 처리
            Debug.Log("아이템 획득");
            PlayerStat.Instance.ApplyItemEffect(itemEffect);
            DestroyItem();
        }
    }
}

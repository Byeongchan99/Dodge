using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    protected ItemEffect itemEffect;

    private void OnEnable()
    {
        InitItem();
    }

    /// <summary> 아이템 초기화 </summary>
    protected virtual void InitItem()
    {
        // 아이템 효과 초기화
    }

    /// <summary> 아이템 파괴 </summary>
    protected void DestroyItem()
    {
        // 나중에 오브젝트 풀로 변경
        Destroy(gameObject);
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

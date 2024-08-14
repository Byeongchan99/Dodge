using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    /// <summary> 각 아이템 효과 </summary>
    protected ItemEffect itemEffect;

    /// <summary> 아이템이 필드에 남아있는 시간 </summary>
    [SerializeField] protected float itemRemainTime = 20f;

    /// <summary> Fade Effect 참조 </summary>
    private FadeEffect fadeEffect;

    private void Awake()
    {
        fadeEffect = GetComponent<FadeEffect>();
    }

    private void OnEnable()
    {
        InitItem();
        fadeEffect.StartFadeIn(0.5f);
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
        fadeEffect.StartFadeOut(0.5f);
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

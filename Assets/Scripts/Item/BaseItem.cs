using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    /****************************************************************************
                                protected Fields
    ****************************************************************************/
    /// <summary> 각 아이템 효과 </summary>
    protected ItemEffect itemEffect;

    /// <summary> Fade Effect 참조 </summary>
    private FadeEffect fadeEffect;

    /// <summary> 아이템이 필드에 남아있는 시간 </summary>
    [SerializeField] protected float _itemRemainTime = 20f;

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        fadeEffect = GetComponent<FadeEffect>();
    }

    private void OnEnable()
    {
        InitItem();
        fadeEffect.StartFadeIn(0.5f, 0.1f);
        StartCoroutine(ItemDisableAfterRemainTime(_itemRemainTime)); // itemRemainTime 이후 아이템 비활성화
    }

    /****************************************************************************
                                private Methods
    ****************************************************************************/
    /// <summary> 아이템 초기화 </summary>
    protected virtual void InitItem()
    {
        // 아이템 효과 초기화    
    }

    /// <summary> 아이템이 필드에 남아있는 시간 이후 아이템 비활성화 시작 </summary>
    private IEnumerator ItemDisableAfterRemainTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StartDisableItem());
    }

    /// <summary> 아이템 비활성화 시작 </summary>
    IEnumerator StartDisableItem()
    {
        fadeEffect.StartFadeOut(0.5f, 0.1f);
        yield return new WaitForSeconds(1.5f);
        DisableItem();
    }

    /// <summary> 아이템 비활성화 </summary>
    protected void DisableItem()
    {     
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
            StartCoroutine(StartDisableItem());
        }
    }
}

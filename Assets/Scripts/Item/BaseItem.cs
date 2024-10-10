using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    /****************************************************************************
                                protected Fields
    ****************************************************************************/
    /// <summary> �������� BoxCollider2D ���� </summary>
    private BoxCollider2D boxCollider2D;

    /// <summary> �� ������ ȿ�� </summary>
    protected ItemEffect itemEffect;

    /// <summary> Fade Effect ���� </summary>
    private FadeEffect fadeEffect;

    /// <summary> ������ ȿ���� </summary>
    public AudioClip itemEffectAudioClip;

    /// <summary> �������� �ʵ忡 �����ִ� �ð� </summary>
    [SerializeField] protected float _itemRemainTime = 20f;

    [SerializeField] protected AudioClip enableAudioClip;
    [SerializeField] protected AudioClip disableAudioClip;

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        fadeEffect = GetComponent<FadeEffect>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        itemEffect = GetComponent<ItemEffect>();
    }

    private void OnEnable()
    {
        InitItem();
        AudioManager.instance.sfxAudioSource.PlayOneShot(enableAudioClip);
        fadeEffect.StartFadeIn(0.5f, 0.1f);
        StartCoroutine(ItemDisableAfterRemainTime(_itemRemainTime)); // itemRemainTime ���� ������ ��Ȱ��ȭ
    }

    /****************************************************************************
                                private Methods
    ****************************************************************************/
    /// <summary> ������ �ʱ�ȭ </summary>
    protected virtual void InitItem()
    {
        // ������ ȿ�� �ʱ�ȭ
        boxCollider2D.enabled = true;
    }

    /// <summary> �������� �ʵ忡 �����ִ� �ð� ���� ������ ��Ȱ��ȭ ���� </summary>
    private IEnumerator ItemDisableAfterRemainTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(StartDisableItem());
    }

    /// <summary> ������ ��Ȱ��ȭ ���� </summary>
    IEnumerator StartDisableItem()
    {
        AudioManager.instance.sfxAudioSource.PlayOneShot(disableAudioClip);
        fadeEffect.StartFadeOut(0.5f, 0.1f);
        yield return new WaitForSeconds(1.5f);
        DisableItem();
    }

    /// <summary> ������ ��Ȱ��ȭ </summary>
    public void DisableItem()
    {
        ItemPoolManager.Instance.Return(this.GetType().Name, this);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ �浹 ����
        if (collision.gameObject.CompareTag("Player"))
        {
            // ������ ȹ�� ó��
            Debug.Log("������ ȹ��");
            boxCollider2D.enabled = false; // ������ �ߺ� ȹ�� ó�� ����
            PlayerStat.Instance.ApplyItemEffect(itemEffect);
            StartCoroutine(StartDisableItem());
        }
    }
}

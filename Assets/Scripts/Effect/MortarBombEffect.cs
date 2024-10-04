using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBombEffect : BaseEffect
{
    PolygonCollider2D effectCollider;
    protected float _delayTime;
    public AudioClip AudioClip; // 폭발음

    private void Awake()
    {
        effectCollider = GetComponent<PolygonCollider2D>();
        effectCollider.enabled = false;
    }

    /// <summary> 초기화 /// </summary>
    protected virtual void OnEnable()
    {
        _delayTime = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed;

        StartCoroutine(ActiveMortarBombEffect());
    }

    // MortarBomb이 터질 때 콜라이더 활성화 그 후 이펙트 비활성화
    protected IEnumerator ActiveMortarBombEffect()
    {
        yield return new WaitForSeconds(_delayTime);  // 포탄이 터질 때까지 대기
        effectCollider.enabled = true;
        AudioManager.instance.sfxAudioSource.PlayOneShot(AudioClip);
        yield return new WaitForSeconds(0.1f); // 콜라이더 활성화 후 0.1초 대기
        effectCollider.enabled = false;
        DestroyEffect();
    }

    /// <summary> 충돌 검사 </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌");
            PlayerStat.Instance.TakeDamage();
        }
    }

    public void DestroyByEMP()
    {
        Debug.Log("박격포탄이 EMP에 의해 소멸");
        DestroyEffect();
    }
}

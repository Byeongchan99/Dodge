using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionEffect : ItemEffect
{
    public override void ApplyEffect()
    {
        Debug.Log("슬로 모션 아이템 효과 적용");
        GameManager.Instance.StartSlowEffect(_duration); // 슬로우 모션 효과 적용

        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // 아이템 효과음 재생

        if (!GameManager.Instance.isItemSlowMotion) // 아이템에 의한 슬로우 모션 효과가 적용 중이 아니라면
        {
            PlayerStat.Instance.currentMoveSpeed *= 250; // 슬로우 효과에 맞게 속도 증가
            GameManager.Instance.isItemSlowMotion = true;
        }
        GameManager.Instance.slowMotionItemCount++;
    }

    public override void RemoveEffect()
    {
        Debug.Log("슬로 모션 아이템 효과 종료");

        // 아이템에 의한 슬로우 모션 효과가 적용 중일 때
        if (GameManager.Instance.isItemSlowMotion && GameManager.Instance.slowMotionItemCount == 1)
        {
            PlayerStat.Instance.currentMoveSpeed /= 250;  // 원래 속도로 복구
            GameManager.Instance.isItemSlowMotion = false;
        }
        GameManager.Instance.slowMotionItemCount--;
    }
}

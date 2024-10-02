using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreEffect : ItemEffect
{
    public override void ApplyEffect()
    {
        Debug.Log("점수 획득 아이템 효과 적용");
        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // 아이템 효과음 재생

        ScoreManager.Instance.AddScore(10f); // 점수 획득
    }

    public override void RemoveEffect()
    {
        Debug.Log("점수 획득 아이템 효과 종료"); // 별 의미없음
    }
}

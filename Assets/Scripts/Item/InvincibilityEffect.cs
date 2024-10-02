using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    private Coroutine colorChangeCoroutine;  // 색상 변경 코루틴을 저장할 변수

    public override void ApplyEffect()
    {
        // 무적 적용
        Debug.Log("무적 아이템 효과 적용");
        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // 아이템 효과음 재생

        PlayerStat.Instance.StartInvincibility(_duration);  // 무적 상태 시작
        colorChangeCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(ChangeColor());
    }

    public override void RemoveEffect()
    {
        // 무적 해제
        Debug.Log("무적 아이템 효과 종료");
        if (colorChangeCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(colorChangeCoroutine);
        }

        // 색상 원래대로 복구
        RestoreOriginalColors();
    }

    private void RestoreOriginalColors()
    {
        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();
        // 플레이어 색상 초기화
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.color = Color.white;
            //Debug.Log("플레이어 색상 복구됨");
        }

        // 왕관 오브젝트의 SpriteRenderer 복구
        Transform childTransform = target.transform.Find("Crown");  // 왕관 오브젝트 찾기
        if (childTransform != null)
        {
            SpriteRenderer crownSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
            if (crownSpriteRenderer != null)
            {
                crownSpriteRenderer.color = Color.white;
                //Debug.Log("왕관 색상 복구됨");
            }
        }
    }

    // 색상 변경 코루틴
    // 0.1초 마다 색상을 변경하여 무지개 색으로 깜빡이는 효과 구현
    private IEnumerator ChangeColor()
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int colorIndex = 0;

        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();

        Transform childTransform = target.transform.Find("Crown");  // 자식 오브젝트 Crown의 SpriteRenderer 가져오기
        SpriteRenderer crownSpriteRenderer = null;

        if (childTransform != null)
        {
            crownSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        }

        while (true)
        {
            if (playerSpriteRenderer != null)
            {
                //Debug.Log("플레이어 색상 변경");
                playerSpriteRenderer.color = colors[colorIndex];
            }

            if (crownSpriteRenderer != null)
            {
                //Debug.Log("왕관 색상 변경");
                crownSpriteRenderer.color = colors[colorIndex];
            }

            colorIndex = (colorIndex + 1) % colors.Length;  // 다음 색상 인덱스로 업데이트
            yield return new WaitForSeconds(0.1f);  // 0.1초 마다 색상 변경
        }
    }
}

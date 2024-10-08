using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void StartFadeIn(float duration, float distance)
    {
        StartCoroutine(FadeIn(duration, distance));
    }

    public void StartFadeOut(float duration, float distance)
    {
        StartCoroutine(FadeOut(duration, distance));
    }

    // 스프라이트 초기화
    public void resetSprite()
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 1f;
            spriteRenderer.color = color;
        }
    }

    // 위에서 아래로 distance만큼 이동하면서 duration동안 페이드 인
    private IEnumerator FadeIn(float duration, float distance)
    {
        // 초기 위치에서 살짝 위로 이동
        Vector3 originalPosition = transform.localPosition;
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + distance, originalPosition.z);

        // 위치를 원래대로 되돌리면서 페이드 인
        foreach (var spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; // 초기 alpha를 0으로 설정
            spriteRenderer.color = color;
        }

        // 페이드 인과 함께 아래로 이동 (로컬 좌표로)
        transform.DOLocalMoveY(originalPosition.y, duration).SetEase(Ease.InOutQuad);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(1f, duration).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(duration);
    }

    // 아래에서 위로 distance만큼 이동하면서 duration동안 페이드 아웃
    private IEnumerator FadeOut(float duration, float distance)
    {
        // 페이드 아웃과 함께 위로 이동
        Vector3 originalPosition = transform.localPosition;
        transform.DOLocalMoveY(originalPosition.y + distance, duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(0f, duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        }

        yield return new WaitForSeconds(duration);
    }
}

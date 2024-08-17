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

    private IEnumerator FadeIn(float duration, float distance)
    {
        // �ʱ� ��ġ���� ��¦ ���� �̵�
        Vector3 originalPosition = transform.localPosition;
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + distance, originalPosition.z);

        // ��ġ�� ������� �ǵ����鼭 ���̵� ��
        foreach (var spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; // �ʱ� alpha�� 0���� ����
            spriteRenderer.color = color;
        }

        // ���̵� �ΰ� �Բ� �Ʒ��� �̵� (���� ��ǥ��)
        transform.DOLocalMoveY(originalPosition.y, duration).SetEase(Ease.InOutQuad);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(1f, duration).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(duration);
    }

    private IEnumerator FadeOut(float duration, float distance)
    {
        // ���̵� �ƿ��� �Բ� ���� �̵�
        Vector3 originalPosition = transform.localPosition;
        transform.DOLocalMoveY(originalPosition.y + distance, duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(0f, duration).SetEase(Ease.InOutQuad).SetUpdate(true);
        }

        yield return new WaitForSeconds(duration);
    }
}

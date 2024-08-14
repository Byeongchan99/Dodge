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

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeIn(duration));
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeIn(float duration)
    {
        // �ʱ� ��ġ���� ��¦ ���� �̵�
        Vector3 originalPosition = transform.localPosition;
        transform.localPosition = new Vector3(originalPosition.x, originalPosition.y + 0.2f, originalPosition.z);

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

    private IEnumerator FadeOut(float duration)
    {
        // ���̵� �ƿ��� �Բ� ���� �̵�
        Vector3 originalPosition = transform.localPosition;
        transform.DOLocalMoveY(originalPosition.y + 0.2f, duration).SetEase(Ease.InOutQuad);
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(0f, duration).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(duration);
    }
}

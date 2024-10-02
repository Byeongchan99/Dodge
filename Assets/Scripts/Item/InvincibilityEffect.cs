using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    private Coroutine colorChangeCoroutine;  // ���� ���� �ڷ�ƾ�� ������ ����

    public override void ApplyEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        AudioManager.instance.sfxAudioSource.PlayOneShot(audioClip); // ������ ȿ���� ���

        PlayerStat.Instance.StartInvincibility(_duration);  // ���� ���� ����
        colorChangeCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(ChangeColor());
    }

    public override void RemoveEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        if (colorChangeCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(colorChangeCoroutine);
        }

        // ���� ������� ����
        RestoreOriginalColors();
    }

    private void RestoreOriginalColors()
    {
        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();
        // �÷��̾� ���� �ʱ�ȭ
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.color = Color.white;
            //Debug.Log("�÷��̾� ���� ������");
        }

        // �հ� ������Ʈ�� SpriteRenderer ����
        Transform childTransform = target.transform.Find("Crown");  // �հ� ������Ʈ ã��
        if (childTransform != null)
        {
            SpriteRenderer crownSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
            if (crownSpriteRenderer != null)
            {
                crownSpriteRenderer.color = Color.white;
                //Debug.Log("�հ� ���� ������");
            }
        }
    }

    // ���� ���� �ڷ�ƾ
    // 0.1�� ���� ������ �����Ͽ� ������ ������ �����̴� ȿ�� ����
    private IEnumerator ChangeColor()
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int colorIndex = 0;

        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();

        Transform childTransform = target.transform.Find("Crown");  // �ڽ� ������Ʈ Crown�� SpriteRenderer ��������
        SpriteRenderer crownSpriteRenderer = null;

        if (childTransform != null)
        {
            crownSpriteRenderer = childTransform.GetComponent<SpriteRenderer>();
        }

        while (true)
        {
            if (playerSpriteRenderer != null)
            {
                //Debug.Log("�÷��̾� ���� ����");
                playerSpriteRenderer.color = colors[colorIndex];
            }

            if (crownSpriteRenderer != null)
            {
                //Debug.Log("�հ� ���� ����");
                crownSpriteRenderer.color = colors[colorIndex];
            }

            colorIndex = (colorIndex + 1) % colors.Length;  // ���� ���� �ε����� ������Ʈ
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���� ���� ����
        }
    }
}

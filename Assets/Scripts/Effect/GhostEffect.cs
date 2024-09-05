using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : BaseEffect
{
    private float _ghostDelayTime;
    private float _ghostDelay = 0.1f; // �ܻ� ���� �ֱ�

    public bool isMakeGhost; // �ܻ� ���� ����
    public int moveSpeedUpItemCount; // ȹ���� �̵� �ӵ� ���� ������ ����

    public GameObject ghost; // �ܻ� ������
    public GameObject crown; // �հ� ������Ʈ
    public Transform ghostEffectContainer;

    void Start()
    {
        this._ghostDelayTime = this._ghostDelay;
        moveSpeedUpItemCount = 0;
    }

    void FixedUpdate()
    {
        if (this.isMakeGhost)
        {
            if (this._ghostDelayTime > 0)
            {
                this._ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                CreateGhost();
                this._ghostDelayTime = this._ghostDelay;
            }
        }
    }

    /// <summary> �ܻ� ���� </summary>
    void CreateGhost()
    {
        GameObject currentGhost = Instantiate(this.ghost, this.transform.position, this.transform.rotation, ghostEffectContainer);
        SpriteRenderer playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
        SpriteRenderer ghostSpriteRenderer = currentGhost.GetComponent<SpriteRenderer>();

        // �÷��̾� ��������Ʈ�� �ܻ� ����
        ghostSpriteRenderer.sprite = playerSpriteRenderer.sprite;
        currentGhost.transform.localScale = this.transform.localScale;
        if (playerSpriteRenderer.flipX)
        {
            ghostSpriteRenderer.flipX = true;
        }

        // �ܻ��� ������ �÷��̾� ���� ������ 60% ������ ����
        Color newColor = playerSpriteRenderer.color;
        newColor.a = 0.6f;
        ghostSpriteRenderer.color = newColor;

        // �հ����� ��Ʈ ȿ�� ����
        if (crown != null && crown.activeSelf)
        {
            GameObject crownGhost = new GameObject("CrownGhost");
            crownGhost.transform.SetParent(currentGhost.transform);
            crownGhost.transform.localPosition = crown.transform.localPosition;
            crownGhost.transform.localRotation = crown.transform.localRotation;
            crownGhost.transform.localScale = crown.transform.localScale;

            SpriteRenderer crownSpriteRenderer = crown.GetComponent<SpriteRenderer>();
            SpriteRenderer crownGhostSpriteRenderer = crownGhost.AddComponent<SpriteRenderer>();

            crownGhostSpriteRenderer.sprite = crownSpriteRenderer.sprite;
            crownGhostSpriteRenderer.flipX = crownSpriteRenderer.flipX;
            crownGhostSpriteRenderer.color = newColor;
        }

        StartCoroutine(FadeOutGhost(currentGhost));
    }

    /// <summary> �ܻ� ���̵� �ƿ� </summary>
    IEnumerator FadeOutGhost(GameObject ghost)
    {
        SpriteRenderer[] ghostSprites = ghost.GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1.0f;  // ���̵� �ƿ� �ð�
        float fadeRate = 1.0f / fadeDuration;  // ���̵� �ƿ� ����
        float alpha = ghostSprites[0].color.a;  // �ʱ� ���İ�

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeRate;  // ���İ� ����
            foreach (var sprite in ghostSprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }
            yield return null;
        }
        Destroy(ghost);
    }
}

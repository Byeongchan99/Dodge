using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : BaseEffect
{
    private float _ghostDelayTime;
    private float _ghostDelay = 0.1f; // 잔상 생성 주기

    public bool isMakeGhost; // 잔상 생성 여부
    public int moveSpeedUpItemCount; // 획득한 이동 속도 증가 아이템 개수

    public GameObject ghost; // 잔상 프리팹
    public GameObject crown; // 왕관 오브젝트
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

    /// <summary> 잔상 생성 </summary>
    void CreateGhost()
    {
        GameObject currentGhost = Instantiate(this.ghost, this.transform.position, this.transform.rotation, ghostEffectContainer);
        SpriteRenderer playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
        SpriteRenderer ghostSpriteRenderer = currentGhost.GetComponent<SpriteRenderer>();

        // 플레이어 스프라이트를 잔상에 적용
        ghostSpriteRenderer.sprite = playerSpriteRenderer.sprite;
        currentGhost.transform.localScale = this.transform.localScale;
        if (playerSpriteRenderer.flipX)
        {
            ghostSpriteRenderer.flipX = true;
        }

        // 잔상의 색상을 플레이어 현재 색상의 60% 투명도로 적용
        Color newColor = playerSpriteRenderer.color;
        newColor.a = 0.6f;
        ghostSpriteRenderer.color = newColor;

        // 왕관에도 고스트 효과 적용
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

    /// <summary> 잔상 페이드 아웃 </summary>
    IEnumerator FadeOutGhost(GameObject ghost)
    {
        SpriteRenderer[] ghostSprites = ghost.GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1.0f;  // 페이드 아웃 시간
        float fadeRate = 1.0f / fadeDuration;  // 페이드 아웃 비율
        float alpha = ghostSprites[0].color.a;  // 초기 알파값

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeRate;  // 알파값 감소
            foreach (var sprite in ghostSprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }
            yield return null;
        }
        Destroy(ghost);
    }
}

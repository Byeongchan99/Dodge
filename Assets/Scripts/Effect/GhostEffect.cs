using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : BaseEffect
{
    public GameObject ghost; // ÀÜ»ó ÇÁ¸®ÆÕ
    public GameObject crown; // ¿Õ°ü ¿ÀºêÁ§Æ®
    private float _ghostDelayTime;
    public float ghostDelay = 0.1f; // ÀÜ»ó »ý¼º ÁÖ±â
    public bool isMakeGhost; // ÀÜ»ó »ý¼º ¿©ºÎ
    public int moveSpeedUpItemCount; // È¹µæÇÑ ÀÌµ¿ ¼Óµµ Áõ°¡ ¾ÆÀÌÅÛ °³¼ö

    public Transform ghostEffectContainer;

    void Start()
    {
        this._ghostDelayTime = this.ghostDelay;
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
                this._ghostDelayTime = this.ghostDelay;
            }
        }
    }

    void CreateGhost()
    {
        GameObject currentGhost = Instantiate(this.ghost, this.transform.position, this.transform.rotation, ghostEffectContainer);
        SpriteRenderer playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
        SpriteRenderer ghostSpriteRenderer = currentGhost.GetComponent<SpriteRenderer>();

        // Copy player's current sprite and scale
        ghostSpriteRenderer.sprite = playerSpriteRenderer.sprite;
        currentGhost.transform.localScale = this.transform.localScale;
        if (playerSpriteRenderer.flipX)
        {
            ghostSpriteRenderer.flipX = true;
        }

        // Apply the current color of the player to the ghost with reduced opacity
        Color newColor = playerSpriteRenderer.color;
        newColor.a = 0.6f;  // Reduce opacity to 50%
        ghostSpriteRenderer.color = newColor;

        // Handle crown ghost
        if (crown != null)
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
            crownGhostSpriteRenderer.color = newColor; // Apply the same color with reduced opacity
        }

        StartCoroutine(FadeOutGhost(currentGhost));
    }

    IEnumerator FadeOutGhost(GameObject ghost)
    {
        SpriteRenderer[] ghostSprites = ghost.GetComponentsInChildren<SpriteRenderer>();
        float fadeDuration = 1.0f;  // Fade duration in seconds
        float fadeRate = 1.0f / fadeDuration;  // Rate of fade
        float alpha = ghostSprites[0].color.a;  // Initial alpha

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeRate;  // Reduce alpha based on time and rate
            foreach (var sprite in ghostSprites)
            {
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
            }
            yield return null;
        }
        Destroy(ghost);  // Destroy the ghost after fading out
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : BaseEffect
{
    public GameObject ghost; // ÀÜ»ó ÇÁ¸®ÆÕ
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

        // Apply the current color of the player to the ghost with reduced opacity
        Color newColor = playerSpriteRenderer.color;
        newColor.a = 0.6f;  // Reduce opacity to 50%
        ghostSpriteRenderer.color = newColor;

        StartCoroutine(FadeOutGhost(currentGhost));
    }

    IEnumerator FadeOutGhost(GameObject ghost)
    {
        SpriteRenderer ghostSprite = ghost.GetComponent<SpriteRenderer>();
        float fadeDuration = 1.0f;  // Fade duration in seconds
        float fadeRate = 1.0f / fadeDuration;  // Rate of fade
        float alpha = ghostSprite.color.a;  // Initial alpha

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeRate;  // Reduce alpha based on time and rate
            ghostSprite.color = new Color(ghostSprite.color.r, ghostSprite.color.g, ghostSprite.color.b, alpha);
            yield return null;
        }

        Destroy(ghost);  // Destroy the ghost after fading out
    }

}

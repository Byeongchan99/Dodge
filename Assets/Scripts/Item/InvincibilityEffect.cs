using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    private Coroutine _colorChangeCoroutine;  // 색상 변경 코루틴을 저장할 변수

    public InvincibilityEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        // 무적 적용
        Debug.Log("무적 아이템 효과 적용");
        PlayerStat.Instance.StartInvincibility(_duration);  // 무적 상태 시작
        _colorChangeCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(ChangeColor());
    }

    public override void RemoveEffect()
    {
        // 무적 해제
        Debug.Log("무적 아이템 효과 종료");
        if (_colorChangeCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(_colorChangeCoroutine);
        }

        // 색상을 원래대로 복구
        RestoreOriginalColors();
    }

    private void RestoreOriginalColors()
    {
        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();
        if (playerSpriteRenderer != null)
        {
            playerSpriteRenderer.color = Color.white;
        }

        SpriteRenderer crownSpriteRenderer = target.GetComponentInChildren<SpriteRenderer>();
        if (crownSpriteRenderer != null)
        {
            crownSpriteRenderer.color = Color.white;
        }
    }

    private IEnumerator ChangeColor()
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int colorIndex = 0;

        SpriteRenderer playerSpriteRenderer = target.GetComponent<SpriteRenderer>();
        SpriteRenderer crownSpriteRenderer = target.GetComponentInChildren<SpriteRenderer>();

        while (true)
        {
            if (playerSpriteRenderer != null)
            {
                Debug.Log("플레이어 색상 변경");
                playerSpriteRenderer.color = colors[colorIndex];
            }

            if (crownSpriteRenderer != null)
            {
                Debug.Log("왕관 색상 변경");
                crownSpriteRenderer.color = colors[colorIndex];
                Debug.Log("왕관 색상: " + crownSpriteRenderer.color.ToString());
            }

            colorIndex = (colorIndex + 1) % colors.Length;  // 다음 색상 인덱스로 업데이트
            yield return new WaitForSeconds(0.1f);  // 0.1초 마다 색상 변경
        }
    }
}

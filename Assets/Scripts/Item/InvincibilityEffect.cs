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
        PlayerStat.Instance.isInvincibility = true;
        _colorChangeCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(ChangeColor());
    }

    public override void RemoveEffect()
    {
        // 무적 해제
        Debug.Log("무적 아이템 효과 종료");
        PlayerStat.Instance.isInvincibility = false;
        if (_colorChangeCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(_colorChangeCoroutine);
        }
        target.GetComponent<SpriteRenderer>().color = Color.white;  // 색상을 원래대로 복구
    }

    private IEnumerator ChangeColor()
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int colorIndex = 0;

        while (true)
        {
            target.GetComponent<SpriteRenderer>().color = colors[colorIndex];  // 현재 인덱스의 색으로 변경
            colorIndex = (colorIndex + 1) % colors.Length;  // 다음 색상 인덱스로 업데이트
            yield return new WaitForSeconds(0.1f);  // 0.1초 마다 색상 변경
        }
    }
}

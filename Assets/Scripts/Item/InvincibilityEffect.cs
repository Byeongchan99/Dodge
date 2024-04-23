using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityEffect : ItemEffect
{
    private Coroutine _colorChangeCoroutine;  // ���� ���� �ڷ�ƾ�� ������ ����

    public InvincibilityEffect(float duration, GameObject target) : base(duration, target) { }

    public override void ApplyEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        PlayerStat.Instance.StartInvincibility(_duration);  // ���� ���� ����
        _colorChangeCoroutine = target.GetComponent<MonoBehaviour>().StartCoroutine(ChangeColor());
    }

    public override void RemoveEffect()
    {
        // ���� ����
        Debug.Log("���� ������ ȿ�� ����");
        if (_colorChangeCoroutine != null)
        {
            target.GetComponent<MonoBehaviour>().StopCoroutine(_colorChangeCoroutine);
        }
        target.GetComponent<SpriteRenderer>().color = Color.white;  // ������ ������� ����
    }

    private IEnumerator ChangeColor()
    {
        Color[] colors = new Color[] { Color.red, Color.yellow, Color.green, Color.cyan, Color.blue, Color.magenta };
        int colorIndex = 0;

        while (true)
        {
            target.GetComponent<SpriteRenderer>().color = colors[colorIndex];  // ���� �ε����� ������ ����
            colorIndex = (colorIndex + 1) % colors.Length;  // ���� ���� �ε����� ������Ʈ
            yield return new WaitForSeconds(0.1f);  // 0.1�� ���� ���� ����
        }
    }
}

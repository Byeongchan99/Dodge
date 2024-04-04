using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute(PlayerStat stat);
}

public class Blink : IPlayerAbility
{
    private float blinkDistance = 5.0f; // �÷��̾ ������ �� �ִ� �Ÿ�
    private float slowDownFactor = 0.05f; // �ð��� ������ �ϴ� ���
    private bool isBlinking = false;

    public void Execute(PlayerStat stat)
    {
        if (!isBlinking)
        {
            // �ð��� ������ �Ѵ�
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            // �ð�ȭ ������ ���⿡ �߰��Ѵ� (��: ���� ������ ǥ���ϴ� ���� �׸���)
            ShowBlinkRange(stat.currentPoisition.position);

            // �÷��̾� �Է��� �޴´� (Input.GetButtonDown ���� ���)
            Vector3 direction = Vector3.zero;

            // �÷��̾ �Է��� ��ġ�� �޾Ƽ� �����Ѵ�
            Vector3 targetPosition = GetBlinkTargetPosition(stat.currentPoisition.position, direction);

            // ���� ����
            DoBlink(stat, targetPosition);
        }
    }

    private Vector3 GetBlinkTargetPosition(Vector3 currentPosition, Vector3 direction)
    {
        // �Է��� ������� �� ��ġ�� ���
        Vector3 targetPosition = currentPosition + direction.normalized * blinkDistance;


        // ������ Ȯ���Ͽ� �ִ� ���� �Ÿ��� �ʰ����� �ʴ��� Ȯ���Ѵ�
        return Vector3.ClampMagnitude(targetPosition - currentPosition, blinkDistance) + currentPosition;
    }

    private void DoBlink(PlayerStat stat, Vector3 targetPosition)
    {
        // �ð��� �������� �ǵ�����
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;

        // �÷��̾��� ��ġ�� ������Ʈ�Ѵ�
        stat.currentPoisition.position = targetPosition;

        // ���� ���� ǥ�ø� �����Ѵ�
        HideBlinkRange();

        // ���� ���¸� �����Ѵ�
        isBlinking = false;
    }

    private void ShowBlinkRange(Vector3 position)
    {
        // ���� ������ �ð������� ǥ���ϴ� �ڵ�
    }

    private void HideBlinkRange()
    {
        // �ð������� ǥ�õ� ���� ������ �����ϴ� �ڵ�
    }
}

public class EMP : IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // EMP ����
        // ��ó�� ����ü ����ȭ
    }
}

public class DefenseProtocol: IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // ��� �������� ����
        // ���� �ð� ���� ���� �� �ൿ �Ұ�
    }
}
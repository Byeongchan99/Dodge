using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute();
}

public class EMP : IPlayerAbility
{
    public void Execute()
    {
        // EMP ����
        // ��ó�� ����ü ����ȭ
    }
}

public class DefenseProtocol: IPlayerAbility
{
    public void Execute()
    {
        // ��� �������� ����
        // ���� �ð� ���� ���� �� �ൿ �Ұ�
    }
}
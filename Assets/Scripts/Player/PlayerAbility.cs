using System.Diagnostics;
using TMPro;
using UnityEngine;

public interface IPlayerAbility
{
    void Execute(PlayerStat stat);
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
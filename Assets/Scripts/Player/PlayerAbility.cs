using System.Diagnostics;

public interface IPlayerAbility
{
    void Execute(PlayerStat stat);
}

public class Blink : IPlayerAbility
{
    public void Execute(PlayerStat stat)
    {
        // ��ũ ����
        // ª�� �Ÿ��� �����̵�
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
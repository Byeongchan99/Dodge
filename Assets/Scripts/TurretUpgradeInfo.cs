[System.Serializable]
public class TurretUpgradeInfo
{
    public enum TurretType // �ͷ� ����
    {
        Bullet, // �Ѿ�
        Laser, // ������
        Rocket, // ����
        Mortar // �ڰ���
    }
    public TurretType turretType;

    public enum EnhancementType // �ͷ� ��ȭ ����
    {
        CountIncrease, // �߻�ü ���� ����
        SpeedIncrease, // �߻�ü �ӵ� ����
        ProjectileSplit, // �߻�ü �п�
        RemainTimeIncrease, // �߻�ü ���ӽð� ����(������ �ͷ� ����)
        InductionUpgrade, // ���� ���� ����(�ڰ��� �ͷ� ����)
        RemoveSplit // �п� ����
    }
    public EnhancementType enhancementType;
}

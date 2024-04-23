using System.Numerics;
using UnityEngine.UIElements.Experimental;

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
        CountChange, // �߻�ü ���� ����
        SpeedChange, // �߻�ü �ӵ� ����
        IsProjectileSplit, // �߻�ü �п�
        RemainTimeChange, // �߻�ü ���ӽð� ����(������ �ͷ�, ���� �ͷ� ����)
        SizeChange, // ũ�� ����
        Init, // �ʱ�ȭ  
    }
    public EnhancementType enhancementType;

    public float value; // ��ȭ ��
    public Vector3 projectileSize; // �߻�ü ũ��
}

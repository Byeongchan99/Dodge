using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventProcessor : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    public void HandleEvent(string eventName)
    {
        // �̺�Ʈ�� ���� ���� ó��

        // currentStatData ���� ����
        /*
        CopyedStatData updatedStatData = StatDataManager.Instance.GetDataForEvent(eventName);
        StatDataManager.Instance.currentStatData = updatedStatData;
        */
    }

    /// <summary> �̺�Ʈ ���� </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        

        switch (enhancement.turretType)
        {
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret�� ���׷��̵� ó���� ���� ���� switch ��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �Ѿ� ���׷��̵� ó��
                        Debug.Log("�п� �Ѿ� ���׷��̵� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ���� ó��
                        Debug.Log("�Ѿ� ���� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount += (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ���� ó��
                        Debug.Log("�Ѿ� �ӵ� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ���� ó��
                        Debug.Log("�Ѿ� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.x + enhancement.projectileSize.X,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.y + enhancement.projectileSize.Y,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.z + enhancement.projectileSize.Z
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;  
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ ����
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeChange:
                        // ���� �ð� ó��
                        Debug.Log("������ ���� �ð� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ���� ó��
                        Debug.Log("������ ���� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount += (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // �ӵ� ���� ó��
                        Debug.Log("������ ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                                                    StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.x + enhancement.projectileSize.X,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.y + enhancement.projectileSize.Y,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.z + enhancement.projectileSize.Z
                                                    );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ ����
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ���� ó��
                        Debug.Log("���� ���� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[2].projectileCount += (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ���� ó��
                        Debug.Log("���� �ӵ� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.RemainTimeChange:
                        // ���� �ð� ���� ó��
                        Debug.Log("���� ���� �ð� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ���� ó��
                        Debug.Log("���� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                                                    StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.x + enhancement.projectileSize.X,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.y + enhancement.projectileSize.Y,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.z + enhancement.projectileSize.Z
                                                    );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ ����
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                    // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �ڰ���ź ���׷��̵� ó��
                        Debug.Log("�п� �ڰ���ź ���׷��̵� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ���� ó��
                        Debug.Log("�ڰ���ź ���� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileCount += (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ���� ó��
                        Debug.Log("�ڰ���ź �ӵ� ���� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed += enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ���� ó��
                        Debug.Log("���� ���� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                                                    StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.x + enhancement.projectileSize.X,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.y + enhancement.projectileSize.Y,
                                                    StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.z + enhancement.projectileSize.Z
                                                    );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ ����
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
                // ��Ÿ �ͷ� ������ ���� ó��
        }
    }
}

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
            // �Ѿ� �ͷ�
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �Ѿ� ���׷��̵�
                        Debug.Log("�п� �Ѿ� ���׷��̵� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        Debug.Log("�Ѿ� ���� ���� " + enhancement.value);
                        // ���� ����
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        Debug.Log("�Ѿ� �ӵ� ���� " + enhancement.value);
                        // ���� ����
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("�Ѿ� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;  
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                }
                break;

            // ������ �ͷ�
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // ���� �ð�
                        Debug.Log("������ ���� �ð� ���� " + enhancement.value);
                        // ���� ����
                        float newLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime = Mathf.Max(0.5f, newLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        Debug.Log("������ ���� ���� " + enhancement.value);
                        // ���� ����
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // �ӵ� ����
                        Debug.Log("������ ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;

            // ���� �ͷ�
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // ���� �ð� ����
                        Debug.Log("���� ���� �ð� ���� " + enhancement.value);
                        // ���� ����
                        float newLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime = Mathf.Max(0.5f, newLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        Debug.Log("���� ���� ���� " + enhancement.value);
                        // ���� ����
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[2].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[2].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        Debug.Log("���� �ӵ� ���� " + enhancement.value);
                        // ���� ����
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("���� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.currentStatData = new CopyedStatData(StatDataManager.Instance.originalStatData);
                        break;
                    // ��Ÿ �ʿ��� ��� �߰�
                }
                break;

            // �ڰ��� �ͷ�
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �ڰ���ź ���׷��̵�
                        Debug.Log("�п� �ڰ���ź ���׷��̵� " + enhancement.value);
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex = (int)enhancement.value;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        Debug.Log("�ڰ���ź ���� ���� " + enhancement.value);
                        // ���� ����
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[3].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[3].projectileCount = Mathf.Max(1, newProjectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        Debug.Log("�ڰ���ź �ӵ� ���� " + enhancement.value);
                        // ���� ����
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed = Mathf.Max(0.5f, newProjectileSpeed);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("���� ���� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.z *= enhancement.value
                            );
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
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

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
                        Debug.Log("projectileCount " + StatDataManager.Instance.currentStatData.turretDatas[0].projectileCount);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        Debug.Log("�Ѿ� �ӵ� ���� " + enhancement.value);
                        // ���� ����
                        float newProjectileSpeed = StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 0.5f, 10f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("�Ѿ� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize.z *= enhancement.value
                            );
                        // ������ �ּ� �� �ִ� ũ�� ���Ѱ� ����
                        Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
                        Vector3 maxSize = new Vector3(1f, 1f, 1f);

                        // Vector3�� �� ��ҿ� ���� Clamp ����
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[0].projectileSize = newSize;
                        break;  
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.InitStatData();
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
                        float newProjectileLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime + enhancement.value;
                        // projectileLifeTime�� �ּ� 0.5��, �ִ� 5�� ���̷� ����
                        float clampedProjectileLifeTime = Mathf.Clamp(newProjectileLifeTime, 0.5f, 5f);
                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileLifeTime = clampedProjectileLifeTime;
                        // newProjectileLifeTime 5���� ���� ��쿡�� �ͷ��� ���� �ð� ����
                        if (newProjectileLifeTime > 5f)
                        {
                            float adjustedTurretLifeTime = StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime + (enhancement.value * StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount);
                            StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime = Mathf.Max(3f, adjustedTurretLifeTime);
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        Debug.Log("������ ���� ���� " + enhancement.value);
                        // ���� ����
                        int newProjectileCount = StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount + (int)enhancement.value;
                        StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount = Mathf.Max(1, newProjectileCount);
                        // �ͷ��� ���� �ð� ���� ���� ����
                        float newTurretLifeTime = (StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime * StatDataManager.Instance.currentStatData.turretDatas[1].projectileCount);
                        StatDataManager.Instance.currentStatData.turretDatas[1].turretLifeTime = Mathf.Max(3f, newTurretLifeTime);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // �ӵ� ����
                        Debug.Log("������ ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.y = 25f, // ������
                            StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize.z *= enhancement.value
                            );

                        // ������ �ּ� �� �ִ� ũ�� ���Ѱ� ����
                        Vector3 minSize = new Vector3(0.2f, 25f, 0.2f);
                        Vector3 maxSize = new Vector3(2.5f, 25f, 2.5f);

                        // Vector3�� �� ��ҿ� ���� Clamp ����
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = 25f; // ������
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[1].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.InitStatData();
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
                        float newProjectileLifeTime = StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime + enhancement.value;
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileLifeTime = Mathf.Clamp(newProjectileLifeTime, 5f, 20f);
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
                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 0.5f, 10f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("���� ũ�� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize.z *= enhancement.value
                            );
                        // ������ �ּ� �� �ִ� ũ�� ���Ѱ� ����
                        Vector3 minSize = new Vector3(0.1f, 0.1f, 0.1f);
                        Vector3 maxSize = new Vector3(1f, 1f, 1f);

                        // Vector3�� �� ��ҿ� ���� Clamp ����
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[2].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.InitStatData();
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
                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSpeed = Mathf.Clamp(newProjectileSpeed, 1f, 5f);
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        Debug.Log("���� ���� ���� " + enhancement.value);
                        Vector3 newSize = new Vector3(
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.x *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.y *= enhancement.value,
                            StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize.z *= enhancement.value
                            );
                        // ������ �ּ� �� �ִ� ũ�� ���Ѱ� ����
                        Vector3 minSize = new Vector3(1.666667f, 1f, 1.666667f);
                        Vector3 maxSize = new Vector3(3.75f, 2.25f, 3.75f);

                        // Vector3�� �� ��ҿ� ���� Clamp ����
                        newSize.x = Mathf.Clamp(newSize.x, minSize.x, maxSize.x);
                        newSize.y = Mathf.Clamp(newSize.y, minSize.y, maxSize.y);
                        newSize.z = Mathf.Clamp(newSize.z, minSize.z, maxSize.z);

                        StatDataManager.Instance.currentStatData.projectileDatas[3].projectileSize = newSize;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        Debug.Log("�ͷ� �ʱ�ȭ");
                        StatDataManager.Instance.InitStatData();
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
                // ��Ÿ �ͷ� ������ ���� ó��
        }
    }
}

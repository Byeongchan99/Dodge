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
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret �п� �Ѿ� ���׷��̵� ó��
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = 1;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.RemoveSplit:
                        StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex = 0;
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ ����

                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeIncrease:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.InductionUpgrade:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:

                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
                // ��Ÿ �ͷ� ������ ���� ó��
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static PlayerStat Instance { get; private set; } // �̱��� �ν��Ͻ�

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    // �ӽ÷� �̺�Ʈ �׽�Ʈ�� ���� �޼���
    /// <summary> �ͷ� �п� �Ѿ� Ȱ��ȭ �̺�Ʈ </summary>
    public void TurretSplitEvent()
    {
        TurretUpgradeInfo bulletTurretSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.ProjectileSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> �ͷ� �п� �Ѿ� Ȱ��ȭ �̺�Ʈ </summary>
    public void TurretRemoveSplitEvent()
    {
        TurretUpgradeInfo bulletTurretRemoveSplit = new TurretUpgradeInfo
        {
            turretType = TurretUpgradeInfo.TurretType.Bullet,
            enhancementType = TurretUpgradeInfo.EnhancementType.RemoveSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }
}

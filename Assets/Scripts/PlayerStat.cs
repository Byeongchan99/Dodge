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
        TurretUpgrade bulletTurretSplit = new TurretUpgrade
        {
            turretType = TurretUpgrade.TurretType.Bullet,
            enhancementType = TurretUpgrade.EnhancementType.ProjectileSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretSplit);
    }

    /// <summary> �ͷ� �п� �Ѿ� Ȱ��ȭ �̺�Ʈ </summary>
    public void TurretRemoveSplitEvent()
    {
        TurretUpgrade bulletTurretRemoveSplit = new TurretUpgrade
        {
            turretType = TurretUpgrade.TurretType.Bullet,
            enhancementType = TurretUpgrade.EnhancementType.RemoveSplit,
        };

        EventManager.TriggerEnhancementEvent("TurretUpgrade", bulletTurretRemoveSplit);
    }
}

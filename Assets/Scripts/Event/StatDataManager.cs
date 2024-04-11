using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatData;

[System.Serializable]
public class StatDataEntry
{
    public string eventName;
    public StatData statData;
}

public class StatDataManager : MonoBehaviour
{
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static StatDataManager Instance { get; private set; }
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<StatDataEntry> turretDataList = new List<StatDataEntry>();
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, StatData> turretDataByEvent = new Dictionary<string, StatData>();
    /// <summary> ���� ���� ������ </summary>
    public StatData originalStatData;

    private CopyedStatData _currentStatData;
    public CopyedStatData currentStatData
    {
        get { return _currentStatData; }
        set
        {
            _currentStatData = value;
        }
    }

    private void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            PopulateDictionary();
        }
        else
        {
            Destroy(gameObject);
        }

        // ���� ���� ������ ����
        Debug.Log("���� �Ŵ��� �ʱ�ȭ");
        originalStatData = GetDataForEvent("Init");
        currentStatData = new CopyedStatData(originalStatData);
        SettingTurretSpawnChances();
        SettingItemSpawnChances();
    }

    /// <summary> ����Ʈ�� ��ųʸ��� ��ȯ </summary>
    private void PopulateDictionary()
    {
        turretDataByEvent.Clear();
        foreach (var entry in turretDataList)
        {
            turretDataByEvent[entry.eventName] = entry.statData;
        }
    }

    /// <summary> �̺�Ʈ �̸��� ���� ������ ��ũ���ͺ� ������Ʈ ������ ��ȯ </summary>
    public StatData GetDataForEvent(string eventName)
    {
        if (turretDataByEvent.ContainsKey(eventName))
        {
            return turretDataByEvent[eventName];
        }
        else
        {
            Debug.LogWarning("�߸��� �̺�Ʈ �̸�: " + eventName);
            return null;
        }
    }

    /// <summary> currentData�� spawnLevel�� ���� �ͷ��� spawnChance���� ���� </summary>
    void SettingTurretSpawnChances()
    {
        // ���� �հ� ���
        int levelSum = 0;
        for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
        {
            levelSum += currentStatData.turretSpawnerDatas[i].spawnLevel;
        }

        // ���� �հ谡 0�� ���, ��� Ȯ���� �����ϰ� �����Ͽ� ���� ����
        if (levelSum == 0)
        {
            Debug.LogError("���� �հ谡 0. ��� Ȯ�� �����ϰ� ����");
            float equalChance = 100f / currentStatData.turretSpawnerDatas.Count;
            for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
            {
                currentStatData.turretSpawnerDatas[i].spawnChance = equalChance;
            }
            return;
        }

        // �� �ͷ��� ��ȯ Ȯ�� ���
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // ������ ���� �� Ȯ�� �հ� ���
        for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
        {
            currentStatData.turretSpawnerDatas[i].spawnChance = chancePerLevel * currentStatData.turretSpawnerDatas[i].spawnLevel;
            totalPercentage += currentStatData.turretSpawnerDatas[i].spawnChance;
        }

        // Ȯ���� ������ 100%�� ������� Ȯ�� (�ε��Ҽ��� �������� ���� ���� ���� ���)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"Ȯ�� ������ 100%�� �ƴ�. ����: {totalPercentage}%");
        }
    }

    /// <summary> currentData�� spawnLevel�� ���� �������� spawnChance���� ���� </summary>
    void SettingItemSpawnChances()
    {
        // ���� �հ� ���
        int levelSum = 0;
        for (int i = 0; i < currentStatData.itemDatas.Count; i++)
        {
            levelSum += currentStatData.itemDatas[i].spawnLevel;
        }

        // ���� �հ谡 0�� ���, ��� Ȯ���� �����ϰ� �����Ͽ� ���� ����
        if (levelSum == 0)
        {
            Debug.LogError("���� �հ谡 0. ��� Ȯ�� �����ϰ� ����");
            float equalChance = 100f / currentStatData.itemDatas.Count;
            for (int i = 0; i < currentStatData.itemDatas.Count; i++)
            {
                currentStatData.itemDatas[i].spawnChance = equalChance;
            }
            return;
        }

        // �� �������� ��ȯ Ȯ�� ���
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // ������ ���� �� Ȯ�� �հ� ���
        for (int i = 0; i < currentStatData.itemDatas.Count; i++)
        {
            currentStatData.itemDatas[i].spawnChance = chancePerLevel * currentStatData.itemDatas[i].spawnLevel;
            totalPercentage += currentStatData.itemDatas[i].spawnChance;
        }

        // Ȯ���� ������ 100%�� ������� Ȯ�� (�ε��Ҽ��� �������� ���� ���� ���� ���)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"Ȯ�� ������ 100%�� �ƴ�. ����: {totalPercentage}%");
        }
    }
}

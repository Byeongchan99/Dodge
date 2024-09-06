using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StatData;

[System.Serializable]
public class StatDataEntry
{
    public string statDataName;
    public StatData statData;
}

public class StatDataManager : MonoBehaviour
{
    /****************************************************************************
                                    protected Fields
    ****************************************************************************/
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, StatData> statDataByEvent = new Dictionary<string, StatData>();
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<StatDataEntry> statDataList = new List<StatDataEntry>();

    private CopyedStatData _currentStatData;

    /****************************************************************************
                                    public Fields
    ****************************************************************************/
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static StatDataManager Instance { get; private set; }

    /// <summary> ���� ���� ������ </summary>
    public StatData originalStatData;

    /// <summary> ���� ���� ������ </summary>
    public CopyedStatData currentStatData
    {
        get { return _currentStatData; }
        set
        {
            _currentStatData = value;
        }
    }

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
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
        SetOriginalStatData("Bullet");
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> ����Ʈ�� ��ųʸ��� ��ȯ </summary>
    private void PopulateDictionary()
    {
        statDataByEvent.Clear();
        foreach (var entry in statDataList)
        {
            statDataByEvent[entry.statDataName] = entry.statData;
        }
    }

    /// <summary> ���� ������ �̸��� ���� ������ ��ũ���ͺ� ������Ʈ ������ ��ȯ </summary>
    private StatData GetDataForEvent(string statDataName)
    {
        if (statDataByEvent.ContainsKey(statDataName))
        {
            return statDataByEvent[statDataName];
        }
        else
        {
            Debug.LogWarning("�߸��� �̺�Ʈ �̸�: " + statDataName);
            return null;
        }
    }

    /// <summary> currentData�� spawnLevel�� ���� �ͷ��� spawnChance���� ���� </summary>
    private void SetSpawnChances<T>(List<T> dataList) where T : StatData.BaseSpawnData
    {
        int levelSum = 0;
        foreach (var data in dataList)
        {
            levelSum += data.spawnLevel;
        }

        // spawnLevel�� 0�� ��� ��� �����Ϳ� ������ Ȯ�� �ο�
        if (levelSum == 0)
        {
            float equalChance = 100f / dataList.Count;
            foreach (var data in dataList)
            {
                data.spawnChance = equalChance;
            }
        }
        else // spawnLevel�� 0�� �ƴ� ��� spawnLevel�� ���� Ȯ�� �ο�
        {
            float chancePerLevel = 100f / levelSum;
            foreach (var data in dataList)
            {
                data.spawnChance = chancePerLevel * data.spawnLevel;
            }
        }
    }

    /****************************************************************************
                                    public Methods
    ****************************************************************************/
    /// <summary> �������� ���� ������ ���� </summary>
    public void SetOriginalStatData(string statDataName)
    {
        originalStatData = GetDataForEvent(statDataName);
        InitStatData();
    }

    /// <summary> currentStatData �ʱ�ȭ </summary>
    public void InitStatData()
    {
        currentStatData = new CopyedStatData(originalStatData);
        SetSpawnChances(currentStatData.turretSpawnerDatas);
        SetSpawnChances(currentStatData.itemDatas);
    }
}

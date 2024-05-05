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
    /// <summary> 싱글톤 인스턴스 </summary>
    public static StatDataManager Instance { get; private set; }
    /// <summary> 이벤트 이름에 따른 스크립터블 오브젝트 데이터 매핑 </summary>
    [SerializeField] private List<StatDataEntry> statDataList = new List<StatDataEntry>();
    /// <summary> 딕셔너리로 변환한 스크립터블 오브젝트 데이터 </summary>
    private Dictionary<string, StatData> statDataByEvent = new Dictionary<string, StatData>();
    /// <summary> 원본 스탯 데이터 </summary>
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
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
            PopulateDictionary();
        }
        else
        {
            Destroy(gameObject);
        }

        // 현재 스탯 데이터 설정
        Debug.Log("스탯 매니저 초기화");
        SetOriginalStatData("Init");
    }

    /// <summary> 오리지널 스탯 데이터 설정 </summary>
    public void SetOriginalStatData(string eventName)
    {
        originalStatData = GetDataForEvent(eventName);
        InitStatData();
    }

    /// <summary> 리스트를 딕셔너리로 변환 </summary>
    private void PopulateDictionary()
    {
        statDataByEvent.Clear();
        foreach (var entry in statDataList)
        {
            statDataByEvent[entry.eventName] = entry.statData;
        }
    }

    /// <summary> 이벤트 이름에 따라 적절한 스크립터블 오브젝트 데이터 반환 </summary>
    private StatData GetDataForEvent(string eventName)
    {
        if (statDataByEvent.ContainsKey(eventName))
        {
            return statDataByEvent[eventName];
        }
        else
        {
            Debug.LogWarning("잘못된 이벤트 이름: " + eventName);
            return null;
        }
    }

    /// <summary> currentStatData 초기화 </summary>
    public void InitStatData()
    {
        currentStatData = new CopyedStatData(originalStatData);
        SetSpawnChances(currentStatData.turretSpawnerDatas);
        SetSpawnChances(currentStatData.itemDatas);
    }

    /// <summary> currentData의 spawnLevel을 토대로 터렛의 spawnChance들을 설정 </summary>
    private void SetSpawnChances<T>(List<T> dataList) where T : StatData.BaseSpawnData
    {
        int levelSum = 0;
        foreach (var data in dataList)
        {
            levelSum += data.spawnLevel;
        }

        if (levelSum == 0)
        {
            float equalChance = 100f / dataList.Count;
            foreach (var data in dataList)
            {
                data.spawnChance = equalChance;
            }
        }
        else
        {
            float chancePerLevel = 100f / levelSum;
            foreach (var data in dataList)
            {
                data.spawnChance = chancePerLevel * data.spawnLevel;
            }
        }
    }
}

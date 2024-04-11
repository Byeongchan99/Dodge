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
    [SerializeField] private List<StatDataEntry> turretDataList = new List<StatDataEntry>();
    /// <summary> 딕셔너리로 변환한 스크립터블 오브젝트 데이터 </summary>
    private Dictionary<string, StatData> turretDataByEvent = new Dictionary<string, StatData>();
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
        originalStatData = GetDataForEvent("Init");
        currentStatData = new CopyedStatData(originalStatData);
        SettingTurretSpawnChances();
        SettingItemSpawnChances();
    }

    /// <summary> 리스트를 딕셔너리로 변환 </summary>
    private void PopulateDictionary()
    {
        turretDataByEvent.Clear();
        foreach (var entry in turretDataList)
        {
            turretDataByEvent[entry.eventName] = entry.statData;
        }
    }

    /// <summary> 이벤트 이름에 따라 적절한 스크립터블 오브젝트 데이터 반환 </summary>
    public StatData GetDataForEvent(string eventName)
    {
        if (turretDataByEvent.ContainsKey(eventName))
        {
            return turretDataByEvent[eventName];
        }
        else
        {
            Debug.LogWarning("잘못된 이벤트 이름: " + eventName);
            return null;
        }
    }

    /// <summary> currentData의 spawnLevel을 토대로 터렛의 spawnChance들을 설정 </summary>
    void SettingTurretSpawnChances()
    {
        // 레벨 합계 계산
        int levelSum = 0;
        for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
        {
            levelSum += currentStatData.turretSpawnerDatas[i].spawnLevel;
        }

        // 레벨 합계가 0인 경우, 모든 확률을 동일하게 설정하여 에러 방지
        if (levelSum == 0)
        {
            Debug.LogError("레벨 합계가 0. 모든 확률 동일하게 설정");
            float equalChance = 100f / currentStatData.turretSpawnerDatas.Count;
            for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
            {
                currentStatData.turretSpawnerDatas[i].spawnChance = equalChance;
            }
            return;
        }

        // 각 터렛의 소환 확률 계산
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // 검증을 위한 총 확률 합계 계산
        for (int i = 0; i < currentStatData.turretSpawnerDatas.Count; i++)
        {
            currentStatData.turretSpawnerDatas[i].spawnChance = chancePerLevel * currentStatData.turretSpawnerDatas[i].spawnLevel;
            totalPercentage += currentStatData.turretSpawnerDatas[i].spawnChance;
        }

        // 확률의 총합이 100%에 가까운지 확인 (부동소수점 연산으로 인한 작은 오차 허용)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"확률 총합이 100%가 아님. 총합: {totalPercentage}%");
        }
    }

    /// <summary> currentData의 spawnLevel을 토대로 아이템의 spawnChance들을 설정 </summary>
    void SettingItemSpawnChances()
    {
        // 레벨 합계 계산
        int levelSum = 0;
        for (int i = 0; i < currentStatData.itemDatas.Count; i++)
        {
            levelSum += currentStatData.itemDatas[i].spawnLevel;
        }

        // 레벨 합계가 0인 경우, 모든 확률을 동일하게 설정하여 에러 방지
        if (levelSum == 0)
        {
            Debug.LogError("레벨 합계가 0. 모든 확률 동일하게 설정");
            float equalChance = 100f / currentStatData.itemDatas.Count;
            for (int i = 0; i < currentStatData.itemDatas.Count; i++)
            {
                currentStatData.itemDatas[i].spawnChance = equalChance;
            }
            return;
        }

        // 각 아이템의 소환 확률 계산
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // 검증을 위한 총 확률 합계 계산
        for (int i = 0; i < currentStatData.itemDatas.Count; i++)
        {
            currentStatData.itemDatas[i].spawnChance = chancePerLevel * currentStatData.itemDatas[i].spawnLevel;
            totalPercentage += currentStatData.itemDatas[i].spawnChance;
        }

        // 확률의 총합이 100%에 가까운지 확인 (부동소수점 연산으로 인한 작은 오차 허용)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"확률 총합이 100%가 아님. 총합: {totalPercentage}%");
        }
    }
}

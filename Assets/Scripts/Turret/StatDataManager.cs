using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

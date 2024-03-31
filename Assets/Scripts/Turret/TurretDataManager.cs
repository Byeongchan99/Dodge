using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerDataEntry
{
    public string eventName;
    public TurretSpawnerData spawnerData;
}

public class TurretDataManager : MonoBehaviour
{
    /// <summary> 싱글톤 인스턴스 </summary>
    public static TurretSpawnerDataFactory Instance { get; private set; }
    /// <summary> 이벤트 이름에 따른 스크립터블 오브젝트 데이터 매핑 </summary>
    [SerializeField] private List<SpawnerDataEntry> spawnerDataList = new List<SpawnerDataEntry>();
    /// <summary> 딕셔너리로 변환한 스크립터블 오브젝트 데이터 </summary>
    private Dictionary<string, TurretSpawnerData> spawnerDataByEvent = new Dictionary<string, TurretSpawnerData>();

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
    }

    /// <summary> 리스트를 딕셔너리로 변환 </summary>
    private void PopulateDictionary()
    {
        spawnerDataByEvent.Clear();
        foreach (var entry in spawnerDataList)
        {
            spawnerDataByEvent[entry.eventName] = entry.spawnerData;
        }
    }

    /// <summary> 이벤트 이름에 따라 적절한 스크립터블 오브젝트 데이터 반환 </summary>
    public TurretSpawnerData GetSpawnerDataForEvent(string eventName)
    {
        if (spawnerDataByEvent.ContainsKey(eventName))
        {
            return spawnerDataByEvent[eventName];
        }
        else
        {
            Debug.LogWarning("잘못된 이벤트 이름: " + eventName);
            return null;
        }
    }
}

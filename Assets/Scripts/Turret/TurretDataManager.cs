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
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static TurretSpawnerDataFactory Instance { get; private set; }
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<SpawnerDataEntry> spawnerDataList = new List<SpawnerDataEntry>();
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, TurretSpawnerData> spawnerDataByEvent = new Dictionary<string, TurretSpawnerData>();

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
    }

    /// <summary> ����Ʈ�� ��ųʸ��� ��ȯ </summary>
    private void PopulateDictionary()
    {
        spawnerDataByEvent.Clear();
        foreach (var entry in spawnerDataList)
        {
            spawnerDataByEvent[entry.eventName] = entry.spawnerData;
        }
    }

    /// <summary> �̺�Ʈ �̸��� ���� ������ ��ũ���ͺ� ������Ʈ ������ ��ȯ </summary>
    public TurretSpawnerData GetSpawnerDataForEvent(string eventName)
    {
        if (spawnerDataByEvent.ContainsKey(eventName))
        {
            return spawnerDataByEvent[eventName];
        }
        else
        {
            Debug.LogWarning("�߸��� �̺�Ʈ �̸�: " + eventName);
            return null;
        }
    }
}

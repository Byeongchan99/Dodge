using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnerDataEntry
{
    public string eventName;
    public TurretData spawnerData;
}

public class TurretDataManager : MonoBehaviour
{
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static TurretDataManager Instance { get; private set; }
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<SpawnerDataEntry> spawnerDataList = new List<SpawnerDataEntry>();
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, TurretData> spawnerDataByEvent = new Dictionary<string, TurretData>();

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
    public TurretData GetSpawnerDataForEvent(string eventName)
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

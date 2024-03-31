using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretDataEntry
{
    public string eventName;
    public TurretData spawnerData;
}

public class TurretDataManager : MonoBehaviour
{
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static TurretDataManager Instance { get; private set; }
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<TurretDataEntry> turretDataList = new List<TurretDataEntry>();
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, TurretData> turretDataByEvent = new Dictionary<string, TurretData>();

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
        turretDataByEvent.Clear();
        foreach (var entry in turretDataList)
        {
            turretDataByEvent[entry.eventName] = entry.spawnerData;
        }
    }

    /// <summary> �̺�Ʈ �̸��� ���� ������ ��ũ���ͺ� ������Ʈ ������ ��ȯ </summary>
    public TurretData GetSpawnerDataForEvent(string eventName)
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
}

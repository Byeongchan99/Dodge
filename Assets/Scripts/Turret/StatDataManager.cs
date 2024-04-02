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
    /// <summary> �̱��� �ν��Ͻ� </summary>
    public static StatDataManager Instance { get; private set; }
    /// <summary> �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ ���� </summary>
    [SerializeField] private List<StatDataEntry> turretDataList = new List<StatDataEntry>();
    /// <summary> ��ųʸ��� ��ȯ�� ��ũ���ͺ� ������Ʈ ������ </summary>
    private Dictionary<string, StatData> turretDataByEvent = new Dictionary<string, StatData>();

    /// <summary> ������ �������� ������ ���� ���� ������ </summary>
    public event EventHandler<StatDataChangedEventArgs> StatDataChanged;

    private void OnStatDataChanged(StatDataChangedEventArgs e)
    {
        StatDataChanged?.Invoke(this, e);
    }

    private CopyedStatData _currentStatData;
    public CopyedStatData currentStatData
    {
        get { return _currentStatData; }
        set
        {
            _currentStatData = value;
            // setter���� ���� ����� ������ OnStatDataChanged �߻�
            OnStatDataChanged(new StatDataChangedEventArgs(_currentStatData));
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
        StatData originalStatData = GetDataForEvent("Init");
        currentStatData = new CopyedStatData(originalStatData);
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
}

/// <summary> ����� �����͸� �̺�Ʈ ���ڷ� �����ϱ� ���� Ŭ���� </summary>
public class StatDataChangedEventArgs : EventArgs
{
    public CopyedStatData NewStatData { get; }

    public StatDataChangedEventArgs(CopyedStatData newStatData)
    {
        NewStatData = newStatData;
    }
}

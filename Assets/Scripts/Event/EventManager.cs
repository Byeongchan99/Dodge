using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TurretUpgradeEvent : UnityEvent<TurretUpgradeInfo> { }

/// <summary> �̺�Ʈ ���� �������� ������ �̺�Ʈ �Ŵ��� </summary>
public class EventManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static EventManager Instance;
    // �̺�Ʈ ��ųʸ�
    private Dictionary<string, TurretUpgradeEvent> enhancementEventDictionary;

    void Awake()
    {
        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            enhancementEventDictionary = new Dictionary<string, TurretUpgradeEvent>();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    /// <summary> �̺�Ʈ ������ �߰� </summary>
    public static void StartListening(string eventName, UnityAction<TurretUpgradeInfo> listener)
    {
        TurretUpgradeEvent thisEvent = null;
        if (Instance.enhancementEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TurretUpgradeEvent();
            thisEvent.AddListener(listener);
            Instance.enhancementEventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary> �̺�Ʈ ������ ���� </summary>
    public static void StopListening(string eventName, UnityAction<TurretUpgradeInfo> listener)
    {
        if (Instance == null) return;
        TurretUpgradeEvent thisEvent = null;
        if (Instance.enhancementEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary> �̺�Ʈ Ʈ���� </summary>
    public static void TriggerEnhancementEvent(string eventName, TurretUpgradeInfo enhancementData)
    {
        TurretUpgradeEvent thisEvent = null;
        if (Instance.enhancementEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(enhancementData);
        }
    }
}

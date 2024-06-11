using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeHandler : MonoBehaviour
{
    private Dictionary<int, List<ITurretUpgradeEvent>> stageEvents;
    private List<ITurretUpgradeEvent> currentEvents;
    private Coroutine upgradeCoroutine;

    void Start()
    {
        InitializeStageEvents();
        SetStageEvents(1);  // �ʱ� �������� ����
    }

    /// <summary> ���������� ����� �̺�Ʈ�� �ʱ�ȭ </summary>
    void InitializeStageEvents()
    {
        stageEvents = new Dictionary<int, List<ITurretUpgradeEvent>>()
        {
            {0, new List<ITurretUpgradeEvent>{ new BulletTurretSplit(), new BulletTurretRemoveSplit(), new BulletTurretCountIncrease(), new BulletTurretSpeedIncrease(), new BulletTurretSizeIncrease() }},
            {1, new List<ITurretUpgradeEvent>{ new LaserTurretLifeTimeIncrease(), new LaserTurretCountIncrease(), new LaserTurretSpeedIncrease(), new LaserTurretSizeIncrease() }},
            {2, new List<ITurretUpgradeEvent>{ new RocketTurretLifeTimeIncrease(), new RocketTurretCountIncrease(), new RocketTurretSpeedIncrease(), new RocketTurretSizeIncrease() }},
            {3, new List<ITurretUpgradeEvent>{ new MortarTurretSplit(), new MortarTurretRemoveSplit(), new MortarTurretCountIncrease(), new MortarTurretSpeedIncrease(), new MortarTurretSizeIncrease() }},
            {4, new List<ITurretUpgradeEvent>{
                new BulletTurretSplit(), new BulletTurretRemoveSplit(), new BulletTurretCountIncrease(), new BulletTurretSpeedIncrease(), new BulletTurretSizeIncrease(),
                new LaserTurretLifeTimeIncrease(), new LaserTurretCountIncrease(), new LaserTurretSpeedIncrease(), new LaserTurretSizeIncrease(),
                new RocketTurretLifeTimeIncrease(), new RocketTurretCountIncrease(), new RocketTurretSpeedIncrease(), new RocketTurretSizeIncrease(),
                new MortarTurretSplit(), new MortarTurretRemoveSplit(), new MortarTurretCountIncrease(), new MortarTurretSpeedIncrease(), new MortarTurretSizeIncrease()
            }},
            // �ٸ� �������� �̺�Ʈ �߰�
        };
    }

    /// <summary> ���������� ���� ������ �̺�Ʈ ����� ���� </summary>
    public void SetStageEvents(int stageNumber)
    {
        if (stageEvents.TryGetValue(stageNumber, out var events))
        {
            currentEvents = events;
        }
        else
        {
            Debug.LogError("Stage number out of range: " + stageNumber);
            currentEvents = new List<ITurretUpgradeEvent>();  // �� ������� ����
        }
    }

    /// <summary> ���� ���׷��̵� �ڷ�ƾ ���� </summary>
    public void StartRandomUpgrades(float interval)
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
        }
        upgradeCoroutine = StartCoroutine(TriggerRandomUpgrade(interval));
    }

    /// <summary> interval ���� �������� �ͷ� ���׷��̵� ���� </summary>
    private IEnumerator TriggerRandomUpgrade(float interval)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(interval);
            if (currentEvents != null && currentEvents.Count > 0)
            {
                int eventIndex = Random.Range(0, currentEvents.Count);
                currentEvents[eventIndex].ExecuteEvent();
            }
        }
    }

    /// <summary> ���� ���׷��̵� �ڷ�ƾ ���� </summary>
    public void StopRandomUpgrades()
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
            upgradeCoroutine = null;
        }
    }
}

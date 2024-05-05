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

    void InitializeStageEvents()
    {
        // ���������� �̺�Ʈ�� �ʱ�ȭ
        stageEvents = new Dictionary<int, List<ITurretUpgradeEvent>>()
        {
            {0, new List<ITurretUpgradeEvent>{ new BulletTurretSplit(), new BulletTurretCountIncrease(), new BulletTurretSpeedIncrease(), new BulletTurretSizeIncrease() }},
            {1, new List<ITurretUpgradeEvent>{ new LaserTurretLifeTimeIncrease(), new LaserTurretCountIncrease(), new LaserTurretSizeIncrease() }},
            {2, new List<ITurretUpgradeEvent>{ new RocketTurretLifeTimeIncrease(), new RocketTurretCountIncrease(), new RocketTurretSpeedIncrease(), new RocketTurretSizeIncrease() }},
            {3, new List<ITurretUpgradeEvent>{ new MortarTurretSplit(), new MortarTurretCountIncrease(), new MortarTurretSpeedIncrease(), new MortarTurretSizeIncrease() }},
            {4, new List<ITurretUpgradeEvent>{ }},
            // �ٸ� �������� �̺�Ʈ �߰�
        };
    }

    public void SetStageEvents(int stageNumber)
    {
        // ���������� ���� ������ �̺�Ʈ ����� ����
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

    public void StartRandomUpgrades(float interval)
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
        }
        upgradeCoroutine = StartCoroutine(TriggerRandomUpgrade(interval));
    }

    private IEnumerator TriggerRandomUpgrade(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (currentEvents != null && currentEvents.Count > 0)
            {
                int eventIndex = Random.Range(0, currentEvents.Count);
                currentEvents[eventIndex].ExecuteEvent();
            }
        }
    }

    public void StopRandomUpgrades()
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
            upgradeCoroutine = null;
        }
    }
}

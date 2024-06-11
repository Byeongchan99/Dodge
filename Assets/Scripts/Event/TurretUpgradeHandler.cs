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
        SetStageEvents(1);  // 초기 스테이지 설정
    }

    /// <summary> 스테이지별 사용할 이벤트를 초기화 </summary>
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
            // 다른 스테이지 이벤트 추가
        };
    }

    /// <summary> 스테이지에 따라 적용할 이벤트 목록을 설정 </summary>
    public void SetStageEvents(int stageNumber)
    {
        if (stageEvents.TryGetValue(stageNumber, out var events))
        {
            currentEvents = events;
        }
        else
        {
            Debug.LogError("Stage number out of range: " + stageNumber);
            currentEvents = new List<ITurretUpgradeEvent>();  // 빈 목록으로 설정
        }
    }

    /// <summary> 랜덤 업그레이드 코루틴 시작 </summary>
    public void StartRandomUpgrades(float interval)
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
        }
        upgradeCoroutine = StartCoroutine(TriggerRandomUpgrade(interval));
    }

    /// <summary> interval 마다 랜덤으로 터렛 업그레이드 실행 </summary>
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

    /// <summary> 랜덤 업그레이드 코루틴 중지 </summary>
    public void StopRandomUpgrades()
    {
        if (upgradeCoroutine != null)
        {
            StopCoroutine(upgradeCoroutine);
            upgradeCoroutine = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    /****************************************************************************
                                  private Fields
    ****************************************************************************/
    /// <summary> 소환 위치 Transform 배열 </summary>
    [SerializeField] Transform[] spawnPositions;
    /// <summary> 각 위치의 사용 가능 여부를 나타내는 bool 배열 </summary>
    [SerializeField] private bool[] isAvailableSpawnPosition;
    /// <summary> 소환할 터렛 프리팹 리스트 </summary>
    [SerializeField] List<GameObject> turretPrefabs;
    /// <summary> 터렛 종류별 소환 레벨 </summary>
    [SerializeField] List<int> turretSpawnLevels;
    /// <summary> 터렛 종류별 소환 확률 </summary>
    [SerializeField] List<float> turretSpawnChances;
    /// <summary> 터렛 종류별 초기 소환 쿨타임 </summary>
    [SerializeField] List<float> turretInitialSpawnCooltimes;
    /// <summary> 터렛 종류별 소환 쿨타임 </summary>
    [SerializeField] List<float> turretSpawnCooltimes;

    /// <summary> 스크립터블 오브젝트에서 로드한 터렛 소환 데이터 </summary>
    [SerializeField] TurretData currentEventData;
    [SerializeField] List<int> eventSpawnLevels = new List<int>();
    [SerializeField] List<float> eventSpawnCooltimePercents = new List<float>();

    private float _nextSpawnTime = 0f;
    private bool _isSpawning = false; // 현재 소환 중인지 여부를 나타내는 플래그

    private bool _isBulletSplitActive = false; // 분열 총알 활성화 여부

    /****************************************************************************
                                   Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        Init();
    }

    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void Start()
    {
        StartCoroutine(SpawnTurretRoutine());
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    void Init()
    {
        // 모든 소환 위치를 사용 가능 상태로 초기화
        isAvailableSpawnPosition = new bool[spawnPositions.Length];
        for (int i = 0; i < isAvailableSpawnPosition.Length; i++)
        {
            isAvailableSpawnPosition[i] = true;
        }

        // 이벤트 이름에 따른 스크립터블 오브젝트 데이터 로드
        currentEventData = TurretDataManager.Instance.GetSpawnerDataForEvent("Init");

        // 터렛 종류별 초기 소환 쿨타임 적용
        for (int i = 0; i < turretInitialSpawnCooltimes.Count; i++)
        {
            turretSpawnCooltimes[i] = turretInitialSpawnCooltimes[i];
        }

        // 터렛 종류별 데이터 초기화
        SettingTurretSpawnerDatas(currentEventData);
    }

    /// <summary> 이벤트 적용 </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        switch (enhancement.turretType)
        {
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret의 업그레이드 처리를 위한 내부 switch 문
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret 분열 총알 업그레이드 처리
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.RemoveSplit:
                        _isBulletSplitActive = false;
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeIncrease:
                        // Bullet Turret 분열 총알 업그레이드 처리
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.InductionUpgrade:
                        // Bullet Turret 분열 총알 업그레이드 처리
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret 분열 총알 업그레이드 처리
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // 개수 증가 처리
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // 속도 증가 처리
                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }
    }

    /// <summary> 이벤트에 따른 터렛 소환 데이터 조정 </summary>
    /// <param name="eventName"> 적용할 이벤트 이름 </param>
    void AdjustEvent(string eventName)
    {
        // 이벤트 이름에 따른 스크립터블 오브젝트 데이터 로드
        currentEventData = TurretDataManager.Instance.GetSpawnerDataForEvent(eventName);
        SettingTurretSpawnerDatas(currentEventData);
    }

    /// <summary> 스크립터블 오브젝트에서 터렛 소환 데이터 적용 </summary>
    void SettingTurretSpawnerDatas(TurretData turretSpawnerData)
    {
        eventSpawnLevels.Clear();
        eventSpawnCooltimePercents.Clear();

        // 터렛 종류별 데이터를 리스트에 추가
        for (int i = 0; i < turretSpawnerData.turretDatas.Count; i++)
        {
            eventSpawnLevels.Add(turretSpawnerData.turretDatas[i].spawnLevel);
            eventSpawnCooltimePercents.Add(turretSpawnerData.turretDatas[i].spawnCooldownPercent);
        }

        // 터렛 종류별 데이터 적용
        // 터렛 종류별 소환 레벨 적용
        SettingTurretSpawnLevel(eventSpawnLevels.ToArray());
        // 적용된 터렛 종류별 소환 레벨로 터렛 종류별 소환 확률 적용
        SettingTurretSpawnChances(turretSpawnLevels.ToArray());
        // 터렛 종류별 소환 쿨타임 적용
        SettingTurretSpawnCooltimes(eventSpawnCooltimePercents.ToArray());
    }

    /// <summary> 터렛 종류별 소환 레벨 변경 </summary>
    /// <param name="levels"> 포탑 소환 레벨을 조정할 값 배열 </param>
    void SettingTurretSpawnLevel(params int[] levels)
    {
        // 포탑 소환 레벨을 조정할 값 배열의 길이와 포탑 소환 레벨 배열의 길이가 일치하는지 확인
        if (levels.Length != turretSpawnLevels.Count)
        {
            Debug.LogError("포탑 소환 레벨을 조정할 값 배열의 길이와 포탑 소환 레벨 배열의 길이가 다름" + levels.Length + " " + turretSpawnLevels.Count);
            return;
        }

        // 터렛 레벨 변경
        for (int i = 0; i < levels.Length; i++)
        {
            turretSpawnLevels[i] += levels[i];
        }
    }

    /// <summary> 터렛 종류별 선택 확률 변경 </summary>
    /// <param name="levels"> 각 터렛의 레벨 배열 </param>
    void SettingTurretSpawnChances(params int[] levels)
    {
        // 각 터렛의 레벨 배열의 길이와 터렛 소환 확률 배열의 길이가 일치하는지 확인
        if (levels.Length != turretSpawnChances.Count)
        {
            Debug.LogError("각 터렛의 레벨 배열의 길이와 터렛 소환 확률 배열의 길이가 다름");
            return;
        }

        // 레벨 합계 계산
        int levelSum = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            levelSum += levels[i];
        }

        // 레벨 합계가 0인 경우, 모든 확률을 동일하게 설정하여 에러 방지
        if (levelSum == 0)
        {
            Debug.LogError("레벨 합계가 0. 모든 확률 동일하게 설정");
            float equalChance = 100f / levels.Length;
            for (int i = 0; i < levels.Length; i++)
            {
                turretSpawnChances[i] = equalChance;
            }
            return;
        }

        // 각 터렛의 소환 확률 계산
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // 검증을 위한 총 확률 합계 계산
        for (int i = 0; i < levels.Length; i++)
        {
            turretSpawnChances[i] = chancePerLevel * levels[i];
            totalPercentage += turretSpawnChances[i];
        }

        // 확률의 총합이 100%에 가까운지 확인 (부동소수점 연산으로 인한 작은 오차 허용)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"확률 총합이 100%가 아님. 총합: {totalPercentage}%");
        }
    }

    /// <summary> 터렛 종류별 소환 쿨타임 변경 </summary>
    /// <param name="cooltimePercents"> 쿨타임을 조정할 퍼센트 값 배열 </param>
    void SettingTurretSpawnCooltimes(params float[] cooltimePercents)
    {
        // 쿨타임을 조정할 퍼센트 값 배열의 길이가 터렛 소환 쿨타임 배열의 길이와 일치하는지 확인
        if (cooltimePercents.Length != turretSpawnCooltimes.Count)
        {
            Debug.LogError("쿨타임을 조정할 퍼센트 값 배열의 길이와 터렛 소환 쿨타임 배열의 길이가 다름");
            return;
        }

        // 터렛 쿨타임 변경
        for (int i = 0; i < cooltimePercents.Length; i++)
        {
            // 입력된 비율 값이 음수인지 확인
            if (cooltimePercents[i] < 0)
            {
                Debug.LogWarning($"{i}번째 터렛 쿨타임 비율이 음수이면 쿨타임 증가");
            }
            // 초기 쿨타임의 비율만큼 쿨타임 감소
            float value = turretInitialSpawnCooltimes[i] * cooltimePercents[i];

            // 쿨타임에 비율 적용
            turretSpawnCooltimes[i] -= value;
        }
    }

    /// <summary> 터렛 소환 코루틴 </summary>
    IEnumerator SpawnTurretRoutine()
    {
        while (true) // 무한 루프를 통해 게임 동안 지속적으로 터렛 소환
        {
            if (!_isSpawning) // 소환 중이 아닐 때만 새로운 터렛 소환 시도
            {
                _isSpawning = true; // 소환 시작 플래그 설정
                SpawnTurret();
                // 선택된 터렛의 쿨타임에 따라 대기
                yield return new WaitForSeconds(_nextSpawnTime);
                _isSpawning = false; // 소환 완료 후 플래그 재설정
            }
            yield return null; // 다음 프레임까지 대기
        }
    }

    /// <summary> 터렛 소환 </summary>
    void SpawnTurret()
    {
        // 터렛 종류 선택
        GameObject turretToSpawn = ChooseTurretType();
        // 터렛 위치 선택
        int spawnPositionIndex = ChooseSpawnPosition();
        if (spawnPositionIndex == -1) return; // 사용 가능한 위치가 없는 경우 소환하지 않음

        Transform spawnPosition = spawnPositions[spawnPositionIndex];

        // 오브젝트 풀에서 터렛 가져오기
        BaseTurret turret = TurretPoolManager.Instance.Get(turretToSpawn.name);

        if (turret != null)
        {
            turret.gameObject.SetActive(true);
            turret.transform.position = spawnPosition.position;
            turret.transform.rotation = Quaternion.identity;

            // 1번부터 4번 스폰 포인트에서는 터렛 스프라이트를 반대로 설정
            if (spawnPositionIndex >= 0 && spawnPositionIndex <= 3)
            {
                Vector3 currentScale = turret.transform.localScale;
                currentScale.x *= -1;
                turret.transform.localScale = currentScale;
            }

            // spawnPoint 설정
            turret.spawnPointIndex = spawnPositionIndex;
            turret.spawner = this;

            if (turret is BulletTurret bulletTurret)
            {
                if (_isBulletSplitActive)
                {
                    // 분열 총알 이벤트가 활성화된 경우(임시)
                    Debug.Log("분열 총알 적용");
                    bulletTurret.ChangeProjectile(1); // 분열 총알 프리팹 적용
                }
                else
                {
                    bulletTurret.ChangeProjectile(0); // 기본 총알 프리팹 적용
                }
            }

            _nextSpawnTime = ChooseCooldown(turretToSpawn); // 다음 소환까지의 시간 설정
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {turretToSpawn} from pool.");
        }
    }

    /// <summary> 터렛 종류 선택 </summary>
    GameObject ChooseTurretType()
    {
        float randomChance = Random.value; // 0과 1 사이의 랜덤한 값
        float currentChance = 0f;

        for (int i = 0; i < turretPrefabs.Count; i++)
        {
            currentChance += turretSpawnChances[i]; // 누적 확률 업데이트
            if (randomChance <= currentChance)
            {
                return turretPrefabs[i]; // 조건을 만족하는 터렛 선택
            }
        }
        return null;
    }

    /// <summary> 소환 위치 선택 </summary>
    int ChooseSpawnPosition()
    {
        List<int> availableSpawnPositions = new List<int>();

        // 사용 가능한 위치 인덱스를 리스트에 추가
        for (int i = 0; i < isAvailableSpawnPosition.Length; i++)
        {
            if (isAvailableSpawnPosition[i])
            {
                availableSpawnPositions.Add(i);
            }
        }

        // 사용 가능한 위치가 없는 경우 null 반환
        if (availableSpawnPositions.Count == 0) return -1;

        // 사용 가능한 위치 중에서 랜덤하게 하나 선택
        int selectedIndex = availableSpawnPositions[Random.Range(0, availableSpawnPositions.Count)];
        // 선택된 위치를 사용 불가능 상태로 변경
        isAvailableSpawnPosition[selectedIndex] = false;

        return selectedIndex;
    }

    /// <summary> 소환한 터렛의 쿨타임 반환 </summary>
    float ChooseCooldown(GameObject turret)
    {
        int index = turretPrefabs.IndexOf(turret);
        return turretSpawnCooltimes[index];
    }

    /****************************************************************************
                                  public Methods
    ****************************************************************************/
    /// <summary> 소환 위치 반납 </summary>
    /// 터렛이 비활성화될 때 호출하여 해당 위치를 다시 사용 가능 상태로 변경
    public void SetPositionAvailable(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < isAvailableSpawnPosition.Length)
        {
            isAvailableSpawnPosition[positionIndex] = true;
        }
    }
}

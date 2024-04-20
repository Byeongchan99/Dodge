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

    private float _nextSpawnTime = 0f;
    private bool _isSpawning = false; // 현재 소환 중인지 여부를 나타내는 플래그
    /****************************************************************************
                                   Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        Init();
    }

    void Start()
    {
        StartCoroutine(SpawnTurretRoutine());
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
            // 위치 설정
            turret.transform.position = spawnPosition.position;
            turret.transform.rotation = Quaternion.identity;

            // spawnIndex가 0일 때 시계 방향으로 90도 회전
            if (spawnPositionIndex == 0)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            // 1번부터 3번 스폰 포인트에서는 터렛 스프라이트를 반대로 설정
            else if (spawnPositionIndex >= 1 && spawnPositionIndex <= 3)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (spawnPositionIndex == 4)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, 90);
            }

            // spawnPoint 설정
            turret.spawnPointIndex = spawnPositionIndex;
            turret.spawner = this;

            // 터렛 정보
            turret.turretIndex = turretPrefabs.IndexOf(turretToSpawn);
            _nextSpawnTime = ChooseCooldown(turretToSpawn); // 다음 소환까지의 시간 설정

            turret.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {turretToSpawn} from pool.");
        }
    }

    /// <summary> 터렛 종류 선택 </summary>
    GameObject ChooseTurretType()
    {
        float randomChance = Random.value * 100; // 0과 1 사이의 랜덤한 값
        float currentChance = 0f;

        for (int i = 0; i < turretPrefabs.Count; i++)
        {
            //Debug.Log(i + "번째 터렛 소환 확률: " + StatDataManager.Instance.currentStatData.turretSpawnerDatas[i].spawnChance);
            currentChance += StatDataManager.Instance.currentStatData.turretSpawnerDatas[i].spawnChance; // 누적 확률 업데이트
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
        return StatDataManager.Instance.currentStatData.turretSpawnerDatas[index].spawnCooldown;
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

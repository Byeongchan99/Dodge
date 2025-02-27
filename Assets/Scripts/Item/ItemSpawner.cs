using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    /****************************************************************************
                                    private Fields
    ****************************************************************************/
    /// <summary> 소환할 아이템 프리팹 리스트 </summary>
    [SerializeField] List<GameObject> itemPrefabs;
    /// <summary> 아이템 소환 위치 범위 </summary>
    [SerializeField] Vector2 spawnPositionRange;

    private float _nextSpawnTime = 10f;
    private bool _isSpawning = false; // 현재 소환 중인지 여부를 나타내는 플래그

    private Coroutine spawnCoroutine; // 현재 실행 중인 소환 코루틴 인스턴스

    /****************************************************************************
                                   Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        Init();
    }

    void Start()
    {
        //StartCoroutine(SpawnItemRoutine());
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    void Init()
    {
        _isSpawning = false;
        _nextSpawnTime = 10f; // 초기 소환 시간 설정
    }

    /// <summary> 아이템 소환 코루틴 </summary>
    IEnumerator SpawnItemRoutine()
    {
        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < _nextSpawnTime)
            {
                if (!GameManager.Instance.isPaused)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                }
                yield return null;
            }

            if (!GameManager.Instance.isPaused && !_isSpawning)
            {
                _isSpawning = true;
                SpawnItem();
                _isSpawning = false;
            }
        }

        /*
        // 아이템 소환의 경우 실제 시간 적용
        yield return new WaitForSecondsRealtime(_nextSpawnTime);
        while (true) // 무한 루프를 통해 게임 동안 지속적으로 아이템 소환
        {
            if (GameManager.Instance.isPaused)
            {
                Debug.Log("isPaused Item: " + GameManager.Instance.isPaused);
                yield return null;
            }

            if (!_isSpawning) // 소환 중이 아닐 때만 새로운 아이템 소환 시도
            {
                _isSpawning = true; // 소환 시작 플래그 설정
                SpawnItem();
                // 선택된 아이템의 쿨타임에 따라 대기
                yield return new WaitForSecondsRealtime(_nextSpawnTime);
                _isSpawning = false; // 소환 완료 후 플래그 재설정
            }
            yield return null; // 다음 프레임까지 대기
        }
        */
    }

    /// <summary> 아이템 소환 </summary>
    void SpawnItem()
    {
        // 아이템 종류 선택
        GameObject itemToSpawn = ChooseItemType();
        // 아이템 위치 선택
        Vector2 spawnPosition = ChooseSpawnPosition();

        // 오브젝트 풀에서 아이템 가져오기
        BaseItem item = ItemPoolManager.Instance.Get(itemToSpawn.name);

        if (item != null)
        {
            // 아이템 활성화 및 위치 설정
            item.transform.position = spawnPosition;
            item.transform.rotation = Quaternion.identity;

            _nextSpawnTime = ChooseCooldown(itemToSpawn); // 다음 소환까지의 시간 설정

            item.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Failed to get item of type {itemToSpawn} from pool.");
        }
    }

    /// <summary> 아이템 종류 선택 </summary>
    GameObject ChooseItemType()
    {
        float randomChance = Random.value * 100; // 0과 1 사이의 랜덤한 값
        float currentChance = 0f;

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            //Debug.Log(i + "번째 아이템 소환 확률: " + StatDataManager.Instance.currentStatData.itemDatas[i].spawnChance);
            currentChance += StatDataManager.Instance.currentStatData.itemDatas[i].spawnChance; // 누적 확률 업데이트
            if (randomChance <= currentChance)
            {
                return itemPrefabs[i]; // 조건을 만족하는 아이템 선택
            }
        }
        return null;
    }

    /// <summary> 소환 위치 선택 </summary>
    Vector2 ChooseSpawnPosition()
    {
        Vector2 spawnPosition;
        float minDistanceFromPlayer = 2.0f; // 플레이어와의 최소 거리 설정

        do
        {
            // 소환 범위 내에서 랜덤으로 소환 위치 선택
            spawnPosition = new Vector2(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y - 0.8f, spawnPositionRange.y - 0.8f));
        }
        while (Vector2.Distance(spawnPosition, PlayerStat.Instance.currentPosition.position) < minDistanceFromPlayer);

        return spawnPosition;
    }

    /// <summary> 소환한 아이템의 쿨타임 반환 </summary>
    float ChooseCooldown(GameObject item)
    {
        int index = itemPrefabs.IndexOf(item);
        return StatDataManager.Instance.currentStatData.itemDatas[index].spawnCooldown;
    }

    /****************************************************************************
                                  public Methods
    ****************************************************************************/
    // 아이템 스폰 시작
    public void StartSpawn()
    {
        if (spawnCoroutine == null)
        {
            Init();
            spawnCoroutine = StartCoroutine(SpawnItemRoutine());
        }
    }

    // 아이템 스폰 중지
    public void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}

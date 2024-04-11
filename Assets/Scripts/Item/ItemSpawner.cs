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
        StartCoroutine(SpawnItemRoutine());
    }

    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    void Init()
    {
        
    }

    /// <summary> 아이템 소환 코루틴 </summary>
    IEnumerator SpawnItemRoutine()
    {
        while (true) // 무한 루프를 통해 게임 동안 지속적으로 아이템 소환
        {
            if (!_isSpawning) // 소환 중이 아닐 때만 새로운 아이템 소환 시도
            {
                _isSpawning = true; // 소환 시작 플래그 설정
                SpawnItem();
                // 선택된 아이템의 쿨타임에 따라 대기
                yield return new WaitForSeconds(_nextSpawnTime);
                _isSpawning = false; // 소환 완료 후 플래그 재설정
            }
            yield return null; // 다음 프레임까지 대기
        }
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
            item.gameObject.SetActive(true);
            item.transform.position = spawnPosition;
            item.transform.rotation = Quaternion.identity;

            _nextSpawnTime = ChooseCooldown(itemToSpawn); // 다음 소환까지의 시간 설정
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {itemToSpawn} from pool.");
        }
    }

    /// <summary> 아이템 종류 선택 </summary>
    GameObject ChooseItemType()
    {
        float randomChance = Random.value; // 0과 1 사이의 랜덤한 값
        float currentChance = 0f;

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
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
        // 소환 범위 내에서 랜덤으로 소환 위치 선택
        return new Vector2(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y));
    }

    /// <summary> 소환한 아이템의 쿨타임 반환 </summary>
    float ChooseCooldown(GameObject item)
    {
        int index = itemPrefabs.IndexOf(item);
        return StatDataManager.Instance.currentStatData.itemDatas[index].spawnCooldown;
    }
}

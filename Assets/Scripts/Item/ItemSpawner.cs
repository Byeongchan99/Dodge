using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    /****************************************************************************
                                    private Fields
    ****************************************************************************/
    /// <summary> ��ȯ�� ������ ������ ����Ʈ </summary>
    [SerializeField] List<GameObject> itemPrefabs;
    /// <summary> ������ ��ȯ ��ġ ���� </summary>
    [SerializeField] Vector2 spawnPositionRange;

    private float _nextSpawnTime = 0f;
    private bool _isSpawning = false; // ���� ��ȯ ������ ���θ� ��Ÿ���� �÷���
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
    /// <summary> �ʱ�ȭ </summary>
    void Init()
    {
        
    }

    /// <summary> ������ ��ȯ �ڷ�ƾ </summary>
    IEnumerator SpawnItemRoutine()
    {
        while (true) // ���� ������ ���� ���� ���� ���������� ������ ��ȯ
        {
            if (!_isSpawning) // ��ȯ ���� �ƴ� ���� ���ο� ������ ��ȯ �õ�
            {
                _isSpawning = true; // ��ȯ ���� �÷��� ����
                SpawnItem();
                // ���õ� �������� ��Ÿ�ӿ� ���� ���
                yield return new WaitForSeconds(_nextSpawnTime);
                _isSpawning = false; // ��ȯ �Ϸ� �� �÷��� �缳��
            }
            yield return null; // ���� �����ӱ��� ���
        }
    }

    /// <summary> ������ ��ȯ </summary>
    void SpawnItem()
    {
        // ������ ���� ����
        GameObject itemToSpawn = ChooseItemType();
        // ������ ��ġ ����
        Vector2 spawnPosition = ChooseSpawnPosition();

        // ������Ʈ Ǯ���� ������ ��������
        BaseItem item = ItemPoolManager.Instance.Get(itemToSpawn.name);

        if (item != null)
        {
            // ������ Ȱ��ȭ �� ��ġ ����
            item.gameObject.SetActive(true);
            item.transform.position = spawnPosition;
            item.transform.rotation = Quaternion.identity;

            _nextSpawnTime = ChooseCooldown(itemToSpawn); // ���� ��ȯ������ �ð� ����
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {itemToSpawn} from pool.");
        }
    }

    /// <summary> ������ ���� ���� </summary>
    GameObject ChooseItemType()
    {
        float randomChance = Random.value; // 0�� 1 ������ ������ ��
        float currentChance = 0f;

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            currentChance += StatDataManager.Instance.currentStatData.itemDatas[i].spawnChance; // ���� Ȯ�� ������Ʈ
            if (randomChance <= currentChance)
            {
                return itemPrefabs[i]; // ������ �����ϴ� ������ ����
            }
        }
        return null;
    }

    /// <summary> ��ȯ ��ġ ���� </summary>
    Vector2 ChooseSpawnPosition()
    {
        // ��ȯ ���� ������ �������� ��ȯ ��ġ ����
        return new Vector2(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y));
    }

    /// <summary> ��ȯ�� �������� ��Ÿ�� ��ȯ </summary>
    float ChooseCooldown(GameObject item)
    {
        int index = itemPrefabs.IndexOf(item);
        return StatDataManager.Instance.currentStatData.itemDatas[index].spawnCooldown;
    }
}

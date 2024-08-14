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

    private float _nextSpawnTime = 10f;
    private bool _isSpawning = false; // ���� ��ȯ ������ ���θ� ��Ÿ���� �÷���

    private Coroutine _spawnCoroutine; // ���� ���� ���� ��ȯ �ڷ�ƾ �ν��Ͻ�

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
    /// <summary> �ʱ�ȭ </summary>
    void Init()
    {
        _isSpawning = false;
        _nextSpawnTime = 10f;
    }

    /// <summary> ������ ��ȯ �ڷ�ƾ </summary>
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
        // ������ ��ȯ�� ��� ���� �ð� ����
        yield return new WaitForSecondsRealtime(_nextSpawnTime);
        while (true) // ���� ������ ���� ���� ���� ���������� ������ ��ȯ
        {
            if (GameManager.Instance.isPaused)
            {
                Debug.Log("isPaused Item: " + GameManager.Instance.isPaused);
                yield return null;
            }

            if (!_isSpawning) // ��ȯ ���� �ƴ� ���� ���ο� ������ ��ȯ �õ�
            {
                _isSpawning = true; // ��ȯ ���� �÷��� ����
                SpawnItem();
                // ���õ� �������� ��Ÿ�ӿ� ���� ���
                yield return new WaitForSecondsRealtime(_nextSpawnTime);
                _isSpawning = false; // ��ȯ �Ϸ� �� �÷��� �缳��
            }
            yield return null; // ���� �����ӱ��� ���
        }
        */
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
            item.transform.position = spawnPosition;
            item.transform.rotation = Quaternion.identity;

            _nextSpawnTime = ChooseCooldown(itemToSpawn); // ���� ��ȯ������ �ð� ����

            item.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Failed to get item of type {itemToSpawn} from pool.");
        }
    }

    /// <summary> ������ ���� ���� </summary>
    GameObject ChooseItemType()
    {
        float randomChance = Random.value * 100; // 0�� 1 ������ ������ ��
        float currentChance = 0f;

        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            //Debug.Log(i + "��° ������ ��ȯ Ȯ��: " + StatDataManager.Instance.currentStatData.itemDatas[i].spawnChance);
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
        return new Vector2(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y - 0.8f, spawnPositionRange.y - 0.8f));
    }

    /// <summary> ��ȯ�� �������� ��Ÿ�� ��ȯ </summary>
    float ChooseCooldown(GameObject item)
    {
        int index = itemPrefabs.IndexOf(item);
        return StatDataManager.Instance.currentStatData.itemDatas[index].spawnCooldown;
    }

    /****************************************************************************
                                  public Methods
    ****************************************************************************/
    public void StartSpawn()
    {
        if (_spawnCoroutine == null)
        {
            Init();
            _spawnCoroutine = StartCoroutine(SpawnItemRoutine());
        }
    }

    public void StopSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }
}

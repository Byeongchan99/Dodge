using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    /****************************************************************************
                                  private Fields
    ****************************************************************************/
    /// <summary> ��ȯ ��ġ Transform �迭 </summary>
    [SerializeField] Transform[] spawnPositions;
    /// <summary> �� ��ġ�� ��� ���� ���θ� ��Ÿ���� bool �迭 </summary>
    [SerializeField] private bool[] isAvailableSpawnPosition;
    /// <summary> ��ȯ�� �ͷ� ������ ����Ʈ </summary>
    [SerializeField] List<GameObject> turretPrefabs;

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
        StartCoroutine(SpawnTurretRoutine());
    }
    /****************************************************************************
                                    private Methods
    ****************************************************************************/
    /// <summary> �ʱ�ȭ </summary>
    void Init()
    {
        // ��� ��ȯ ��ġ�� ��� ���� ���·� �ʱ�ȭ
        isAvailableSpawnPosition = new bool[spawnPositions.Length];
        for (int i = 0; i < isAvailableSpawnPosition.Length; i++)
        {
            isAvailableSpawnPosition[i] = true;
        }
    }

    /// <summary> �ͷ� ��ȯ �ڷ�ƾ </summary>
    IEnumerator SpawnTurretRoutine()
    {
        while (true) // ���� ������ ���� ���� ���� ���������� �ͷ� ��ȯ
        {
            if (!_isSpawning) // ��ȯ ���� �ƴ� ���� ���ο� �ͷ� ��ȯ �õ�
            {
                _isSpawning = true; // ��ȯ ���� �÷��� ����
                SpawnTurret();
                // ���õ� �ͷ��� ��Ÿ�ӿ� ���� ���
                yield return new WaitForSeconds(_nextSpawnTime);
                _isSpawning = false; // ��ȯ �Ϸ� �� �÷��� �缳��
            }
            yield return null; // ���� �����ӱ��� ���
        }
    }

    /// <summary> �ͷ� ��ȯ </summary>
    void SpawnTurret()
    {
        // �ͷ� ���� ����
        GameObject turretToSpawn = ChooseTurretType();
        // �ͷ� ��ġ ����
        int spawnPositionIndex = ChooseSpawnPosition();
        if (spawnPositionIndex == -1) return; // ��� ������ ��ġ�� ���� ��� ��ȯ���� ����

        Transform spawnPosition = spawnPositions[spawnPositionIndex];

        // ������Ʈ Ǯ���� �ͷ� ��������
        BaseTurret turret = TurretPoolManager.Instance.Get(turretToSpawn.name);

        if (turret != null)
        {
            // ��ġ ����
            turret.transform.position = spawnPosition.position;
            turret.transform.rotation = Quaternion.identity;

            // spawnIndex�� 0�� �� �ð� �������� 90�� ȸ��
            if (spawnPositionIndex == 0)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            // 1������ 3�� ���� ����Ʈ������ �ͷ� ��������Ʈ�� �ݴ�� ����
            else if (spawnPositionIndex >= 1 && spawnPositionIndex <= 3)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (spawnPositionIndex == 4)
            {
                turret.transform.rotation = Quaternion.Euler(0, 0, 90);
            }

            // spawnPoint ����
            turret.spawnPointIndex = spawnPositionIndex;
            turret.spawner = this;

            // �ͷ� ����
            turret.turretIndex = turretPrefabs.IndexOf(turretToSpawn);
            _nextSpawnTime = ChooseCooldown(turretToSpawn); // ���� ��ȯ������ �ð� ����

            turret.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {turretToSpawn} from pool.");
        }
    }

    /// <summary> �ͷ� ���� ���� </summary>
    GameObject ChooseTurretType()
    {
        float randomChance = Random.value * 100; // 0�� 1 ������ ������ ��
        float currentChance = 0f;

        for (int i = 0; i < turretPrefabs.Count; i++)
        {
            //Debug.Log(i + "��° �ͷ� ��ȯ Ȯ��: " + StatDataManager.Instance.currentStatData.turretSpawnerDatas[i].spawnChance);
            currentChance += StatDataManager.Instance.currentStatData.turretSpawnerDatas[i].spawnChance; // ���� Ȯ�� ������Ʈ
            if (randomChance <= currentChance)
            {
                return turretPrefabs[i]; // ������ �����ϴ� �ͷ� ����
            }
        }
        return null;
    }

    /// <summary> ��ȯ ��ġ ���� </summary>
    int ChooseSpawnPosition()
    {
        List<int> availableSpawnPositions = new List<int>();

        // ��� ������ ��ġ �ε����� ����Ʈ�� �߰�
        for (int i = 0; i < isAvailableSpawnPosition.Length; i++)
        {
            if (isAvailableSpawnPosition[i])
            {
                availableSpawnPositions.Add(i);
            }
        }

        // ��� ������ ��ġ�� ���� ��� null ��ȯ
        if (availableSpawnPositions.Count == 0) return -1;

        // ��� ������ ��ġ �߿��� �����ϰ� �ϳ� ����
        int selectedIndex = availableSpawnPositions[Random.Range(0, availableSpawnPositions.Count)];
        // ���õ� ��ġ�� ��� �Ұ��� ���·� ����
        isAvailableSpawnPosition[selectedIndex] = false;

        return selectedIndex;
    }

    /// <summary> ��ȯ�� �ͷ��� ��Ÿ�� ��ȯ </summary>
    float ChooseCooldown(GameObject turret)
    {
        int index = turretPrefabs.IndexOf(turret);
        return StatDataManager.Instance.currentStatData.turretSpawnerDatas[index].spawnCooldown;
    }

    /****************************************************************************
                                  public Methods
    ****************************************************************************/
    /// <summary> ��ȯ ��ġ �ݳ� </summary>
    /// �ͷ��� ��Ȱ��ȭ�� �� ȣ���Ͽ� �ش� ��ġ�� �ٽ� ��� ���� ���·� ����
    public void SetPositionAvailable(int positionIndex)
    {
        if (positionIndex >= 0 && positionIndex < isAvailableSpawnPosition.Length)
        {
            isAvailableSpawnPosition[positionIndex] = true;
        }
    }
}

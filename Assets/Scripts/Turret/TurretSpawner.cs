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
    /// <summary> �ͷ� ������ ��ȯ ���� </summary>
    [SerializeField] List<int> turretSpawnLevels;
    /// <summary> �ͷ� ������ ��ȯ Ȯ�� </summary>
    [SerializeField] List<float> turretSpawnChances;
    /// <summary> �ͷ� ������ �ʱ� ��ȯ ��Ÿ�� </summary>
    [SerializeField] List<float> turretInitialSpawnCooltimes;
    /// <summary> �ͷ� ������ ��ȯ ��Ÿ�� </summary>
    [SerializeField] List<float> turretSpawnCooltimes;

    /// <summary> ��ũ���ͺ� ������Ʈ���� �ε��� �ͷ� ��ȯ ������ </summary>
    [SerializeField] TurretData currentEventData;
    [SerializeField] List<int> eventSpawnLevels = new List<int>();
    [SerializeField] List<float> eventSpawnCooltimePercents = new List<float>();

    private float _nextSpawnTime = 0f;
    private bool _isSpawning = false; // ���� ��ȯ ������ ���θ� ��Ÿ���� �÷���

    private bool _isBulletSplitActive = false; // �п� �Ѿ� Ȱ��ȭ ����

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
    /// <summary> �ʱ�ȭ </summary>
    void Init()
    {
        // ��� ��ȯ ��ġ�� ��� ���� ���·� �ʱ�ȭ
        isAvailableSpawnPosition = new bool[spawnPositions.Length];
        for (int i = 0; i < isAvailableSpawnPosition.Length; i++)
        {
            isAvailableSpawnPosition[i] = true;
        }

        // �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ �ε�
        currentEventData = TurretDataManager.Instance.GetSpawnerDataForEvent("Init");

        // �ͷ� ������ �ʱ� ��ȯ ��Ÿ�� ����
        for (int i = 0; i < turretInitialSpawnCooltimes.Count; i++)
        {
            turretSpawnCooltimes[i] = turretInitialSpawnCooltimes[i];
        }

        // �ͷ� ������ ������ �ʱ�ȭ
        SettingTurretSpawnerDatas(currentEventData);
    }

    /// <summary> �̺�Ʈ ���� </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        switch (enhancement.turretType)
        {
            case TurretUpgradeInfo.TurretType.Bullet:
                // Bullet Turret�� ���׷��̵� ó���� ���� ���� switch ��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret �п� �Ѿ� ���׷��̵� ó��
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.RemoveSplit:
                        _isBulletSplitActive = false;
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Laser:
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.RemainTimeIncrease:
                        // Bullet Turret �п� �Ѿ� ���׷��̵� ó��
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Rocket:
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.InductionUpgrade:
                        // Bullet Turret �п� �Ѿ� ���׷��̵� ó��
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
            case TurretUpgradeInfo.TurretType.Mortar:
                // Mortar Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.ProjectileSplit:
                        // Bullet Turret �п� �Ѿ� ���׷��̵� ó��
                        _isBulletSplitActive = true;
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountIncrease:
                        // ���� ���� ó��
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedIncrease:
                        // �ӵ� ���� ó��
                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
                // ��Ÿ �ͷ� ������ ���� ó��
        }
    }

    /// <summary> �̺�Ʈ�� ���� �ͷ� ��ȯ ������ ���� </summary>
    /// <param name="eventName"> ������ �̺�Ʈ �̸� </param>
    void AdjustEvent(string eventName)
    {
        // �̺�Ʈ �̸��� ���� ��ũ���ͺ� ������Ʈ ������ �ε�
        currentEventData = TurretDataManager.Instance.GetSpawnerDataForEvent(eventName);
        SettingTurretSpawnerDatas(currentEventData);
    }

    /// <summary> ��ũ���ͺ� ������Ʈ���� �ͷ� ��ȯ ������ ���� </summary>
    void SettingTurretSpawnerDatas(TurretData turretSpawnerData)
    {
        eventSpawnLevels.Clear();
        eventSpawnCooltimePercents.Clear();

        // �ͷ� ������ �����͸� ����Ʈ�� �߰�
        for (int i = 0; i < turretSpawnerData.turretDatas.Count; i++)
        {
            eventSpawnLevels.Add(turretSpawnerData.turretDatas[i].spawnLevel);
            eventSpawnCooltimePercents.Add(turretSpawnerData.turretDatas[i].spawnCooldownPercent);
        }

        // �ͷ� ������ ������ ����
        // �ͷ� ������ ��ȯ ���� ����
        SettingTurretSpawnLevel(eventSpawnLevels.ToArray());
        // ����� �ͷ� ������ ��ȯ ������ �ͷ� ������ ��ȯ Ȯ�� ����
        SettingTurretSpawnChances(turretSpawnLevels.ToArray());
        // �ͷ� ������ ��ȯ ��Ÿ�� ����
        SettingTurretSpawnCooltimes(eventSpawnCooltimePercents.ToArray());
    }

    /// <summary> �ͷ� ������ ��ȯ ���� ���� </summary>
    /// <param name="levels"> ��ž ��ȯ ������ ������ �� �迭 </param>
    void SettingTurretSpawnLevel(params int[] levels)
    {
        // ��ž ��ȯ ������ ������ �� �迭�� ���̿� ��ž ��ȯ ���� �迭�� ���̰� ��ġ�ϴ��� Ȯ��
        if (levels.Length != turretSpawnLevels.Count)
        {
            Debug.LogError("��ž ��ȯ ������ ������ �� �迭�� ���̿� ��ž ��ȯ ���� �迭�� ���̰� �ٸ�" + levels.Length + " " + turretSpawnLevels.Count);
            return;
        }

        // �ͷ� ���� ����
        for (int i = 0; i < levels.Length; i++)
        {
            turretSpawnLevels[i] += levels[i];
        }
    }

    /// <summary> �ͷ� ������ ���� Ȯ�� ���� </summary>
    /// <param name="levels"> �� �ͷ��� ���� �迭 </param>
    void SettingTurretSpawnChances(params int[] levels)
    {
        // �� �ͷ��� ���� �迭�� ���̿� �ͷ� ��ȯ Ȯ�� �迭�� ���̰� ��ġ�ϴ��� Ȯ��
        if (levels.Length != turretSpawnChances.Count)
        {
            Debug.LogError("�� �ͷ��� ���� �迭�� ���̿� �ͷ� ��ȯ Ȯ�� �迭�� ���̰� �ٸ�");
            return;
        }

        // ���� �հ� ���
        int levelSum = 0;
        for (int i = 0; i < levels.Length; i++)
        {
            levelSum += levels[i];
        }

        // ���� �հ谡 0�� ���, ��� Ȯ���� �����ϰ� �����Ͽ� ���� ����
        if (levelSum == 0)
        {
            Debug.LogError("���� �հ谡 0. ��� Ȯ�� �����ϰ� ����");
            float equalChance = 100f / levels.Length;
            for (int i = 0; i < levels.Length; i++)
            {
                turretSpawnChances[i] = equalChance;
            }
            return;
        }

        // �� �ͷ��� ��ȯ Ȯ�� ���
        float chancePerLevel = 100f / levelSum;
        float totalPercentage = 0f; // ������ ���� �� Ȯ�� �հ� ���
        for (int i = 0; i < levels.Length; i++)
        {
            turretSpawnChances[i] = chancePerLevel * levels[i];
            totalPercentage += turretSpawnChances[i];
        }

        // Ȯ���� ������ 100%�� ������� Ȯ�� (�ε��Ҽ��� �������� ���� ���� ���� ���)
        if (Mathf.Abs(100f - totalPercentage) > 0.01f)
        {
            Debug.LogWarning($"Ȯ�� ������ 100%�� �ƴ�. ����: {totalPercentage}%");
        }
    }

    /// <summary> �ͷ� ������ ��ȯ ��Ÿ�� ���� </summary>
    /// <param name="cooltimePercents"> ��Ÿ���� ������ �ۼ�Ʈ �� �迭 </param>
    void SettingTurretSpawnCooltimes(params float[] cooltimePercents)
    {
        // ��Ÿ���� ������ �ۼ�Ʈ �� �迭�� ���̰� �ͷ� ��ȯ ��Ÿ�� �迭�� ���̿� ��ġ�ϴ��� Ȯ��
        if (cooltimePercents.Length != turretSpawnCooltimes.Count)
        {
            Debug.LogError("��Ÿ���� ������ �ۼ�Ʈ �� �迭�� ���̿� �ͷ� ��ȯ ��Ÿ�� �迭�� ���̰� �ٸ�");
            return;
        }

        // �ͷ� ��Ÿ�� ����
        for (int i = 0; i < cooltimePercents.Length; i++)
        {
            // �Էµ� ���� ���� �������� Ȯ��
            if (cooltimePercents[i] < 0)
            {
                Debug.LogWarning($"{i}��° �ͷ� ��Ÿ�� ������ �����̸� ��Ÿ�� ����");
            }
            // �ʱ� ��Ÿ���� ������ŭ ��Ÿ�� ����
            float value = turretInitialSpawnCooltimes[i] * cooltimePercents[i];

            // ��Ÿ�ӿ� ���� ����
            turretSpawnCooltimes[i] -= value;
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
            turret.gameObject.SetActive(true);
            turret.transform.position = spawnPosition.position;
            turret.transform.rotation = Quaternion.identity;

            // 1������ 4�� ���� ����Ʈ������ �ͷ� ��������Ʈ�� �ݴ�� ����
            if (spawnPositionIndex >= 0 && spawnPositionIndex <= 3)
            {
                Vector3 currentScale = turret.transform.localScale;
                currentScale.x *= -1;
                turret.transform.localScale = currentScale;
            }

            // spawnPoint ����
            turret.spawnPointIndex = spawnPositionIndex;
            turret.spawner = this;

            if (turret is BulletTurret bulletTurret)
            {
                if (_isBulletSplitActive)
                {
                    // �п� �Ѿ� �̺�Ʈ�� Ȱ��ȭ�� ���(�ӽ�)
                    Debug.Log("�п� �Ѿ� ����");
                    bulletTurret.ChangeProjectile(1); // �п� �Ѿ� ������ ����
                }
                else
                {
                    bulletTurret.ChangeProjectile(0); // �⺻ �Ѿ� ������ ����
                }
            }

            _nextSpawnTime = ChooseCooldown(turretToSpawn); // ���� ��ȯ������ �ð� ����
        }
        else
        {
            Debug.LogWarning($"Failed to get turret of type {turretToSpawn} from pool.");
        }
    }

    /// <summary> �ͷ� ���� ���� </summary>
    GameObject ChooseTurretType()
    {
        float randomChance = Random.value; // 0�� 1 ������ ������ ��
        float currentChance = 0f;

        for (int i = 0; i < turretPrefabs.Count; i++)
        {
            currentChance += turretSpawnChances[i]; // ���� Ȯ�� ������Ʈ
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
        return turretSpawnCooltimes[index];
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

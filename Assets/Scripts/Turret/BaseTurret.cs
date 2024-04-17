using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    /// <summary> ����ü �߻� ���� </summary>
    [SerializeField] protected int _projectileCount;
    /// <summary> ���� ���� ����ü �߻� ���� </summary>
    protected int currentProjectileCount; 
    /// <summary> ���� �ӵ� </summary>
    [SerializeField] protected float _attackSpeed;
    /// <summary> ������ �߻� ���� ��� �ð� </summary>
    [SerializeField] protected float _timeSinceLastShot = 0f;
    /// <summary> �ͷ� ���� �ð� </summary>
    [SerializeField] protected float _lifeTime;
    /// <summary> ���� �ͷ� ���� �ð� </summary>
    protected float currentLifeTime;
    /// <summary> ������ ����ü �߻� ���� �÷��� </summary>
    protected bool isLastProjectileShot = false;

    /// <summary> ����ü�� �߻�Ǵ� ��ġ(����ü�� �����Ǵ� ��ġ) </summary>
    [SerializeField] protected Transform firePoint;
    /// <summary> ��ǥ ��ġ </summary>
    [SerializeField] protected Transform targetPosition;

    /// <summary> �߻��� ����ü ������ ����Ʈ </summary>
    [SerializeField] protected GameObject[] projectilePrefabs;
    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    /// <summary> TurretSpawner ���� </summary>
    public TurretSpawner spawner;
    /// <summary> �ͷ� ���� �ε��� </summary>
    public int turretIndex;
    /// <summary> ��ȯ ��ġ �ε��� </summary>
    public int spawnPointIndex;
    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void OnEnable()
    {
        InitTurret(); // �ʱ�ȭ
    }

    void Update()
    {
        if (currentLifeTime <= 0 && isLastProjectileShot)
        {
            Invoke("DisableTurret", 1.5f);  // ������ ����ü �߻� 1.5�� �Ŀ� �ͷ� ��Ȱ��ȭ
            return;
        }

        _timeSinceLastShot += Time.deltaTime;
        currentLifeTime -= Time.deltaTime;

        if (ShouldShoot())
        {
            Shoot();
            _projectileCount--;  // �߻��� ����ü �� ����
            if (_projectileCount == 0)
            {
                isLastProjectileShot = true;
            }
        }
    }

    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    /// <summary> ����ü�� �߻� �������� Ȯ�� </summary>
    protected bool ShouldShoot()
    {
        return _timeSinceLastShot >= _attackSpeed;
    }

    /// <summary> �ͷ� �ʱ�ȭ </summary>
    protected virtual void InitTurret()
    {
        _lifeTime = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].turretLifeTime;
        currentLifeTime = _lifeTime;
        _projectileCount = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].projectileCount;
        currentProjectileCount = _projectileCount;
        _attackSpeed = _lifeTime / _projectileCount;
        _attackSpeed = Mathf.Max(_attackSpeed, 0.5f);  // ���� �ӵ� ����
        targetPosition = PlayerStat.Instance.transform;
    }

    /// <summary> ����ü �߻� </summary>
    protected abstract void Shoot();

    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    protected virtual void DisableTurret()
    {
        // �̸����� (Clone)�� ����
        string poolName = gameObject.name.Replace("(Clone)", "");

        // ������Ʈ Ǯ�� �ͷ� ��ȯ
        TurretPoolManager.Instance.Return(poolName, this);

        // ��ȯ ��ġ ��ȯ
        if (spawner != null)
        {
            spawner.SetPositionAvailable(spawnPointIndex);
        }
    }

    /// <summary> �ͷ��� ���� ȸ�� </summary>
    protected abstract void RotateTurret();

    /****************************************************************************
                                 public Methods
    ****************************************************************************/

}

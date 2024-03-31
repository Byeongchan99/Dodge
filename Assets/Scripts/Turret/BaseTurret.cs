using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    /// <summary> ���� �ӵ� = �ͷ� ���� �ð� / ����ü �߻� ���� </summary>
    [SerializeField] protected float _attackSpeed;
    /// <summary> ������ �߻� ���� ��� �ð� </summary>
    [SerializeField] protected float _timeSinceLastShot = 0f;
    /// <summary> �ͷ� ���� �ð� </summary>
    [SerializeField] protected float _lifeTime;

    /// <summary> ����ü�� �߻�Ǵ� ��ġ(����ü�� �����Ǵ� ��ġ) </summary>
    [SerializeField] protected Transform firePoint;
    /// <summary> ��ǥ ��ġ </summary>
    [SerializeField] protected Transform targetPosition;

    /// <summary> �߻��� ����ü ������ ����Ʈ </summary>
    [SerializeField] protected GameObject[] projectilePrefabs;
    /// <summary> ���� �߻��� ����ü ������ </summary>
    [SerializeField] protected GameObject currentProjectilePrefabs;

    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    /// <summary> TurretSpawner ���� </summary>
    public TurretSpawner spawner;
    /// <summary> ���� �ͷ� ���� �ð� </summary>
    public float currentLifeTime;
    /// <summary> ����ü �߻� ���� </summary>
    public int projectileCount;
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
        if (ShouldShoot())
        {
            Shoot();
            _timeSinceLastShot = 0f;
        }

        _timeSinceLastShot += Time.deltaTime;

        if (currentLifeTime <= 0)
        {
            DisableTurret();
        }

        currentLifeTime -= Time.deltaTime;
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
        currentLifeTime = _lifeTime;
        _attackSpeed = _lifeTime / projectileCount;
        currentProjectilePrefabs = projectilePrefabs[0];
        targetPosition = PlayerStat.Instance.transform;
    }

    /// <summary> ����ü �߻� </summary>
    protected abstract void Shoot();

    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    protected void DisableTurret()
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
    /// <summary> �߻��� ����ü ���� </summary>
    public virtual void ChangeProjectile(int projectileIndex)
    {
        Debug.Log("ChangeProjectile");
        currentProjectilePrefabs = projectilePrefabs[projectileIndex];
    }
}

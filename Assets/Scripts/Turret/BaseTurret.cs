using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    /// <summary> 공격 속도 = 터렛 유지 시간 / 투사체 발사 개수 </summary>
    [SerializeField] protected float _attackSpeed;
    /// <summary> 마지막 발사 이후 경과 시간 </summary>
    [SerializeField] protected float _timeSinceLastShot = 0f;
    /// <summary> 터렛 유지 시간 </summary>
    [SerializeField] protected float _lifeTime;

    /// <summary> 투사체가 발사되는 위치(투사체가 생성되는 위치) </summary>
    [SerializeField] protected Transform firePoint;
    /// <summary> 목표 위치 </summary>
    [SerializeField] protected Transform targetPosition;

    /// <summary> 발사할 투사체 프리팹 리스트 </summary>
    [SerializeField] protected GameObject[] projectilePrefabs;
    /// <summary> 현재 발사할 투사체 프리팹 </summary>
    [SerializeField] protected GameObject currentProjectilePrefabs;

    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    /// <summary> TurretSpawner 참조 </summary>
    public TurretSpawner spawner;
    /// <summary> 현재 터렛 유지 시간 </summary>
    public float currentLifeTime;
    /// <summary> 투사체 발사 개수 </summary>
    public int projectileCount;
    /// <summary> 소환 위치 인덱스 </summary>
    public int spawnPointIndex;

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void OnEnable()
    {
        InitTurret(); // 초기화
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
    /// <summary> 투사체를 발사 가능한지 확인 </summary>
    protected bool ShouldShoot()
    {
        return _timeSinceLastShot >= _attackSpeed;
    }

    /// <summary> 터렛 초기화 </summary>
    protected virtual void InitTurret()
    {
        currentLifeTime = _lifeTime;
        _attackSpeed = _lifeTime / projectileCount;
        currentProjectilePrefabs = projectilePrefabs[0];
        targetPosition = PlayerStat.Instance.transform;
    }

    /// <summary> 투사체 발사 </summary>
    protected abstract void Shoot();

    /// <summary> 터렛 비활성화 </summary>
    protected void DisableTurret()
    {
        // 이름에서 (Clone)을 제거
        string poolName = gameObject.name.Replace("(Clone)", "");

        // 오브젝트 풀에 터렛 반환
        TurretPoolManager.Instance.Return(poolName, this);

        // 소환 위치 반환
        if (spawner != null)
        {
            spawner.SetPositionAvailable(spawnPointIndex);
        }
    }

    /// <summary> 터렛의 포를 회전 </summary>
    protected abstract void RotateTurret();

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> 발사할 투사체 변경 </summary>
    public virtual void ChangeProjectile(int projectileIndex)
    {
        Debug.Log("ChangeProjectile");
        currentProjectilePrefabs = projectilePrefabs[projectileIndex];
    }
}

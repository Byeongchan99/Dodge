using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    /// <summary> 투사체 발사 개수 </summary>
    [SerializeField] protected int _projectileCount;
    /// <summary> 현재 남은 투사체 발사 개수 </summary>
    [SerializeField] protected int currentProjectileCount; 
    /// <summary> 공격 속도 </summary>
    [SerializeField] protected float _attackSpeed;
    /// <summary> 마지막 발사 이후 경과 시간 </summary>
    [SerializeField] protected float _timeSinceLastShot = 0f;
    /// <summary> 터렛 유지 시간 </summary>
    [SerializeField] protected float _lifeTime;
    /// <summary> 현재 터렛 유지 시간 </summary>
    [SerializeField] protected float currentLifeTime;
    /// <summary> 마지막 투사체 발사 여부 플래그 </summary>
    protected bool isLastProjectileShot = false;

    /// <summary> 투사체가 발사되는 위치(투사체가 생성되는 위치) </summary>
    [SerializeField] protected Transform firePoint;
    /// <summary> 목표 위치 </summary>
    [SerializeField] protected Transform targetPosition;

    /// <summary> 발사할 투사체 프리팹 리스트 </summary>
    [SerializeField] protected GameObject[] projectilePrefabs;

    /// <summary> 플래그 변수 </summary>
    private bool isDisabling = false; // 코루틴 실행 여부를 체크하기 위한 플래그
    private bool isInitialized = false; // 초기화 플래그

    /// <summary> Fade Effect 참조 </summary>
    private FadeEffect fadeEffect;

    /****************************************************************************
                                   public Fields
    ****************************************************************************/
    /// <summary> TurretSpawner 참조 </summary>
    public TurretSpawner spawner;
    /// <summary> 터렛 종류 인덱스 </summary>
    public int turretIndex;
    /// <summary> 소환 위치 인덱스 </summary>
    public int spawnPointIndex;

    /****************************************************************************
                                    Unity Callbacks
    ****************************************************************************/
    void Awake()
    {
        fadeEffect = GetComponent<FadeEffect>();
    }

    protected virtual void OnEnable()
    {
        if (!isInitialized)
        {
            // 처음 실행될 때는 초기화만 수행
            isInitialized = true;
            return;
        }

        fadeEffect.StartFadeIn(1f, 0.2f);
        InitTurret(); // 초기화
    }

    void Update()
    {
        if (currentLifeTime <= 0 && isLastProjectileShot)
        {
            // 이미 코루틴이 실행 중인지 확인
            if (!isDisabling)
            {
                StartCoroutine(StartDisableTurret());
            }
            return;
        }

        _timeSinceLastShot += Time.deltaTime;
        currentLifeTime -= Time.deltaTime;

        if (ShouldShoot())
        {
            Shoot();
            currentProjectileCount--;  // 발사할 투사체 수 감소
            if (currentProjectileCount == 0)
            {
                isLastProjectileShot = true;
            }
        }
    }

    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    /// <summary> 터렛 비활성화 실행 코루틴 </summary>
    /// 마지막 발사 후 애니메이션 적용하기 위해 사용
    IEnumerator StartDisableTurret()
    {
        // 코루틴 실행 중 플래그 설정
        isDisabling = true;
        fadeEffect.StartFadeOut(1.5f, 0.2f);
        yield return new WaitForSeconds(1.5f);
        DisableTurret();
    }

    /****************************************************************************
                            abstract and virtual Methods
    ****************************************************************************/
    /// <summary> 터렛 초기화 </summary>
    protected virtual void InitTurret()
    {
        // 터렛 설정
        // 터렛 유지 시간
        _lifeTime = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].turretLifeTime;
        currentLifeTime = _lifeTime;
        // 투사체 발사 개수
        _projectileCount = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].projectileCount;
        currentProjectileCount = _projectileCount;
        // 공격 속도
        _attackSpeed = _lifeTime / (_projectileCount + 1);
        //Debug.Log("Attack Speed: " + _attackSpeed);
        //_attackSpeed = Mathf.Max(_attackSpeed, 0.5f);  // 공격 속도 보장

        // 변수 초기화
        isDisabling = false;
        _timeSinceLastShot = 0f;
        isLastProjectileShot = false;
        targetPosition = PlayerStat.Instance.transform;
    }

    /// <summary> 투사체를 발사 가능한지 확인 </summary>
    protected virtual bool ShouldShoot()
    {
        return _timeSinceLastShot >= _attackSpeed && currentProjectileCount > 0;
    }

    /// <summary> 터렛의 포를 회전 </summary>
    protected abstract void RotateTurret();

    /// <summary> 투사체 발사 </summary>
    protected abstract void Shoot();

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> 터렛 비활성화 </summary>
    public virtual void DisableTurret()
    {
        Debug.Log("DisableTurret");

        // 이름에서 (Clone)을 제거
        string poolName = gameObject.name.Replace("(Clone)", "");

        // 오브젝트 풀에 터렛 반환
        TurretPoolManager.Instance.Return(poolName, this);

        // 소환 위치 반환
        if (spawner != null)
        {
            spawner.SetPositionAvailable(spawnPointIndex);
        }

        StopAllCoroutines();
    }
}

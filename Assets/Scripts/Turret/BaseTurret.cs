using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening; // DOTween 네임스페이스 추가
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

    /// <summary> 이펙트를 적용하기 위한 캔버스 그룹 </summary>
    [SerializeField] private CanvasGroup canvasGroup;

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
    protected virtual void OnEnable()
    {
        StartCoroutine(FadeIn(1f));
        // 등장 시 흔들리는 효과
        transform.DOShakePosition(enterShakeDuration, new Vector3(0, enterShakeStrength, 0), enterShakeVibrato);
        InitTurret(); // 초기화
    }

    void Update()
    {
        if (currentLifeTime <= 0 && isLastProjectileShot)
        {
            StartCoroutine(StartDisableTurret());
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
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(FadeOut(1f));
        DisableTurret();
    }

    /****************************************************************************
                                Effect Methods
    ****************************************************************************/
    IEnumerator FadeIn(float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator FadeOut(float duration)
    {
        float currentTime = 0;
        while (currentTime < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // 사라질 때 위로 부드럽게 움직이는 효과, 애니메이션 완료 후 비활성화
        transform.DOMoveY(transform.position.y + exitMoveY, exitMoveDuration).OnComplete(() => {
            // 비활성화 로직을 이 위치로 이동
            DisableTurret();
        });
    }

    [SerializeField] private float enterShakeDuration = 0.3f;
    [SerializeField] private float enterShakeStrength = 0.3f;
    [SerializeField] private int enterShakeVibrato = 10;
    [SerializeField] private float exitMoveDuration = 0.3f;
    [SerializeField] private float exitMoveY = 1f;

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

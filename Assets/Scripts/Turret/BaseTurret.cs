using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening; // DOTween ���ӽ����̽� �߰�
using UnityEngine;

public abstract class BaseTurret : MonoBehaviour
{
    /****************************************************************************
                                 protected Fields
    ****************************************************************************/
    /// <summary> ����ü �߻� ���� </summary>
    [SerializeField] protected int _projectileCount;
    /// <summary> ���� ���� ����ü �߻� ���� </summary>
    [SerializeField] protected int currentProjectileCount; 
    /// <summary> ���� �ӵ� </summary>
    [SerializeField] protected float _attackSpeed;
    /// <summary> ������ �߻� ���� ��� �ð� </summary>
    [SerializeField] protected float _timeSinceLastShot = 0f;
    /// <summary> �ͷ� ���� �ð� </summary>
    [SerializeField] protected float _lifeTime;
    /// <summary> ���� �ͷ� ���� �ð� </summary>
    [SerializeField] protected float currentLifeTime;
    /// <summary> ������ ����ü �߻� ���� �÷��� </summary>
    protected bool isLastProjectileShot = false;

    /// <summary> ����ü�� �߻�Ǵ� ��ġ(����ü�� �����Ǵ� ��ġ) </summary>
    [SerializeField] protected Transform firePoint;
    /// <summary> ��ǥ ��ġ </summary>
    [SerializeField] protected Transform targetPosition;

    /// <summary> �߻��� ����ü ������ ����Ʈ </summary>
    [SerializeField] protected GameObject[] projectilePrefabs;

    // �ڷ�ƾ ���� ���θ� üũ�ϱ� ���� �÷���
    private bool isDisabling = false;

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
    protected virtual void OnEnable()
    {
        StartCoroutine(FadeIn(1f));
        InitTurret(); // �ʱ�ȭ
    }

    void Update()
    {
        if (currentLifeTime <= 0 && isLastProjectileShot)
        {
            // �̹� �ڷ�ƾ�� ���� ������ Ȯ��
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
            currentProjectileCount--;  // �߻��� ����ü �� ����
            if (currentProjectileCount == 0)
            {
                isLastProjectileShot = true;
            }
        }
    }

    /****************************************************************************
                                 private Methods
    ****************************************************************************/
    /// <summary> �ͷ� ��Ȱ��ȭ ���� �ڷ�ƾ </summary>
    /// ������ �߻� �� �ִϸ��̼� �����ϱ� ���� ���
    IEnumerator StartDisableTurret()
    {
        Debug.Log("StartDisableTurret");
        // �ڷ�ƾ ���� �� �÷��� ����
        isDisabling = true;
        StartCoroutine(FadeOut(1.5f));
        yield return new WaitForSeconds(1.5f);
        DisableTurret();
    }

    /****************************************************************************
                                 Effect Methods
    ****************************************************************************/
    IEnumerator FadeIn(float duration)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (var spriteRenderer in spriteRenderers)
        {
            Color color = spriteRenderer.color;
            color.a = 0f; // �ʱ� alpha�� 0���� ����
            spriteRenderer.color = color;
            spriteRenderer.DOFade(1f, duration).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(duration);
    }

    IEnumerator FadeOut(float duration)
    {
        Debug.Log("FadeOut");
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.DOFade(0f, duration).SetEase(Ease.InOutQuad);
        }

        yield return new WaitForSeconds(duration);
    }

    /****************************************************************************
                            abstract and virtual Methods
    ****************************************************************************/
    /// <summary> �ͷ� �ʱ�ȭ </summary>
    protected virtual void InitTurret()
    {
        // �ͷ� ����
        // �ͷ� ���� �ð�
        _lifeTime = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].turretLifeTime;
        currentLifeTime = _lifeTime;
        // ����ü �߻� ����
        _projectileCount = StatDataManager.Instance.currentStatData.turretDatas[turretIndex].projectileCount;
        currentProjectileCount = _projectileCount;
        // ���� �ӵ�
        _attackSpeed = _lifeTime / (_projectileCount + 1);
        //Debug.Log("Attack Speed: " + _attackSpeed);
        //_attackSpeed = Mathf.Max(_attackSpeed, 0.5f);  // ���� �ӵ� ����

        // ���� �ʱ�ȭ
        _timeSinceLastShot = 0f;
        isLastProjectileShot = false;
        targetPosition = PlayerStat.Instance.transform;
    }

    /// <summary> ����ü�� �߻� �������� Ȯ�� </summary>
    protected virtual bool ShouldShoot()
    {
        return _timeSinceLastShot >= _attackSpeed && currentProjectileCount > 0;
    }

    /// <summary> �ͷ��� ���� ȸ�� </summary>
    protected abstract void RotateTurret();

    /// <summary> ����ü �߻� </summary>
    protected abstract void Shoot();

    /****************************************************************************
                                 public Methods
    ****************************************************************************/
    /// <summary> �ͷ� ��Ȱ��ȭ </summary>
    public virtual void DisableTurret()
    {
        Debug.Log("DisableTurret");

        // �̸����� (Clone)�� ����
        string poolName = gameObject.name.Replace("(Clone)", "");

        // ������Ʈ Ǯ�� �ͷ� ��ȯ
        TurretPoolManager.Instance.Return(poolName, this);

        // ��ȯ ��ġ ��ȯ
        if (spawner != null)
        {
            spawner.SetPositionAvailable(spawnPointIndex);
        }

        StopAllCoroutines();
    }
}

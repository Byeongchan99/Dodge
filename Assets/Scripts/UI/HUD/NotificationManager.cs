using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationObject; // 공지 오브젝트
    public Transform notificationParent;  // 공지 내용을 배치할 부모 객체
    private CanvasGroup canvasGroup; // 공지 오브젝트의 캔버스 그룹
    private Text notificationText; // 공지 메시지
    private Queue<string> notificationQueue = new Queue<string>();
    private bool _isDisplayingNotification = false;

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        notificationText = GetComponentInChildren<Text>();
    }

    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    /// <summary> 공지창에 들어갈 내용 관리 </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        string turretName = string.Empty;
        string enhancementTypeName = string.Empty;
        bool OnNotification = true;

        switch (enhancement.turretType)
        {
            // 총알 터렛
            case TurretUpgradeInfo.TurretType.Bullet:
                turretName = "Bullet Turret:";
                // Bullet Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // 분열 총알 업그레이드
                        if (enhancement.value == 1)
                        {
                            // 이미 분열 총알이 활성화되어 있을 때
                            if (StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex == 1)
                            {
                                Debug.Log("분열 총알 활성화 되어있음");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "Split bullet activation";
                            }
                        }
                        else
                        {
                            // 이미 분열 총알이 비활성화되어 있을 때
                            if (StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex == 0)
                            {
                                Debug.Log("분열 총알 비활성화 되어있음");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "Split bullet deactivation";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 총알 개수 변경
                        enhancementTypeName = "Increase bullet count";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Bullet speed is maximum";
                            Debug.Log("총알 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase bullet speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Bullet size is maximum";
                            Debug.Log("총알 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase bullet size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        break;
                }
                break;

            // 레이저 터렛
            case TurretUpgradeInfo.TurretType.Laser:
                turretName = "Laser Turret:";
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // 지속 시간 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "Laser duration is maximum";
                            Debug.Log("레이저 지속 시간 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser duration";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        if (StatDataManager.Instance.currentStatData.turretDatas[1].isMaxProjectileCount)
                        {
                            enhancementTypeName = "Laser count is maximum";
                            Debug.Log("레이저 개수 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser count";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Laser fire speed is maximum";
                            Debug.Log("레이저 발사 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser fire speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Laser size is maximum";
                            Debug.Log("레이저 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        break;
                }
                break;

            // 로켓 터렛
            case TurretUpgradeInfo.TurretType.Rocket:
                turretName = "Rocket Turret:";
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "Rocket lifetime is maximum";
                            Debug.Log("로켓 지속 시간 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase rocket lifetime";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        enhancementTypeName = "Increase rocket count";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // Change speed
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Rocket speed is maximum";
                            Debug.Log("로켓 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase rocket speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // Change size
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Rocket size is maximum";
                            Debug.Log("로켓 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase rocket size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        break;
                }
                break;

            // 박격포 터렛
            case TurretUpgradeInfo.TurretType.Mortar:
                turretName = "Mortar Turret:";
                // Mortar Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // 분열 박격포탄 업그레이드
                        if (enhancement.value == 1)
                        {
                            // 이미 분열 박격포탄이 활성화되어 있을 때
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 1)
                            {
                                Debug.Log("분열 박격포탄 활성화 되어있음");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "Split mortar bomb activation";
                            }
                        }
                        else
                        {
                            // 이미 분열 박격포탄이 비활성화되어 있을 때
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 0)
                            {
                                Debug.Log("분열 박격포탄 비활성화 되어있음");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "Split mortar bomb deactivation";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // Change count
                        enhancementTypeName = "Increase mortar bomb count";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // Change speed
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Mortar bomb speed is maximum";
                            Debug.Log("박격포탄 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase mortar bomb speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // Change size
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Explosion range is maximum";
                            Debug.Log("폭발 범위 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase explosion range";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        break;
                }
                break;
        }

        AddNotification(turretName + " " + enhancementTypeName, OnNotification);
    }

    /// <summary> 공지창 큐에 추가 </summary>
    public void AddNotification(string message, bool OnNotification)
    {
        Debug.Log(message);
        // 만약 공지을 안해도 되는 이벤트일 경우는 무시
        if (OnNotification == true)
        {
            notificationQueue.Enqueue(message);
            if (!_isDisplayingNotification)
            {
                StartCoroutine(DisplayNotification());
            }
        }
    }

    /// <summary> 공지창 디스플레이 </summary>
    private IEnumerator DisplayNotification()
    {
        while (notificationQueue.Count > 0)
        {
            _isDisplayingNotification = true;
            string message = notificationQueue.Dequeue();
            notificationObject.SetActive(true);
            notificationText.text = message;
            
            // 페이드 인
            yield return StartCoroutine(FadeIn());

            // 일정 시간 대기
            yield return new WaitForSeconds(3f);

            // 페이드 아웃
            yield return StartCoroutine(FadeOut());

            notificationObject.SetActive(false);
        }
        _isDisplayingNotification = false;
    }

    /// <summary> 공지창 페이드 인 효과 </summary>
    private IEnumerator FadeIn()
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / duration;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    /// <summary> 공지창 페이드 아웃 효과 </summary>
    private IEnumerator FadeOut()
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / duration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }

    /// <summary> 공지창 초기화 </summary>
    public void ClearNotifications()
    {
        notificationQueue.Clear();
        canvasGroup.alpha = 0;
        _isDisplayingNotification = false;
        notificationText.text = string.Empty;
        notificationObject.SetActive(false);
    }
}
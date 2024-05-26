using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    // Text noticeText;

    public GameObject notificationPrefab; // 알림 프리팹
    public Transform notificationParent;  // 알림을 배치할 부모 객체
    private Queue<string> notificationQueue = new Queue<string>();
    private bool isDisplayingNotification = false;

    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    /// <summary> 알림창에 들어갈 내용 관리 </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        string turretName = string.Empty;
        string enhancementTypeName = string.Empty;
        bool OnNotification = true;

        switch (enhancement.turretType)
        {
            // 총알 터렛
            case TurretUpgradeInfo.TurretType.Bullet:
                turretName = "총알 터렛";
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
                                enhancementTypeName = "분열 총알 활성화";
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
                                enhancementTypeName = "분열 총알 비활성화";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        enhancementTypeName = "총알 개수 변경";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "총알 속도 최대치";
                            Debug.Log("총알 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "총알 속도 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSize)
                        {
                            enhancementTypeName = "총알 크기 최대치";
                            Debug.Log("총알 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "총알 크기 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화
                        break;
                }
                break;

            // 레이저 터렛
            case TurretUpgradeInfo.TurretType.Laser:
                turretName = "레이저 터렛";
                // Laser Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // 지속 시간
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "레이저 지속 시간 최대치";
                            Debug.Log("레이저 지속 시간 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "레이저 지속 시간 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 변경
                        if (StatDataManager.Instance.currentStatData.turretDatas[1].isMaxProjectileCount)
                        {
                            enhancementTypeName = "레이저 개수 최대치";
                            Debug.Log("레이저 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "레이저 개수 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSize)
                        {
                            enhancementTypeName = "레이저 크기 최대치";
                            Debug.Log("레이저 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "레이저 크기 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화


                        break;
                        // 기타 필요한 경우 추가
                }
                break;

            // 로켓 터렛
            case TurretUpgradeInfo.TurretType.Rocket:
                turretName = "로켓 터렛";
                // Rocket Turret의 업그레이드 처리
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "로켓 지속 시간 최대치";
                            Debug.Log("로켓 지속 시간 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "로켓 지속 시간 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가
                        enhancementTypeName = "로켓 개수 변경";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "로켓 속도 최대치";
                            Debug.Log("로켓 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "로켓 속도 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSize)
                        {
                            enhancementTypeName = "로켓 크기 최대치";
                            Debug.Log("로켓 크기 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "로켓 크기 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화


                        break;
                        // 기타 필요한 경우 추가
                }
                break;

            // 박격포 터렛
            case TurretUpgradeInfo.TurretType.Mortar:
                turretName = "박격포 터렛";
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
                                enhancementTypeName = "분열 박격포탄 활성화";
                            }
                        }
                        else
                        {
                            // 이미 분열 총알이 비활성화되어 있을 때
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 0)
                            {
                                Debug.Log("분열 박격포탄 비활성화 되어있음");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "분열 박격포탄 비활성화";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // 개수 증가
                        enhancementTypeName = "박격포탄 개수 변경";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // 속도 증가
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "박격포탄 속도 최대치";
                            Debug.Log("박격포탄 속도 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "박격포탄 속도 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // 크기 변경
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSize)
                        {
                            enhancementTypeName = "폭발 범위 최대치";
                            Debug.Log("폭발 범위 최대치");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "폭발 범위 변경";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // 초기화


                        break;
                        // 기타 필요한 경우 추가
                }
                break;
                // 기타 터렛 유형에 대한 처리
        }

        AddNotification(turretName + " " + enhancementTypeName, OnNotification);
    }

    /// <summary> 알림창 큐에 추가 </summary>
    public void AddNotification(string message, bool OnNotification)
    {
        Debug.Log(message);
        // 만약 알림을 안해도 되는 이벤트일 경우는 무시
        if (OnNotification == true)
        {
            notificationQueue.Enqueue(message);
            if (!isDisplayingNotification)
            {
                StartCoroutine(DisplayNotification());
            }
        }
    }

    /// <summary> 알림창 디스플레이 </summary>
    private IEnumerator DisplayNotification()
    {
        while (notificationQueue.Count > 0)
        {
            isDisplayingNotification = true;
            string message = notificationQueue.Dequeue();
            GameObject notification = Instantiate(notificationPrefab, notificationParent);
            notification.GetComponentInChildren<Text>().text = message;
            CanvasGroup canvasGroup = notification.GetComponent<CanvasGroup>();

            // 페이드 인
            yield return StartCoroutine(FadeIn(canvasGroup));

            // 일정 시간 대기
            yield return new WaitForSeconds(3f);

            // 페이드 아웃
            yield return StartCoroutine(FadeOut(canvasGroup));

            Destroy(notification);
        }
        isDisplayingNotification = false;
    }

    /// <summary> 알림창 페이드 인 효과 </summary>
    private IEnumerator FadeIn(CanvasGroup canvasGroup)
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / duration;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    /// <summary> 알림창 페이드 아웃 효과 </summary>
    private IEnumerator FadeOut(CanvasGroup canvasGroup)
    {
        float duration = 0.5f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / duration);
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
}

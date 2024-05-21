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

    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        //noticeText.text = $"{enhancement.turretType}" + $"{enhancement.enhancementType}" + "업그레이드 적용";
        AddNotification($"{enhancement.turretType}" + " " + $"{enhancement.enhancementType}" + "업그레이드 적용");
    }

    public void AddNotification(string message)
    {
        Debug.Log(message);
        notificationQueue.Enqueue(message);
        if (!isDisplayingNotification)
        {
            StartCoroutine(DisplayNotification());
        }
    }

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

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    // Text noticeText;

    public GameObject notificationPrefab; // �˸� ������
    public Transform notificationParent;  // �˸��� ��ġ�� �θ� ��ü
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
        //noticeText.text = $"{enhancement.turretType}" + $"{enhancement.enhancementType}" + "���׷��̵� ����";
        AddNotification($"{enhancement.turretType}" + " " + $"{enhancement.enhancementType}" + "���׷��̵� ����");
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

            // ���̵� ��
            yield return StartCoroutine(FadeIn(canvasGroup));

            // ���� �ð� ���
            yield return new WaitForSeconds(3f);

            // ���̵� �ƿ�
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

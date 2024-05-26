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

    /// <summary> �˸�â�� �� ���� ���� </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        string turretName = string.Empty;
        string enhancementTypeName = string.Empty;
        bool OnNotification = true;

        switch (enhancement.turretType)
        {
            // �Ѿ� �ͷ�
            case TurretUpgradeInfo.TurretType.Bullet:
                turretName = "�Ѿ� �ͷ�";
                // Bullet Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �Ѿ� ���׷��̵�
                        if (enhancement.value == 1)
                        {
                            // �̹� �п� �Ѿ��� Ȱ��ȭ�Ǿ� ���� ��
                            if (StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex == 1)
                            {
                                Debug.Log("�п� �Ѿ� Ȱ��ȭ �Ǿ�����");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "�п� �Ѿ� Ȱ��ȭ";
                            }
                        }
                        else
                        {
                            // �̹� �п� �Ѿ��� ��Ȱ��ȭ�Ǿ� ���� ��
                            if (StatDataManager.Instance.currentStatData.turretDatas[0].projectileIndex == 0)
                            {
                                Debug.Log("�п� �Ѿ� ��Ȱ��ȭ �Ǿ�����");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "�п� �Ѿ� ��Ȱ��ȭ";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        enhancementTypeName = "�Ѿ� ���� ����";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "�Ѿ� �ӵ� �ִ�ġ";
                            Debug.Log("�Ѿ� �ӵ� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "�Ѿ� �ӵ� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSize)
                        {
                            enhancementTypeName = "�Ѿ� ũ�� �ִ�ġ";
                            Debug.Log("�Ѿ� ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "�Ѿ� ũ�� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        break;
                }
                break;

            // ������ �ͷ�
            case TurretUpgradeInfo.TurretType.Laser:
                turretName = "������ �ͷ�";
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // ���� �ð�
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "������ ���� �ð� �ִ�ġ";
                            Debug.Log("������ ���� �ð� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "������ ���� �ð� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        if (StatDataManager.Instance.currentStatData.turretDatas[1].isMaxProjectileCount)
                        {
                            enhancementTypeName = "������ ���� �ִ�ġ";
                            Debug.Log("������ �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "������ ���� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSize)
                        {
                            enhancementTypeName = "������ ũ�� �ִ�ġ";
                            Debug.Log("������ ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "������ ũ�� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ


                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;

            // ���� �ͷ�
            case TurretUpgradeInfo.TurretType.Rocket:
                turretName = "���� �ͷ�";
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "���� ���� �ð� �ִ�ġ";
                            Debug.Log("���� ���� �ð� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "���� ���� �ð� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        enhancementTypeName = "���� ���� ����";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "���� �ӵ� �ִ�ġ";
                            Debug.Log("���� �ӵ� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "���� �ӵ� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSize)
                        {
                            enhancementTypeName = "���� ũ�� �ִ�ġ";
                            Debug.Log("���� ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "���� ũ�� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ


                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;

            // �ڰ��� �ͷ�
            case TurretUpgradeInfo.TurretType.Mortar:
                turretName = "�ڰ��� �ͷ�";
                // Mortar Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.IsProjectileSplit:
                        // �п� �ڰ���ź ���׷��̵�
                        if (enhancement.value == 1)
                        {
                            // �̹� �п� �ڰ���ź�� Ȱ��ȭ�Ǿ� ���� ��
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 1)
                            {
                                Debug.Log("�п� �ڰ���ź Ȱ��ȭ �Ǿ�����");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "�п� �ڰ���ź Ȱ��ȭ";
                            }
                        }
                        else
                        {
                            // �̹� �п� �Ѿ��� ��Ȱ��ȭ�Ǿ� ���� ��
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 0)
                            {
                                Debug.Log("�п� �ڰ���ź ��Ȱ��ȭ �Ǿ�����");
                                OnNotification = false;
                            }
                            else
                            {
                                enhancementTypeName = "�п� �ڰ���ź ��Ȱ��ȭ";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        enhancementTypeName = "�ڰ���ź ���� ����";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "�ڰ���ź �ӵ� �ִ�ġ";
                            Debug.Log("�ڰ���ź �ӵ� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "�ڰ���ź �ӵ� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[3].isMaxProjectileSize)
                        {
                            enhancementTypeName = "���� ���� �ִ�ġ";
                            Debug.Log("���� ���� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "���� ���� ����";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ


                        break;
                        // ��Ÿ �ʿ��� ��� �߰�
                }
                break;
                // ��Ÿ �ͷ� ������ ���� ó��
        }

        AddNotification(turretName + " " + enhancementTypeName, OnNotification);
    }

    /// <summary> �˸�â ť�� �߰� </summary>
    public void AddNotification(string message, bool OnNotification)
    {
        Debug.Log(message);
        // ���� �˸��� ���ص� �Ǵ� �̺�Ʈ�� ���� ����
        if (OnNotification == true)
        {
            notificationQueue.Enqueue(message);
            if (!isDisplayingNotification)
            {
                StartCoroutine(DisplayNotification());
            }
        }
    }

    /// <summary> �˸�â ���÷��� </summary>
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

    /// <summary> �˸�â ���̵� �� ȿ�� </summary>
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

    /// <summary> �˸�â ���̵� �ƿ� ȿ�� </summary>
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

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public GameObject notificationObject; // ���� ������Ʈ
    public Transform notificationParent;  // ���� ������ ��ġ�� �θ� ��ü
    private CanvasGroup canvasGroup; // ���� ������Ʈ�� ĵ���� �׷�
    private Text notificationText; // ���� �޽���
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

    /// <summary> ����â�� �� ���� ���� </summary>
    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        string turretName = string.Empty;
        string enhancementTypeName = string.Empty;
        bool OnNotification = true;

        switch (enhancement.turretType)
        {
            // �Ѿ� �ͷ�
            case TurretUpgradeInfo.TurretType.Bullet:
                turretName = "Bullet Turret:";
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
                                enhancementTypeName = "Split bullet activation";
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
                                enhancementTypeName = "Split bullet deactivation";
                            }
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // �Ѿ� ���� ����
                        enhancementTypeName = "Increase bullet count";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Bullet speed is maximum";
                            Debug.Log("�Ѿ� �ӵ� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase bullet speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[0].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Bullet size is maximum";
                            Debug.Log("�Ѿ� ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase bullet size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        break;
                }
                break;

            // ������ �ͷ�
            case TurretUpgradeInfo.TurretType.Laser:
                turretName = "Laser Turret:";
                // Laser Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        // ���� �ð� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "Laser duration is maximum";
                            Debug.Log("������ ���� �ð� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser duration";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        if (StatDataManager.Instance.currentStatData.turretDatas[1].isMaxProjectileCount)
                        {
                            enhancementTypeName = "Laser count is maximum";
                            Debug.Log("������ ���� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser count";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // �ӵ� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Laser fire speed is maximum";
                            Debug.Log("������ �߻� �ӵ� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser fire speed";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.SizeChange:
                        // ũ�� ����
                        if (StatDataManager.Instance.currentStatData.projectileDatas[1].isMaxProjectileSize)
                        {
                            enhancementTypeName = "Laser size is maximum";
                            Debug.Log("������ ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase laser size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        break;
                }
                break;

            // ���� �ͷ�
            case TurretUpgradeInfo.TurretType.Rocket:
                turretName = "Rocket Turret:";
                // Rocket Turret�� ���׷��̵� ó��
                switch (enhancement.enhancementType)
                {
                    case TurretUpgradeInfo.EnhancementType.LifeTimeChange:
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileLifeTime)
                        {
                            enhancementTypeName = "Rocket lifetime is maximum";
                            Debug.Log("���� ���� �ð� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase rocket lifetime";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.CountChange:
                        // ���� ����
                        enhancementTypeName = "Increase rocket count";
                        break;
                    case TurretUpgradeInfo.EnhancementType.SpeedChange:
                        // Change speed
                        if (StatDataManager.Instance.currentStatData.projectileDatas[2].isMaxProjectileSpeed)
                        {
                            enhancementTypeName = "Rocket speed is maximum";
                            Debug.Log("���� �ӵ� �ִ�ġ");
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
                            Debug.Log("���� ũ�� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase rocket size";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        break;
                }
                break;

            // �ڰ��� �ͷ�
            case TurretUpgradeInfo.TurretType.Mortar:
                turretName = "Mortar Turret:";
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
                                enhancementTypeName = "Split mortar bomb activation";
                            }
                        }
                        else
                        {
                            // �̹� �п� �ڰ���ź�� ��Ȱ��ȭ�Ǿ� ���� ��
                            if (StatDataManager.Instance.currentStatData.turretDatas[3].projectileIndex == 0)
                            {
                                Debug.Log("�п� �ڰ���ź ��Ȱ��ȭ �Ǿ�����");
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
                            Debug.Log("�ڰ���ź �ӵ� �ִ�ġ");
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
                            Debug.Log("���� ���� �ִ�ġ");
                            OnNotification = false;
                        }
                        else
                        {
                            enhancementTypeName = "Increase explosion range";
                        }
                        break;
                    case TurretUpgradeInfo.EnhancementType.Init:
                        // �ʱ�ȭ
                        break;
                }
                break;
        }

        AddNotification(turretName + " " + enhancementTypeName, OnNotification);
    }

    /// <summary> ����â ť�� �߰� </summary>
    public void AddNotification(string message, bool OnNotification)
    {
        Debug.Log(message);
        // ���� ������ ���ص� �Ǵ� �̺�Ʈ�� ���� ����
        if (OnNotification == true)
        {
            notificationQueue.Enqueue(message);
            if (!_isDisplayingNotification)
            {
                StartCoroutine(DisplayNotification());
            }
        }
    }

    /// <summary> ����â ���÷��� </summary>
    private IEnumerator DisplayNotification()
    {
        while (notificationQueue.Count > 0)
        {
            _isDisplayingNotification = true;
            string message = notificationQueue.Dequeue();
            notificationObject.SetActive(true);
            notificationText.text = message;
            
            // ���̵� ��
            yield return StartCoroutine(FadeIn());

            // ���� �ð� ���
            yield return new WaitForSeconds(3f);

            // ���̵� �ƿ�
            yield return StartCoroutine(FadeOut());

            notificationObject.SetActive(false);
        }
        _isDisplayingNotification = false;
    }

    /// <summary> ����â ���̵� �� ȿ�� </summary>
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

    /// <summary> ����â ���̵� �ƿ� ȿ�� </summary>
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

    /// <summary> ����â �ʱ�ȭ </summary>
    public void ClearNotifications()
    {
        notificationQueue.Clear();
        canvasGroup.alpha = 0;
        _isDisplayingNotification = false;
        notificationText.text = string.Empty;
        notificationObject.SetActive(false);
    }
}
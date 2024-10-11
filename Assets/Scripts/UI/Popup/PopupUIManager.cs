using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PopupUIManager : MonoBehaviour
{
    /****************************************************************************
                                     private Fields
    ****************************************************************************/
    /// <summary> �ǽð� �˾� ���� ��ũ�� ����Ʈ </summary>
    private LinkedList<PopupUI> popupLinkedList;

    /// <summary> UI ����Ʈ </summary>
    [SerializeField] private List<PopupUI> popupList = new List<PopupUI>();

    /// <summary> Popup UI �ν��Ͻ����� �̸����� �����ϱ� ���� ��ųʸ� </summary>
    private Dictionary<string, PopupUI> popupDictionary = new Dictionary<string, PopupUI>();

    [SerializeField] PauseUI pauseUI; // �Ͻ� ����â

    /****************************************************************************
                                     Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        // ��ũ�� ����Ʈ ����
        popupLinkedList = new LinkedList<PopupUI>();
        // �ʱ�ȭ
        Init();
    }

    private void Update()
    {
        // esc Ű�� ������ �˾� �ݱ�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // � �˾�â�� �������� ��
            if (popupLinkedList.Count > 0)
            {
                // �Ͻ� ���� â�� �������� ��
                if (popupLinkedList.First.Value.gameObject.name == "Pause" && GameManager.Instance.isPaused)
                {
                    ClosePausePopup();
                }
                else
                {
                    ClosePopup(popupLinkedList.First.Value);
                }
            }
            // ���� �������� �÷��� ���� ��
            else if (GameManager.Instance.isPlayingStage)
            {
                PopupUI pausePopup = GetPopup("Pause");
                if (pausePopup != null)
                {                 
                    pauseUI.OpenPauseUI();
                }
                // �Ͻ� ����â ����
                OpenPopup("Pause");
            }
            /*
            // ����â ����
            else
            {
                OpenPopup("Option");
            }
            */
        }
    }

    /****************************************************************************
                                     private Methods
    ****************************************************************************/
    /// <summary> �ʱ�ȭ </summary>
    private void Init()
    {
        // ����Ʈ�� PopupUI �ν��Ͻ����� ��ųʸ��� ��� �� ��Ȱ��ȭ
        foreach (var popup in popupList)
        {
            RegisterUI(popup.gameObject.name, popup);
            popup.gameObject.SetActive(false);

            // ��� Focus �̺�Ʈ ���
            popup.OnFocus += () =>
            {
                // ��ũ�� ����Ʈ���� �����ϰ� ���Ӱ� �߰�
                popupLinkedList.Remove(popup);
                popupLinkedList.AddFirst(popup);
                UpdatePopupUIOrder();
            };

            // �˾� �ݱ� ��ư ���
            popup.closeButton.onClick.AddListener(() => ClosePopup(popup));
        }
    }

    /// <summary> PopupUI �ν��Ͻ����� ��ųʸ��� ����ϴ� �޼��� </summary>
    private void RegisterUI(string name, PopupUI popup)
    {
        if (!popupDictionary.ContainsKey(name))
        {
            popupDictionary.Add(name, popup);
        }
    }

    /// <summary> �̸��� ����Ͽ� �˾��� �������� �޼��� </summary>
    private PopupUI GetPopup(string UIName)
    {
        if (popupDictionary.TryGetValue(UIName, out PopupUI popup))
        {
            return popup;
        }
        else { return null; }
    }

    /// <summary> �˾� ���� </summary>
    private void OpenPopup(PopupUI popup)
    {
        // ��ũ�� ����Ʈ�� �߰��ϰ�
        popupLinkedList.AddFirst(popup);
        // Ȱ��ȭ
        popup.Show();
        //popup.isOpen = true;
        //popup.gameObject.SetActive(true);
        // ���� ������Ʈ
        UpdatePopupUIOrder();
    }

    /// <summary> �̸��� ����Ͽ� �˾� ���� </summary>
    public void OpenPopup(string UIName)
    {
        if (popupDictionary.TryGetValue(UIName, out PopupUI popup))
        {
            OpenPopup(popup);
        }
        else
        {
            Debug.LogWarning($"Popup with name {UIName} not found.");
        }
    }

    /// <summary> �˾� �ݱ� </summary>
    private void ClosePopup(PopupUI popup)
    {
        // ��ũ�� ����Ʈ���� �����ϰ�
        popupLinkedList.Remove(popup);
        // ��Ȱ��ȭ
        popup.Hide();
        //popup.isOpen = false;
        //popup.gameObject.SetActive(false);
        // ���� ������Ʈ
        UpdatePopupUIOrder();
    }

    /// <summary> �̸��� ����Ͽ� �˾� �ݱ� </summary>
    public void ClosePopup(string UIName)
    {
        if (popupDictionary.TryGetValue(UIName, out PopupUI popup))
        {
            ClosePopup(popup);
        }
        else
        {
            Debug.LogWarning($"Popup with name {UIName} not found.");
        }
    }

    /// <summary> ����Ű�� ���� �˾� ��� </summary>
    private void togglePopup(PopupUI popup)
    {
        if (popup.isOpen)
        {
            ClosePopup(popup);
        }
        else
        {
            OpenPopup(popup);
        }
    }

    /// <summary> �˾� UI���� ���� ������Ʈ </summary>
    private void UpdatePopupUIOrder()
    {
        foreach (var popup in popupLinkedList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }

    /****************************************************************************
                             public Methods
    ****************************************************************************/
    /// <summary> �������� ���� UI ���� </summary>
    public void OpenStageInformationPopup()
    {
        OpenPopup("Stage Information");
    }

    /// <summary> �������� ���� UI �ݱ� </summary>
    public void CloseStageInformationPopup()
    {
        ClosePopup("Stage Information");
    }

    public void ClosePausePopup()
    {
        PopupUI pausePopup = GetPopup("Pause");
        if (pausePopup != null)
        {
            pauseUI.ClosePauseUI();
        }
        // �Ͻ� ����â �ݱ�
        Debug.Log("�Ͻ� ����â �ݱ�");
        ClosePopup(popupLinkedList.First.Value);
    }
}

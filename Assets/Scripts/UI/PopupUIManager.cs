using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace UIManage
{
    public class PopupUIManager : MonoBehaviour
    {
        /****************************************************************************
                                         private Fields
        ****************************************************************************/
        /// <summary> �ǽð� �˾� ���� ��ũ�� ����Ʈ </summary>
        private LinkedList<PopupUI> _popupLinkedList;

        /// <summary> UI ����Ʈ </summary>
        [SerializeField] private List<PopupUI> _popupList = new List<PopupUI>();

        /// <summary> Popup UI �ν��Ͻ����� �̸����� �����ϱ� ���� ��ųʸ� </summary>
        private Dictionary<string, PopupUI> _popupDictionary = new Dictionary<string, PopupUI>();

        /****************************************************************************
                                         Unity Callbacks
        ****************************************************************************/
        private void Awake()
        {
            // ��ũ�� ����Ʈ ����
            _popupLinkedList = new LinkedList<PopupUI>();
            // �ʱ�ȭ
            Init();
        }

        private void Update()
        {
            // esc Ű�� ������ �˾� �ݱ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_popupLinkedList.Count > 0)
                {                  
                    // �Ͻ� ���� â�� �������� ��
                    if (_popupLinkedList.First.Value.gameObject.name == "Pause" && GameManager.Instance.isPlayingStage) 
                    {
                        // �Ͻ� ����â �ݱ�
                        ClosePopup(_popupLinkedList.First.Value);
                        // Ÿ�̸� �簳
                        ScoreManager.Instance.ResumeTimer();
                    }
                    else
                    {
                        ClosePopup(_popupLinkedList.First.Value);
                    }
                }
                // ���� �������� �÷��� ���� ��
                else if (GameManager.Instance.isPlayingStage)
                {
                    // �Ͻ� ����â ����
                    OpenPopup("Pause");
                    // Ÿ�̸� �Ͻ� ����
                    ScoreManager.Instance.PauseTimer();
                }
            }
        }

        /****************************************************************************
                                         private Methods
        ****************************************************************************/
        /// <summary> �ʱ�ȭ </summary>
        private void Init()
        {
            // ����Ʈ�� PopupUI �ν��Ͻ����� ��ųʸ��� ��� �� ��Ȱ��ȭ
            foreach (var popup in _popupList)
            {
                RegisterUI(popup.gameObject.name, popup);
                popup.gameObject.SetActive(false);

                // ��� Focus �̺�Ʈ ���
                popup.OnFocus += () =>
                {
                    // ��ũ�� ����Ʈ���� �����ϰ� ���Ӱ� �߰�
                    _popupLinkedList.Remove(popup);
                    _popupLinkedList.AddFirst(popup);
                    UpdatePopupUIOrder();
                };

                // �˾� �ݱ� ��ư ���
                popup.closeButton.onClick.AddListener(() => ClosePopup(popup));
            }
        }

        /// <summary> PopupUI �ν��Ͻ����� ��ųʸ��� ����ϴ� �޼��� </summary>
        private void RegisterUI(string name, PopupUI popup)
        {
            if (!_popupDictionary.ContainsKey(name))
            {
                _popupDictionary.Add(name, popup);
            }
        }

        private PopupUI GetPopup(string UIName)
        {
            if (_popupDictionary.TryGetValue(UIName, out PopupUI popup))
            {
                return popup;
            }
            else { return null; }
        }

        /// <summary> �˾� ���� </summary>
        private void OpenPopup(PopupUI popup)
        {
            // ��ũ�� ����Ʈ�� �߰��ϰ�
            _popupLinkedList.AddFirst(popup);
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
            if (_popupDictionary.TryGetValue(UIName, out PopupUI popup))
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
            _popupLinkedList.Remove(popup);
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
            if (_popupDictionary.TryGetValue(UIName, out PopupUI popup))
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
            foreach (var popup in _popupLinkedList)
            {
                popup.transform.SetAsFirstSibling();
            }
        }

        /****************************************************************************
                                 public Methods
        ****************************************************************************/
        public void OpenStageInformationPopup()
        {
            OpenPopup("Stage Information");
        }

        public void CloseStageInformationPopup()
        {
            ClosePopup("Stage Information");
        }
    }
}

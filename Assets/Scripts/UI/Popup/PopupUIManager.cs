using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PopupUIManager : MonoBehaviour
{
    /****************************************************************************
                                     private Fields
    ****************************************************************************/
    /// <summary> 실시간 팝업 관리 링크드 리스트 </summary>
    private LinkedList<PopupUI> popupLinkedList;

    /// <summary> UI 리스트 </summary>
    [SerializeField] private List<PopupUI> popupList = new List<PopupUI>();

    /// <summary> Popup UI 인스턴스들을 이름으로 관리하기 위한 딕셔너리 </summary>
    private Dictionary<string, PopupUI> popupDictionary = new Dictionary<string, PopupUI>();

    [SerializeField] PauseUI pauseUI; // 일시 정지창

    /****************************************************************************
                                     Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        // 링크드 리스트 생성
        popupLinkedList = new LinkedList<PopupUI>();
        // 초기화
        Init();
    }

    private void Update()
    {
        // esc 키를 누르면 팝업 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 어떤 팝업창이 켜져있을 때
            if (popupLinkedList.Count > 0)
            {
                // 일시 정지 창이 켜져있을 때
                if (popupLinkedList.First.Value.gameObject.name == "Pause" && GameManager.Instance.isPaused)
                {
                    ClosePausePopup();
                }
                else
                {
                    ClosePopup(popupLinkedList.First.Value);
                }
            }
            // 현재 스테이지 플레이 중일 때
            else if (GameManager.Instance.isPlayingStage)
            {
                PopupUI pausePopup = GetPopup("Pause");
                if (pausePopup != null)
                {                 
                    pauseUI.OpenPauseUI();
                }
                // 일시 정지창 열기
                OpenPopup("Pause");
            }
            /*
            // 설정창 열기
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
    /// <summary> 초기화 </summary>
    private void Init()
    {
        // 리스트의 PopupUI 인스턴스들을 딕셔너리에 등록 및 비활성화
        foreach (var popup in popupList)
        {
            RegisterUI(popup.gameObject.name, popup);
            popup.gameObject.SetActive(false);

            // 헤더 Focus 이벤트 등록
            popup.OnFocus += () =>
            {
                // 링크드 리스트에서 제거하고 새롭게 추가
                popupLinkedList.Remove(popup);
                popupLinkedList.AddFirst(popup);
                UpdatePopupUIOrder();
            };

            // 팝업 닫기 버튼 등록
            popup.closeButton.onClick.AddListener(() => ClosePopup(popup));
        }
    }

    /// <summary> PopupUI 인스턴스들을 딕셔너리에 등록하는 메서드 </summary>
    private void RegisterUI(string name, PopupUI popup)
    {
        if (!popupDictionary.ContainsKey(name))
        {
            popupDictionary.Add(name, popup);
        }
    }

    /// <summary> 이름을 사용하여 팝업을 가져오는 메서드 </summary>
    private PopupUI GetPopup(string UIName)
    {
        if (popupDictionary.TryGetValue(UIName, out PopupUI popup))
        {
            return popup;
        }
        else { return null; }
    }

    /// <summary> 팝업 열기 </summary>
    private void OpenPopup(PopupUI popup)
    {
        // 링크드 리스트에 추가하고
        popupLinkedList.AddFirst(popup);
        // 활성화
        popup.Show();
        //popup.isOpen = true;
        //popup.gameObject.SetActive(true);
        // 순서 업데이트
        UpdatePopupUIOrder();
    }

    /// <summary> 이름을 사용하여 팝업 열기 </summary>
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

    /// <summary> 팝업 닫기 </summary>
    private void ClosePopup(PopupUI popup)
    {
        // 링크드 리스트에서 제거하고
        popupLinkedList.Remove(popup);
        // 비활성화
        popup.Hide();
        //popup.isOpen = false;
        //popup.gameObject.SetActive(false);
        // 순서 업데이트
        UpdatePopupUIOrder();
    }

    /// <summary> 이름을 사용하여 팝업 닫기 </summary>
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

    /// <summary> 단축키에 따라 팝업 토글 </summary>
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

    /// <summary> 팝업 UI들의 순서 업데이트 </summary>
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
    /// <summary> 스테이지 정보 UI 열기 </summary>
    public void OpenStageInformationPopup()
    {
        OpenPopup("Stage Information");
    }

    /// <summary> 스테이지 정보 UI 닫기 </summary>
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
        // 일시 정지창 닫기
        Debug.Log("일시 정지창 닫기");
        ClosePopup(popupLinkedList.First.Value);
    }
}

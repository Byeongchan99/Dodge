using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenUIManager : MonoBehaviour
{
    /****************************************************************************
                                     private Fields
    ****************************************************************************/
    /// <summary> UI 관리 스택 </summary>
    private Stack<FullscreenUI> fullscreenStack = new Stack<FullscreenUI>();

    /// <summary> UI 리스트 </summary>
    [SerializeField] private List<FullscreenUI> fullscreenList = new List<FullscreenUI>();

    /// <summary> Fullscreen UI 인스턴스들을 이름으로 관리하기 위한 딕셔너리 </summary>
    private Dictionary<string, FullscreenUI> fullscreenDictionary = new Dictionary<string, FullscreenUI>();

    /// <summary> 현재 FullscreenUI를 반환하는 프로퍼티 </summary>
    private FullscreenUI _current => fullscreenStack.Count > 0 ? fullscreenStack.Peek() : null;

    /****************************************************************************
                                     Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        // 초기화
        Init();
    }

    private void Update()
    {
        // Esc 키를 누르면 현재 FullscreenUI를 숨기고 이전 FullscreenUI를 반환 - 뒤로가기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 메인 화면이 아니라면 뒤로가기
            if (fullscreenStack.Count > 1)
                Pop();
        }
    }

    /****************************************************************************
                                     private Methods
    ****************************************************************************/
    /// <summary> 초기화 </summary>
    private void Init()
    {
        // 리스트의 FullscreenUI 인스턴스들을 딕셔너리에 등록 및 비활성화
        foreach (var fullscreen in fullscreenList)
        {
            RegisterUI(fullscreen.gameObject.name, fullscreen);
            fullscreen.gameObject.SetActive(false);
        }

        // 메인 화면
        Push("Main");
    }

    /// <summary> FullscreenUI 인스턴스들을 딕셔너리에 등록하는 메서드 </summary>
    private void RegisterUI(string name, FullscreenUI fullscreen)
    {
        if (!fullscreenDictionary.ContainsKey(name))
        {
            fullscreenDictionary.Add(name, fullscreen);
        }
    }

    /// <summary> UIName을 가진 FullscreenUI를 스택에 추가하고 반환 </summary>
    private FullscreenUI Push(string UIName)
    {
        if (fullscreenDictionary.TryGetValue(UIName, out FullscreenUI fullscreen))
        {
            if (_current != null)
            {
                _current.Hide();
            }
            fullscreenStack.Push(fullscreen);
            fullscreen.Show();
            return fullscreen;
        }
        else
        {
            Debug.LogError($"Fullscreen with name {UIName} not found.");
            return null;
        }
    }

    /// <summary> 현재 FullscreenUI를 숨기고 이전 FullscreenUI를 반환 </summary>
    private void Pop()
    {
        if (fullscreenStack.Count > 0)
        {
            fullscreenStack.Pop().Hide();
        }

        if (_current != null)
        {
            _current.Show();
        }
    }

    /// <summary> 특정 이름의 FullscreenUI가 나올 때까지 Pop </summary>
    private void PopTo(string UIName)
    {
        while (fullscreenStack.Count > 0 && _current.gameObject.name != UIName)
        {
            Pop();
        }
    }

    /// <summary> 첫 번째 FullscreenUI가 나올 때까지 모두 Pop </summary>
    private void PopToRoot()
    {
        while (fullscreenStack.Count > 1) // Root만 남겨두기
        {
            Pop();
        }
    }

    /****************************************************************************
                             public Methods
    ****************************************************************************/
    /// <summary> 뒤로가기 버튼 클릭 메서드 </summary>
    public void OnBackButtonClicked()
    {
        Pop();
    }

    /// <summary> UI 버튼에 할당하기 위한 메서드 </summary>
    public void OnPushFullscreenUI(string UIName)
    {
        Push(UIName);
    }

    /*
    /// <summary> 캐릭터 선택창 버튼 클릭 메서드 </summary>
    public void OnCharacterSelectButtonClicked()
    {
        Push("Character Select");
    }

    /// <summary> 홈 버튼 클릭 메서드 </summary>
    public void OnHomeButtonClicked()
    {
        PopToRoot();
    }

    /// <summary> 스테이지 선택창 버튼 클릭 메서드 </summary>
    public void OnStageSelectButtonClicked()
    {
        Push("Stage Select");
    }
    */
}

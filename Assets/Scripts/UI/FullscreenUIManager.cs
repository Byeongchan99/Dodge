using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenUIManager : MonoBehaviour
{
    /****************************************************************************
                                     private Fields
    ****************************************************************************/
    /// <summary> UI ���� ���� </summary>
    private Stack<FullscreenUI> _fullscreenStack = new Stack<FullscreenUI>();

    /// <summary> UI ����Ʈ </summary>
    [SerializeField] private List<FullscreenUI> _fullscreenList = new List<FullscreenUI>();

    /// <summary> Fullscreen UI �ν��Ͻ����� �̸����� �����ϱ� ���� ��ųʸ� </summary>
    private Dictionary<string, FullscreenUI> _fullscreenDictionary = new Dictionary<string, FullscreenUI>();

    /// <summary> ���� FullscreenUI�� ��ȯ�ϴ� ������Ƽ </summary>
    private FullscreenUI _current => _fullscreenStack.Count > 0 ? _fullscreenStack.Peek() : null;

    /****************************************************************************
                                     Unity Callbacks
    ****************************************************************************/
    private void Awake()
    {
        // �ʱ�ȭ
        Init();
    }

    private void Update()
    {
        // Esc Ű�� ������ ���� FullscreenUI�� ����� ���� FullscreenUI�� ��ȯ - �ڷΰ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pop();
        }
    }

    /****************************************************************************
                                     private Methods
    ****************************************************************************/
    /// <summary> �ʱ�ȭ </summary>
    private void Init()
    {
        // ����Ʈ�� FullscreenUI �ν��Ͻ����� ��ųʸ��� ��� �� ��Ȱ��ȭ
        foreach (var fullscreen in _fullscreenList)
        {
            RegisterUI(fullscreen.gameObject.name, fullscreen);
            fullscreen.gameObject.SetActive(false);
        }

        // ���� ȭ��
        Push("Main");
    }

    /// <summary> FullscreenUI �ν��Ͻ����� ��ųʸ��� ����ϴ� �޼��� </summary>
    private void RegisterUI(string name, FullscreenUI fullscreen)
    {
        if (!_fullscreenDictionary.ContainsKey(name))
        {
            _fullscreenDictionary.Add(name, fullscreen);
        }
    }

    /// <summary> UIName�� ���� FullscreenUI�� ���ÿ� �߰��ϰ� ��ȯ </summary>
    private FullscreenUI Push(string UIName)
    {
        if (_fullscreenDictionary.TryGetValue(UIName, out FullscreenUI fullscreen))
        {
            if (_current != null)
            {
                _current.Hide();
            }
            _fullscreenStack.Push(fullscreen);
            fullscreen.Show();
            return fullscreen;
        }
        else
        {
            Debug.LogError($"Fullscreen with name {UIName} not found.");
            return null;
        }
    }

    /// <summary> ���� FullscreenUI�� ����� ���� FullscreenUI�� ��ȯ </summary>
    private void Pop()
    {
        if (_fullscreenStack.Count > 0)
        {
            _fullscreenStack.Pop().Hide();
        }

        if (_current != null)
        {
            _current.Show();
        }
    }

    /// <summary> Ư�� �̸��� FullscreenUI�� ���� ������ Pop </summary>
    private void PopTo(string UIName)
    {
        while (_fullscreenStack.Count > 0 && _current.gameObject.name != UIName)
        {
            Pop();
        }
    }

    /// <summary> ù ��° FullscreenUI�� ���� ������ ��� Pop </summary>
    private void PopToRoot()
    {
        while (_fullscreenStack.Count > 1) // Root�� ���ܵα�
        {
            Pop();
        }
    }

    /****************************************************************************
                             public Methods
    ****************************************************************************/
    /// <summary> �ڷΰ��� ��ư Ŭ�� �޼��� </summary>
    public void OnBackButtonClicked()
    {
        Pop();
    }

    /// <summary> ĳ���� ����â ��ư Ŭ�� �޼��� </summary>
    public void OnCharacterSelectButtonClicked()
    {
        Push("Character Select");
    }

    /*
    /// <summary> Ȩ ��ư Ŭ�� �޼��� </summary>
    public void OnHomeButtonClicked()
    {
        PopToRoot();
    }
    */

    /// <summary> �������� ����â ��ư Ŭ�� �޼��� </summary>
    public void OnStageSelectButtonClicked()
    {
        Push("Stage Select");
    }
}

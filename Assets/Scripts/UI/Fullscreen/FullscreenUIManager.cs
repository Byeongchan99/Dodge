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
    private Stack<FullscreenUI> fullscreenStack = new Stack<FullscreenUI>();

    /// <summary> UI ����Ʈ </summary>
    [SerializeField] private List<FullscreenUI> fullscreenList = new List<FullscreenUI>();

    /// <summary> Fullscreen UI �ν��Ͻ����� �̸����� �����ϱ� ���� ��ųʸ� </summary>
    private Dictionary<string, FullscreenUI> fullscreenDictionary = new Dictionary<string, FullscreenUI>();

    /// <summary> ���� FullscreenUI�� ��ȯ�ϴ� ������Ƽ </summary>
    private FullscreenUI _current => fullscreenStack.Count > 0 ? fullscreenStack.Peek() : null;

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
            // ���� ȭ���� �ƴ϶�� �ڷΰ���
            if (fullscreenStack.Count > 1)
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
        foreach (var fullscreen in fullscreenList)
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
        if (!fullscreenDictionary.ContainsKey(name))
        {
            fullscreenDictionary.Add(name, fullscreen);
        }
    }

    /// <summary> UIName�� ���� FullscreenUI�� ���ÿ� �߰��ϰ� ��ȯ </summary>
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

    /// <summary> ���� FullscreenUI�� ����� ���� FullscreenUI�� ��ȯ </summary>
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

    /// <summary> Ư�� �̸��� FullscreenUI�� ���� ������ Pop </summary>
    private void PopTo(string UIName)
    {
        while (fullscreenStack.Count > 0 && _current.gameObject.name != UIName)
        {
            Pop();
        }
    }

    /// <summary> ù ��° FullscreenUI�� ���� ������ ��� Pop </summary>
    private void PopToRoot()
    {
        while (fullscreenStack.Count > 1) // Root�� ���ܵα�
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

    /// <summary> UI ��ư�� �Ҵ��ϱ� ���� �޼��� </summary>
    public void OnPushFullscreenUI(string UIName)
    {
        Push(UIName);
    }

    /*
    /// <summary> ĳ���� ����â ��ư Ŭ�� �޼��� </summary>
    public void OnCharacterSelectButtonClicked()
    {
        Push("Character Select");
    }

    /// <summary> Ȩ ��ư Ŭ�� �޼��� </summary>
    public void OnHomeButtonClicked()
    {
        PopToRoot();
    }

    /// <summary> �������� ����â ��ư Ŭ�� �޼��� </summary>
    public void OnStageSelectButtonClicked()
    {
        Push("Stage Select");
    }
    */
}

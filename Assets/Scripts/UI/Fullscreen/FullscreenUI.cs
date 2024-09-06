using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FullscreenUI : MonoBehaviour
{
    private enum VisibleState
    {
        Appearing,
        Appeared,
        Disappearing,
        Disappeared
    }

    // UIView�� ���� ��ġ�� ������ �ʵ�
    private Vector2 _originalPosition;
    // �ʱ� ���� ����
    private VisibleState _state = VisibleState.Disappeared;
    // RectTransform ������Ʈ�� ���� ����
    private RectTransform rectTransform;

    /// <summary> ���� �� UIView�� ���� ��ġ ���� </summary>
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _originalPosition = rectTransform.anchoredPosition;
    }

    /// <summary> UI ��Ҹ� �����ִ� �޼��� </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _state = VisibleState.Appearing;
        // ȭ�� �߾����� �̵�
        //_rectTransform.DOAnchorPos(Vector2.zero, 0.5f).OnComplete(() => _state = VisibleState.Appeared);

        rectTransform.anchoredPosition = Vector2.zero;
        _state = VisibleState.Appeared;
    }

    /// <summary> UI ��Ҹ� ����� �޼��� </summary>
    public void Hide()
    {
        _state = VisibleState.Disappearing;
        // ���� ��ġ�� �̵�
        /*
        _rectTransform.DOAnchorPos(_originalPosition, 0.5f).OnComplete(() =>
        {
            _state = VisibleState.Disappeared;
        });
        */
        rectTransform.anchoredPosition = _originalPosition;
        _state = VisibleState.Disappeared;
        gameObject.SetActive(false);
    }
}

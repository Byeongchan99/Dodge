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

    // UIView의 원래 위치를 저장할 필드
    private Vector2 _originalPosition;
    // 초기 상태 설정
    private VisibleState _state = VisibleState.Disappeared;
    // RectTransform 컴포넌트에 대한 참조
    private RectTransform _rectTransform;

    /// <summary> 시작 시 UIView의 원래 위치 저장 </summary>
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalPosition = _rectTransform.anchoredPosition;
    }

    /// <summary> UI 요소를 보여주는 메서드 </summary>
    public void Show()
    {
        gameObject.SetActive(true);
        _state = VisibleState.Appearing;
        // 화면 중앙으로 이동
        //_rectTransform.DOAnchorPos(Vector2.zero, 0.5f).OnComplete(() => _state = VisibleState.Appeared);

        _rectTransform.anchoredPosition = Vector2.zero;
        _state = VisibleState.Appeared;
    }

    /// <summary> UI 요소를 숨기는 메서드 </summary>
    public void Hide()
    {
        _state = VisibleState.Disappearing;
        // 원래 위치로 이동
        /*
        _rectTransform.DOAnchorPos(_originalPosition, 0.5f).OnComplete(() =>
        {
            _state = VisibleState.Disappeared;
        });
        */
        _rectTransform.anchoredPosition = _originalPosition;
        _state = VisibleState.Disappeared;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class PopupUI : MonoBehaviour, IPointerDownHandler
{
    public Button closeButton;
    // 클릭 시 팝업을 가장 위로 올리는 액션
    public event Action OnFocus;

    // 팝업이 열려있는지 여부를 나타내는 프로퍼티
    public bool isOpen { get; set; } = false;

    // Focus를 사용할지 여부
    public bool useFocus;

    // UIView의 원래 위치를 저장할 필드
    private Vector2 _originalPosition;
    // RectTransform 컴포넌트에 대한 참조
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    /// <summary> 시작 시 UIView의 원래 위치 저장 </summary>
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _originalPosition = rectTransform.anchoredPosition;
        canvasGroup = gameObject.AddComponent<CanvasGroup>();

        rectTransform.localScale = Vector3.zero; // 초기 상태는 스케일 0
        canvasGroup.alpha = 0f; // 초기 상태는 투명
    }

    public void Show()
    {
        rectTransform.anchoredPosition = Vector2.zero;

        gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        sequence.Append(rectTransform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBack).SetUpdate(true))
                .Join(canvasGroup.DOFade(1f, 0.1f).SetEase(Ease.OutQuad).SetUpdate(true))
                .OnComplete(() =>
                {
                    isOpen = true;
                });
    }

    public void Hide()
    {
        /*
        _rectTransform.anchoredPosition = _originalPosition;
        isOpen = false;
        gameObject.SetActive(false);
        */

        Sequence sequence = DOTween.Sequence().SetUpdate(true);
        sequence.Append(rectTransform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).SetUpdate(true))
                .Join(canvasGroup.DOFade(0f, 0.1f).SetEase(Ease.InQuad).SetUpdate(true))
                .OnComplete(() =>
                {
                    rectTransform.anchoredPosition = _originalPosition;
                    isOpen = false;
                    gameObject.SetActive(false);
                });
    }

    /// <summary> OnFocus 이벤트를 외부에서 트리거할 수 있는 메서드 </summary>
    public void TriggerOnFocus()
    {
        if (useFocus)
        {
            OnFocus?.Invoke();
        }
    }

    /// <summary> 팝업 UI를 마우스로 클릭할 때 </summary>
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        TriggerOnFocus();
    }
}

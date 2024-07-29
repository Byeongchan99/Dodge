using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

namespace UIManage
{
    public class PopupUI : MonoBehaviour, IPointerDownHandler
    {
        public Button closeButton;
        // Ŭ�� �� �˾��� ���� ���� �ø��� �׼�
        public event Action OnFocus;

        // �˾��� �����ִ��� ���θ� ��Ÿ���� ������Ƽ
        public bool isOpen { get; set; } = false;

        // Focus�� ������� ����
        public bool useFocus;

        // UIView�� ���� ��ġ�� ������ �ʵ�
        private Vector2 _originalPosition;
        // RectTransform ������Ʈ�� ���� ����
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;

        /// <summary> ���� �� UIView�� ���� ��ġ ���� </summary>
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            _rectTransform.localScale = Vector3.zero; // �ʱ� ���´� ������ 0
            _canvasGroup.alpha = 0f; // �ʱ� ���´� ����
        }

        public void Show()
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            isOpen = true;
            gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectTransform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack))
                    .Join(_canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutQuad))
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

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_rectTransform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InBack))
                    .Join(_canvasGroup.DOFade(0f, 0.5f).SetEase(Ease.InQuad))
                    .OnComplete(() =>
                    {
                        _rectTransform.anchoredPosition = _originalPosition;
                        isOpen = false;
                        gameObject.SetActive(false);
                    });
        }

        /// <summary> OnFocus �̺�Ʈ�� �ܺο��� Ʈ������ �� �ִ� �޼��� </summary>
        public void TriggerOnFocus()
        {
            if (useFocus)
            {
                OnFocus?.Invoke();
            }
        }

        /// <summary> �˾� UI�� ���콺�� Ŭ���� �� </summary>
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            TriggerOnFocus();
        }
    }
}

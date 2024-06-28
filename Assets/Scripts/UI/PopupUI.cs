using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        /// <summary> ���� �� UIView�� ���� ��ġ ���� </summary>
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
        }

        public void Show()
        {
            _rectTransform.anchoredPosition = Vector2.zero;
            isOpen = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _rectTransform.anchoredPosition = _originalPosition;
            isOpen = false;
            gameObject.SetActive(false);
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

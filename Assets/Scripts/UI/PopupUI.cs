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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    public Image abilityIcon; // ��ų ������ �̹���
    public Image cooldownOverlay; // ��Ÿ�� �������� �̹���
    public Text cooldownText; // ��Ÿ�� �ؽ�Ʈ

    public float cooldownDuration; // ��Ÿ�� ���� �ð�
    private float _cooldownTimer; // ���� ��Ÿ�� Ÿ�̸�
    private bool _isCooldown; // ��Ÿ�� ���� ����

    // �ʱ�ȭ
    public void Init(float cooldownTime)
    {
        ResetCooldown(cooldownTime);
    }

    void Update()
    {
        if (_isCooldown)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _isCooldown = false;
                _cooldownTimer = 0;
                cooldownOverlay.fillAmount = 0;
                cooldownText.text = "";
            }
            else
            {
                // ��Ÿ�� �������� ������Ʈ
                cooldownOverlay.fillAmount = _cooldownTimer / cooldownDuration;
                // ���� �ð� �ؽ�Ʈ ������Ʈ
                cooldownText.text = Mathf.Ceil(_cooldownTimer).ToString();
            }
        }
    }

    // ��ٿ� ����
    public void StartCooldown()
    {
        if (!_isCooldown)
        {
            _isCooldown = true;
            _cooldownTimer = cooldownDuration;
            cooldownOverlay.fillAmount = 1;
            cooldownText.text = cooldownDuration.ToString();
        }
    }

    // ��ٿ� �ʱ�ȭ
    public void ResetCooldown(float cooldownTime)
    {
        _isCooldown = false;
        cooldownDuration = cooldownTime;
        _cooldownTimer = 0;
        cooldownOverlay.fillAmount = 0;
        cooldownText.text = "";
    }
}

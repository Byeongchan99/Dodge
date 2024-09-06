using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    public Image abilityIcon; // 스킬 아이콘 이미지
    public Image cooldownOverlay; // 쿨타임 오버레이 이미지
    public Text cooldownText; // 쿨타임 텍스트

    public float cooldownDuration; // 쿨타임 지속 시간
    private float _cooldownTimer; // 현재 쿨타임 타이머
    private bool _isCooldown; // 쿨타임 진행 여부

    // 초기화
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
                // 쿨타임 오버레이 업데이트
                cooldownOverlay.fillAmount = _cooldownTimer / cooldownDuration;
                // 남은 시간 텍스트 업데이트
                cooldownText.text = Mathf.Ceil(_cooldownTimer).ToString();
            }
        }
    }

    // 쿨다운 시작
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

    // 쿨다운 초기화
    public void ResetCooldown(float cooldownTime)
    {
        _isCooldown = false;
        cooldownDuration = cooldownTime;
        _cooldownTimer = 0;
        cooldownOverlay.fillAmount = 0;
        cooldownText.text = "";
    }
}

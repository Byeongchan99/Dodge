using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : MonoBehaviour
{
    public Text noticeText;

    void OnEnable()
    {
        EventManager.StartListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    void OnDisable()
    {
        EventManager.StopListening("TurretUpgrade", HandleTurretUpgradeEvent);
    }

    private void HandleTurretUpgradeEvent(TurretUpgradeInfo enhancement)
    {
        noticeText.text = $"{enhancement.turretType}" +  $"{enhancement.enhancementType}" + "업그레이드 적용";
    }
}

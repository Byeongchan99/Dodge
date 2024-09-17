using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityItem : BaseItem
{
    protected override void InitItem()
    {
        base.InitItem();
        itemEffect = new InvincibilityEffect(5f, PlayerStat.Instance.gameObject);
    }
}

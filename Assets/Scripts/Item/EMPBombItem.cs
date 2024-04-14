using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPBombItem : BaseItem
{
    protected override void InitItem()
    {
        itemEffect = new EMPBombEffect(0.5f, PlayerStat.Instance.gameObject);
    }
}

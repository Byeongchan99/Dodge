using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUpItem : BaseItem
{
    protected override void InitItem()
    {
        itemEffect = new MoveSpeedUpEffect(5f, PlayerStat.Instance.gameObject, 150f);
    }
}

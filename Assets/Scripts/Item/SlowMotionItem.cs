using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionItem : BaseItem
{
    protected override void InitItem()
    {
        itemEffect = new SlowMotionEffect(3f, PlayerStat.Instance.gameObject);
    }
}

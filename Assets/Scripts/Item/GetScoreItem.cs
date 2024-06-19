using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScoreItem : BaseItem
{
    protected override void InitItem()
    {
        itemEffect = new GetScoreEffect(0f, PlayerStat.Instance.gameObject);
    }
}

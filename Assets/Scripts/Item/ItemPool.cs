using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public GameObject moveSpeedUpItemPrefab;
    public GameObject invincibilityItemPrefab;
    public GameObject slowMotionItemPrefab;
    public GameObject empBombItemPrefab;
    public GameObject getScoreItemPrefab;
    // 다른 아이템 프리팹도 추가

    public Transform itemContainer;

    private void Start()
    {
        ItemPoolManager.Instance.CreatePool(moveSpeedUpItemPrefab.GetComponent<MoveSpeedUpItem>(), 10, itemContainer);
        ItemPoolManager.Instance.CreatePool(invincibilityItemPrefab.GetComponent<InvincibilityItem>(), 10, itemContainer);
        ItemPoolManager.Instance.CreatePool(slowMotionItemPrefab.GetComponent<SlowMotionItem>(), 10, itemContainer);
        ItemPoolManager.Instance.CreatePool(empBombItemPrefab.GetComponent<EMPBombItem>(), 10, itemContainer);
        ItemPoolManager.Instance.CreatePool(getScoreItemPrefab.GetComponent<GetScoreItem>(), 10, itemContainer);
        // 다른 아이템 풀도 생성
    }
}

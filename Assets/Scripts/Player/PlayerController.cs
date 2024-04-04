using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerStat playerStat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }
    }

    void UseAbility()
    {
        playerStat.playerAbility.Execute(playerStat);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 아이템 충돌 감지
        if (collision.gameObject.CompareTag("Item"))
        {
            // 아이템 획득 처리
        }

        // 투사체 충돌 감지
        if (collision.gameObject.CompareTag("Projectile"))
        {
            playerStat.TakeDamage();
        }
    }
}

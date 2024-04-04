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
        // ������ �浹 ����
        if (collision.gameObject.CompareTag("Item"))
        {
            // ������ ȹ�� ó��
        }

        // ����ü �浹 ����
        if (collision.gameObject.CompareTag("Projectile"))
        {
            playerStat.TakeDamage();
        }
    }
}

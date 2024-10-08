using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        // 스페이스바를 눌러 어빌리티 사용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseAbility();
        }
    }

    void UseAbility()
    {
        PlayerStat.Instance.playerAbility.Execute();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 충돌 감지
        if (collision.gameObject.CompareTag("Item"))
        {
            // 아이템 획득 처리
        }

        // 투사체 충돌 감지
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayerStat.Instance.TakeDamage();
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 투사체 충돌 감지
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PlayerStat.Instance.TakeDamage();
        }
    }
    */
}

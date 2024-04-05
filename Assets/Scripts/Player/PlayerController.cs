using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("�����Ƽ ����");
            UseAbility();
        }
    }

    void UseAbility()
    {
        PlayerStat.Instance.playerAbility.Execute();
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
            PlayerStat.Instance.TakeDamage();
        }
    }
}

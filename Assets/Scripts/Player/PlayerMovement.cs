using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private Vector2 InputVec;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �Է��� Update���� ó��
        // ���߿� ����Ƽ�� new Input system���� ���� ����
        InputVec = Vector2.zero;
        InputVec.x = Input.GetAxisRaw("Horizontal");
        InputVec.y = Input.GetAxisRaw("Vertical");

        // �÷��̾� ���� ��ȯ
        if (InputVec.x != 0)
        {
            spriteRenderer.flipX = InputVec.x < 0;
        }
    }

    void FixedUpdate()
    {
        // ���� ��� �̵��� FixedUpdate���� ó��
        Move();
    }

    void Move()
    {
        // velocity�� �̿��� �̵�
        rb.velocity = InputVec.normalized * PlayerStat.Instance.currentMoveSpeed * Time.fixedDeltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private Vector2 inputVec;

    // �б� ���� ������Ƽ
    public Vector2 InputVec
    {
        get { return inputVec; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �Է��� Update���� ó��
        // ���߿� ����Ƽ�� new Input system���� ���� ����
        inputVec = Vector2.zero;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // �÷��̾� ���� ��ȯ
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
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

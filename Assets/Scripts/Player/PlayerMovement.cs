using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStat playerStat;
    private Rigidbody2D rb;
    private Vector2 InputVec;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �Է��� Update���� ó��
        // ���߿� ����Ƽ�� new Input system���� ���� ����
        InputVec = Vector2.zero;
        InputVec.x = Input.GetAxisRaw("Horizontal");
        InputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // ���� ��� �̵��� FixedUpdate���� ó��
        Move();
    }

    void Move()
    {
        // Rigidbody�� ����Ͽ� �÷��̾� �̵�
        rb.velocity = InputVec.normalized * playerStat.currentMoveSpeed * Time.fixedDeltaTime;
    }
}

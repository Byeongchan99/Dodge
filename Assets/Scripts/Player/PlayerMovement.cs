using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    private Vector2 _inputVec;

    // �б� ���� ������Ƽ
    public Vector2 InputVec
    {
        get { return _inputVec; }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �Է��� Update���� ó��
        // ���߿� ����Ƽ�� new Input system���� ���� ����
        _inputVec = Vector2.zero;
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");

        // �÷��̾� ���� ��ȯ
        if (_inputVec.x != 0)
        {
            spriteRenderer.flipX = _inputVec.x < 0;
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
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    // �÷��̾� �ӵ� ����
    public void ChangeVelocity(Vector2 vec2)
    {
        rb.velocity = vec2;
    }

    // �÷��̾� ��ġ ����
    public void FreezePosition()
    {
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    // �÷��̾� ��ġ ���� ����
    public void UnFreezePosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

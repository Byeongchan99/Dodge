using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private Vector2 inputVec;

    // 읽기 전용 프로퍼티
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
        // 입력을 Update에서 처리
        // 나중에 유니티의 new Input system으로 변경 예정
        inputVec = Vector2.zero;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // 플레이어 방향 전환
        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    void FixedUpdate()
    {
        // 물리 기반 이동을 FixedUpdate에서 처리
        Move();
    }

    void Move()
    {
        // velocity를 이용한 이동
        rb.velocity = InputVec.normalized * PlayerStat.Instance.currentMoveSpeed * Time.fixedDeltaTime;
    }
}

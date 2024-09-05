using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private Vector2 _inputVec;

    // 읽기 전용 프로퍼티
    public Vector2 InputVec
    {
        get { return _inputVec; }
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
        _inputVec = Vector2.zero;
        _inputVec.x = Input.GetAxisRaw("Horizontal");
        _inputVec.y = Input.GetAxisRaw("Vertical");

        // 플레이어 방향 전환
        if (_inputVec.x != 0)
        {
            spriteRenderer.flipX = _inputVec.x < 0;
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

    // 플레이어 속도 변경
    public void ChangeVelocity(Vector2 vec2)
    {
        rb.velocity = vec2;
    }

    // 플레이어 위치 고정
    public void FreezePosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
    }

    // 플레이어 위치 고정 해제
    public void UnFreezePosition()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}

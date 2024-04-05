using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 InputVec;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 입력을 Update에서 처리
        // 나중에 유니티의 new Input system으로 변경 예정
        InputVec = Vector2.zero;
        InputVec.x = Input.GetAxisRaw("Horizontal");
        InputVec.y = Input.GetAxisRaw("Vertical");
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

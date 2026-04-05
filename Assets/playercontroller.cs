using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 5f; // ジャンプ力
    private Rigidbody rb;

    private Vector2 moveInput = Vector2.zero;

    private bool isGrounded = true; // 地面にいるかどうか

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // --- 移動入力 ---
    void OnMove(InputValue movementValue)
    {
        moveInput = movementValue.Get<Vector2>();
    }

    // --- ジャンプ入力 ---
    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;  // 空中状態に移行
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y);
        rb.AddForce(movement * speed);
    }

    // --- 地面に触れたら着地判定 ---
    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}
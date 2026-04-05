using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 10f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Vector3 inputDir;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // 回転を固定
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDir = new Vector3(h, 0, v).normalized;

        // --- 接地判定 ---
        // 足元に SphereCast を置くと確実
        isGrounded = Physics.SphereCast(transform.position, col.radius * 0.9f, Vector3.down, out RaycastHit hit, col.bounds.extents.y + 0.05f);

        // --- ジャンプ ---
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Y速度リセット
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (inputDir.magnitude >= 0.1f)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;
            camForward.y = 0;
            camRight.y = 0;

            Vector3 moveDir = camForward * inputDir.z + camRight * inputDir.x;
            moveDir.Normalize();

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);

            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // デバッグ用 Gizmos
    private void OnDrawGizmos()
    {
        if (col != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (col.bounds.extents.y + 0.05f));
        }
    }
}

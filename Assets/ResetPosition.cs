using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour
{
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ① 位置をリセット
            player.position = new Vector3(0, 5, 0);

            // ② 速度を完全停止（Rigidbody がある場合）
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;          // 移動速度をゼロ
                rb.angularVelocity = Vector3.zero;   // 回転速度もゼロ
            }
        }
    }
}
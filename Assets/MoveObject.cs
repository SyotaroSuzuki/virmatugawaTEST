using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Vector3 pointA = new Vector3(13f, -1.5f, 31.76f);
    public Vector3 pointB = new Vector3(30f, -1.5f, 31.76f);
    public float speed = 2f;

    private Vector3 target;
    private bool isActivated = false;   // ← 一度でも乗ったら true にして永続化

    void Start()
    {
        transform.position = pointA;
        target = pointB;
    }

    void Update()
    {
        if (isActivated)   // ← 一度でも起動したらずっと動き続ける
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            // 到着したら反転して戻る
            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                target = (target == pointA) ? pointB : pointA;
            }
        }
    }

    // プレイヤーが乗った瞬間に動き開始（その後はずっと動く）
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isActivated = true;
        }
    }
}

using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform target;   // CameraTarget
    public float distance = 5f;
    public float height = 2f;
    public float rotateSpeed = 150f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        rotationX += mouseX;
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -20f, 60f);
    }

    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, height, -distance);
        Quaternion rot = Quaternion.Euler(rotationY, rotationX, 0);

        transform.position = target.position + rot * dir;
        transform.LookAt(target);
    }
}

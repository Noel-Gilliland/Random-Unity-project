using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // The player to follow
    public Vector3 offset = new Vector3(0, 2, -5);
    public float smoothSpeed = 5f;
    public float rotationSpeed = 5f;
    public bool rotateWithMouse = true;

    private float yaw;
    private float pitch;

    void LateUpdate()
    {
        if (!target) return;

        if (rotateWithMouse)
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -35f, 60f);
        }

        target.rotation = Quaternion.Euler(0, yaw, 0);  // <--- rotates the player
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 desiredPosition = target.position + rotation * offset;
        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}

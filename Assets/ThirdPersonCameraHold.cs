using UnityEngine;

public class ThirdPersonCameraHold : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 2f, -4f);
    public float sensitivity = 2.5f;
    public float smooth = 12f;
    public float minPitch = -10f;
    public float maxPitch = 60f;

    float yaw;
    float pitch = 15f;

    void LateUpdate()
    {
        if (target == null) return;

        // 右クリック中だけ視点操作 + カーソルロック
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }

        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPos = target.position + rot * offset;

        transform.position = Vector3.Lerp(transform.position, desiredPos, smooth * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.2f);
    }
}
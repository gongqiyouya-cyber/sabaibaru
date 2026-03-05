using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // ’Ç‚¢‚©‚¯‚é‘ŠèiPlayerj
    public Vector3 offset = new Vector3(0, 3, -6);
    public float smooth = 10f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * smooth);
        transform.LookAt(target.position + Vector3.up * 1.5f); // ‚¿‚å‚¢ã‚ğŒ©‚é
    }
}
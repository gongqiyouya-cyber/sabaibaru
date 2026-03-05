using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -25f;

    CharacterController cc;
    float vy;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;

        // 地面に貼り付け（坂で沈みにくい）
        if (cc.isGrounded && vy < 0f) vy = -2f;
        vy += gravity * Time.deltaTime;

        move.y = vy;
        cc.Move(move * Time.deltaTime);
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;       // 歩きスピード
    public float runSpeed = 30f;        // ダッシュスピード
    public float rotateSpeed = 120f;    // 回転スピード
    public float gravity = -9.81f;      // 重力の強さ
    public float jumpHeight = 2f;       // ジャンプの高さ

    private CharacterController controller;
    private Vector3 velocity;           // 縦方向の動き（重力・ジャンプ）

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float move = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move = -1f;

        float turn = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) turn = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) turn = 1f;

        // Shift で走る
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // 回転
        transform.Rotate(Vector3.up, turn * rotateSpeed * Time.deltaTime);

        // 前後移動
        Vector3 forward = transform.forward * move * currentSpeed * Time.deltaTime;

        // --- 重力とジャンプ処理 ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 地面に張り付ける
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // ジャンプ
        }

        velocity.y += gravity * Time.deltaTime; // 重力を加算

        // 最終的な移動
        controller.Move(forward + velocity * Time.deltaTime);
    }
}


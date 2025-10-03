using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 10f;       // �����X�s�[�h
    public float runSpeed = 30f;        // �_�b�V���X�s�[�h
    public float rotateSpeed = 120f;    // ��]�X�s�[�h
    public float gravity = -9.81f;      // �d�͂̋���
    public float jumpHeight = 2f;       // �W�����v�̍���

    private CharacterController controller;
    private Vector3 velocity;           // �c�����̓����i�d�́E�W�����v�j

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

        // Shift �ő���
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        // ��]
        transform.Rotate(Vector3.up, turn * rotateSpeed * Time.deltaTime);

        // �O��ړ�
        Vector3 forward = transform.forward * move * currentSpeed * Time.deltaTime;

        // --- �d�͂ƃW�����v���� ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // �n�ʂɒ���t����
        }

        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // �W�����v
        }

        velocity.y += gravity * Time.deltaTime; // �d�͂����Z

        // �ŏI�I�Ȉړ�
        controller.Move(forward + velocity * Time.deltaTime);
    }
}


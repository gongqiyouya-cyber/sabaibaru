using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 10f;          // �ʏ�ړ����x
    public float sprintMultiplier = 1.8f;  // Shift�_�b�V���{��
    public float jumpForce = 5f;

    [Header("Look")]
    public Transform cameraPivot;          // Player�̎q��CameraPivot������
    public float mouseSensitivity = 0.8f;  // �����Ŋ��x����
    public float pitchMin = -60f;
    public float pitchMax = 75f;

    [Header("Ground Check")]
    public LayerMask groundLayer = ~0;
    public float groundCheckDistance = 1.1f;

    Rigidbody rb;
    bool isGrounded;
    float pitch;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // �����œ|��Ȃ��i����]�֎~�j
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // �}�E�X���b�N�iESC�ŉ����������Ȃ牺�ɒǉ�����j
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // �n�ʔ���i�J�v�Z�����S���牺�ցj
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // �W�����v
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // ���_����
        Look();

        // ESC�ŃJ�[�\�������i�K�v�Ȃ�j
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Look()
    {
        if (cameraPivot == null) return;

        float mx = Input.GetAxis("Mouse X") * mouseSensitivity;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // ���E�F�v���C���[�{�̂���
        transform.Rotate(0f, mx, 0f);

        // �㉺�FPivot������
        pitch -= my;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        float speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) speed *= sprintMultiplier;

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized * speed;

        Vector3 vel = rb.linearVelocity;
        rb.linearVelocity = new Vector3(moveDir.x, vel.y, moveDir.z);

        // ����ɉ��΍�i������Y��]�u�����~�߂�j
        rb.angularVelocity = Vector3.zero;
    }
}
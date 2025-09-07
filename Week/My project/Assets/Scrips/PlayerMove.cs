using UnityEngine;


//Player������Ʈ�� �ٿ��� �̵�, ī�޶� ȸ�� ���
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{

    [Header("�÷��̾� �̵� ����")]
    [Tooltip("�÷��̾��� �̵� �ӵ��Դϴ�.")]
    public float moveSpeed = 5f;
    [Tooltip("True�϶��� �̵� ����")]
    public bool canMove = true;

    [Header("���� �� �߷� ����")]
    [Tooltip("�÷��̾��� ���� �����Դϴ�.")]
    public float jumpHeight = 2f;
    [Tooltip("�߷� ���Դϴ�. ���� ���� �߷��� -9.81f�Դϴ�.")]
    public float gravity = -9.81f;
    [Tooltip("���� �پ��ֵ��� ���ϴ� �ּ� �߷��Դϴ�.")]
    public float groundedGravity = -2f;

    [Header("�ٴ� üũ ����")]
    [Tooltip("�ٴ��� ������ ��ġ�Դϴ�. �÷��̾� �߹ؿ� �ִ� ������Ʈ�� �Ҵ��ϼ���.")]
    public Transform groundCheck;
    [Tooltip("�ٴ��� ������ ������ �������Դϴ�.")]
    public float groundDistance = 0.4f;
    [Tooltip("� ���̾ �ٴ����� �ν����� �����մϴ�.")]
    public LayerMask groundMask;

    [Header("ī�޶� ����")]
    [Tooltip("�÷��̾ ������ ī�޶��� Transform�Դϴ�.")]
    public Transform playerCamera;
    [Tooltip("���콺 �����Դϴ�.")]
    public float mouseSensitivity = 100f;

    //����� ������
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float xRotation = 0f;
    private bool isGrounded;

    private void Start()
    {
        //������ �� CharacterController ������Ʈ�� ������
        controller = GetComponent<CharacterController>();

        //���� ���� �� ���콺 Ŀ���� ����� ȭ�� �߾ӿ� ����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    private void Update()
    {
        if(!canMove) return;

        float deltaTime = TunerManager.Instance.isTunerActive ? Time.unscaledDeltaTime : Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        HandleMovement(deltaTime);
        HandleGravityAndJump(deltaTime);
        HandleMouseLook(deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TunerManager.Instance.ToggleTuner();
        }

    }

    //�÷��̾� �̵� �Լ�
    private void HandleMovement(float deltaTime)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        controller.Move(moveDirection * moveSpeed * deltaTime);
    }

    //�÷��̾� ���� �� �߷� �Լ�
    private void HandleGravityAndJump(float deltaTime)
    {
        if(isGrounded && playerVelocity.y < 0f)
        {
            playerVelocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * deltaTime;
        controller.Move(playerVelocity * deltaTime);

    }

    //���콺 �Է��� ���� ī�޶� �� �÷��̾� ȸ�� �Լ�
    private void HandleMouseLook(float deltaTime)
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            // isGrounded ���¿� ���� ���� �ٲߴϴ�.
            Gizmos.color = isGrounded ? Color.green : Color.red;
            // GroundCheck ��ġ�� �ٴ� ���� ������ ������ �׷��ݴϴ�.
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

}

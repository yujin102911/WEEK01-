using UnityEngine;


//Player오브젝트에 붙여서 이동, 카메라 회전 담당
[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{

    [Header("플레이어 이동 설정")]
    [Tooltip("플레이어의 이동 속도입니다.")]
    public float moveSpeed = 5f;
    [Tooltip("True일때만 이동 가능")]
    public bool canMove = true;

    [Header("점프 및 중력 설정")]
    [Tooltip("플레이어의 점프 높이입니다.")]
    public float jumpHeight = 2f;
    [Tooltip("중력 값입니다. 실제 지구 중력은 -9.81f입니다.")]
    public float gravity = -9.81f;
    [Tooltip("땅에 붙어있도록 가하는 최소 중력입니다.")]
    public float groundedGravity = -2f;

    [Header("바닥 체크 설정")]
    [Tooltip("바닥을 감지할 위치입니다. 플레이어 발밑에 있는 오브젝트를 할당하세요.")]
    public Transform groundCheck;
    [Tooltip("바닥을 감지할 영역의 반지름입니다.")]
    public float groundDistance = 0.4f;
    [Tooltip("어떤 레이어를 바닥으로 인식할지 설정합니다.")]
    public LayerMask groundMask;

    [Header("카메라 설정")]
    [Tooltip("플레이어가 조작할 카메라의 Transform입니다.")]
    public Transform playerCamera;
    [Tooltip("마우스 감도입니다.")]
    public float mouseSensitivity = 100f;

    //비공개 변수들
    private CharacterController controller;
    private Vector3 playerVelocity;
    private float xRotation = 0f;
    private bool isGrounded;

    private void Start()
    {
        //시작할 때 CharacterController 컴포넌트를 가져옴
        controller = GetComponent<CharacterController>();

        //게임 시작 시 마우스 커서를 숨기고 화면 중앙에 고정
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

    //플레이어 이동 함수
    private void HandleMovement(float deltaTime)
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        controller.Move(moveDirection * moveSpeed * deltaTime);
    }

    //플레이어 점프 및 중력 함수
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

    //마우스 입력을 통한 카메라 및 플레이어 회전 함수
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
            // isGrounded 상태에 따라 색을 바꿉니다.
            Gizmos.color = isGrounded ? Color.green : Color.red;
            // GroundCheck 위치에 바닥 감지 영역을 원으로 그려줍니다.
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }

}

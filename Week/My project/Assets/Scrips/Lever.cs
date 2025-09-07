using UnityEngine;


//문여는 레버에 붙이는 스크립트
public class Lever : MonoBehaviour
{
    [Tooltip("이 레버가 열 문에 연결")]
    public DoorController targetDoor;

    [Tooltip("실제 눈에 보이는 레버 오브젝트 연결")]
    public GameObject leverBody;

    [Header("상호작용 설정")]
    [Tooltip("상호작용 최대 거리")]
    public float interactRange = 2.5f;
    [Tooltip("상호작용 대상을 'Player'레이어로 설정")]
    public LayerMask playerLayer;
    [Tooltip("UI")]
    public GameObject interactUI;


    //private bool canInteract = false;//Update내에 넣음으로 필요없음
    private Animator animator;
    private MeshRenderer leverRenderer;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(leverBody != null)
        {
            leverRenderer = leverBody.GetComponent<MeshRenderer>();
        }

        if(interactUI != null) interactUI.SetActive(false);

    }

    private void Update()
    {
        bool playerInRange = Physics.CheckSphere(transform.position, interactRange, playerLayer);

        bool isVisible = leverRenderer != null && leverRenderer.enabled;

        if(playerInRange && isVisible )
        {
            if (interactUI != null) interactUI.SetActive(true);

            if(Input.GetKeyDown(KeyCode.E))
            {
                if (animator != null) animator.SetTrigger("Pull");
                if (targetDoor != null) targetDoor.OpenDoor();

                if(interactUI != null) interactUI.SetActive(false);

                this.enabled = false;

            }

        }
        else
        {
            if (interactUI != null) interactUI.SetActive(false);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);

    }

}

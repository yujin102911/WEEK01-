using UnityEngine;


//������ ������ ���̴� ��ũ��Ʈ
public class Lever : MonoBehaviour
{
    [Tooltip("�� ������ �� ���� ����")]
    public DoorController targetDoor;

    [Tooltip("���� ���� ���̴� ���� ������Ʈ ����")]
    public GameObject leverBody;

    [Header("��ȣ�ۿ� ����")]
    [Tooltip("��ȣ�ۿ� �ִ� �Ÿ�")]
    public float interactRange = 2.5f;
    [Tooltip("��ȣ�ۿ� ����� 'Player'���̾�� ����")]
    public LayerMask playerLayer;
    [Tooltip("UI")]
    public GameObject interactUI;


    //private bool canInteract = false;//Update���� �������� �ʿ����
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

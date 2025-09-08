using UnityEngine;

//����ũ ���� ��ư�� ����
public class ButtonController : MonoBehaviour
{
    [Header("���� ���")]
    [Tooltip("�� ��ư�� ������ ��ֹ��� �����ϼ���")]
    public HazardController targetHazard;

    public GameObject buttonBody; //��ư ���̴��� �Ⱥ��̴��� Ȯ���� �� �ִ� ������Ʈ

    [Header("��ȣ�ۿ� UI")]
    public GameObject interactUI;

    private bool canInteract = false;
    private Animator animator;
    private MeshRenderer buttonRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (buttonBody != null ) buttonRenderer = buttonBody.GetComponent<MeshRenderer>();
        if (interactUI != null ) interactUI.SetActive(false);

    }

    private void Update()
    {

        bool isVisible = buttonRenderer != null && buttonRenderer.enabled;

        if (canInteract && isVisible)
        {
            if (interactUI != null) interactUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (animator != null) animator.SetTrigger("Press");
                if (targetHazard != null) targetHazard.DeactivateHazard();

                Debug.Log($"��ư{gameObject.name}�� �������ϴ�.");

                this.enabled = false;
                if(interactUI != null) interactUI.SetActive(false);
            }

        }
        else
        {
            if(interactUI != null) { interactUI.SetActive(false); }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = true;
 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canInteract = false;
    }

}

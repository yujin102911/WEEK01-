using UnityEngine;

//스파크 끄는 버튼에 부착
public class ButtonController : MonoBehaviour
{
    [Header("연결 대상")]
    [Tooltip("이 버튼이 제어할 장애물을 연결하세요")]
    public HazardController targetHazard;

    public GameObject buttonBody; //버튼 보이는지 안보이는지 확인할 수 있는 오브젝트

    [Header("상호작용 UI")]
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

                Debug.Log($"버튼{gameObject.name}을 눌렀습니다.");

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

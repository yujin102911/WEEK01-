using UnityEngine;

public class EndingButton : MonoBehaviour
{
    [Header("연결 대상")]
    [Tooltip("게임이 끝났을 때 활성화할 엔딩UI 패널을 연결")]
    public GameObject endingPenel;

    [Tooltip("'[E] 상호작용' UI 오브젝트")]
    public GameObject interactUI;

    private bool canInteract = false;


    private void Awake()
    {
        if (interactUI != null) interactUI.SetActive(false);
    }
    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (endingPenel != null) endingPenel.SetActive(true);

            PlayerMove player = FindAnyObjectByType<PlayerMove>();
            if (player != null) player.canMove = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (interactUI != null) interactUI.SetActive(false);

            this.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            if (interactUI != null) { interactUI.SetActive(false); }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract= true;
            if (interactUI != null) { interactUI.SetActive(true); }
        }
    }

}

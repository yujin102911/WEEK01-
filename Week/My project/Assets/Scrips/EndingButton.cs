using UnityEngine;

public class EndingButton : MonoBehaviour
{
    [Header("���� ���")]
    [Tooltip("������ ������ �� Ȱ��ȭ�� ����UI �г��� ����")]
    public GameObject endingPenel;

    [Tooltip("'[E] ��ȣ�ۿ�' UI ������Ʈ")]
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

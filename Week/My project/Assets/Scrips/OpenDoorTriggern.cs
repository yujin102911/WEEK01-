using UnityEngine;

public class OpenDoorTriggern : MonoBehaviour
{
    [Header("���� ���")]
    [Tooltip("�� Ʈ���Ű� ������ ���� ����")]
    public DoorController targetDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[OpenDoorTrigger] �÷��̾ {gameObject.name}�� �����߽��ϴ�.");

            if (targetDoor != null) targetDoor.OpenDoor();
        }

        gameObject.SetActive(false);

    }
}

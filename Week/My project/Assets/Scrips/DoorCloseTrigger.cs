using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [Tooltip("�� Ʈ���Ű� ���� ���� �����ϼ���.")]
    public DoorController targetDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("���� �����ϴ�.");

            if(targetDoor != null)
            {
                targetDoor.CloseDoor();
            }
            gameObject.SetActive(false);

        }
    }

}

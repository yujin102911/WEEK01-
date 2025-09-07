using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    [Tooltip("이 트리거가 닫을 문을 열결하세요.")]
    public DoorController targetDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("문이 닫힙니다.");

            if(targetDoor != null)
            {
                targetDoor.CloseDoor();
            }
            gameObject.SetActive(false);

        }
    }

}

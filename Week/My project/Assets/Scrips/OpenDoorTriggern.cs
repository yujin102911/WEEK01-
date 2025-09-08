using UnityEngine;

public class OpenDoorTriggern : MonoBehaviour
{
    [Header("연결 대상")]
    [Tooltip("이 트리거가 열어줄 문을 연결")]
    public DoorController targetDoor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[OpenDoorTrigger] 플레이어가 {gameObject.name}에 도달했습니다.");

            if (targetDoor != null) targetDoor.OpenDoor();
        }

        gameObject.SetActive(false);

    }
}

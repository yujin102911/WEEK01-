using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [Header("체크포인트 설정")]
    [Tooltip("이 체크포인트가 활성화할 스테이지 번호입니다.")]
    public int stageNumber;

    [Tooltip("이 체크포인트의 리스폰 위치입니다. 보통 이 오브젝트 자신을 연결합니다.")]
    public Transform respawnPointForThisStage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (StageManager.instance != null) StageManager.instance.UpdateStage(stageNumber, respawnPointForThisStage);
            else Debug.Log("씬에 StageManager가 없거나 Instance가 설정되지 않았습니다.");
           
        } 
        
        gameObject.SetActive(false); //한번만 작동하도록 자신을 비활성화
    }
}
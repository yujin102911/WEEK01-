using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [Header("üũ����Ʈ ����")]
    [Tooltip("�� üũ����Ʈ�� Ȱ��ȭ�� �������� ��ȣ�Դϴ�.")]
    public int stageNumber;

    [Tooltip("�� üũ����Ʈ�� ������ ��ġ�Դϴ�. ���� �� ������Ʈ �ڽ��� �����մϴ�.")]
    public Transform respawnPointForThisStage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (StageManager.instance != null) StageManager.instance.UpdateStage(stageNumber, respawnPointForThisStage);
            else Debug.Log("���� StageManager�� ���ų� Instance�� �������� �ʾҽ��ϴ�.");
           
        } 
        
        gameObject.SetActive(false); //�ѹ��� �۵��ϵ��� �ڽ��� ��Ȱ��ȭ
    }
}
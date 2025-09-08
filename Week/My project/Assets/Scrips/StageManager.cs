using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("�������� ����")]
    [Tooltip("���� �÷��̾ �ִ� �������� ��ȣ")]
    public int currentStage = 1;

    private void Awake() //�̱��� ����
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }

    //üũ����Ʈ Ʈ���Ű� �� �Լ��� ȣ���Ͽ� ���������� ������Ʈ
    public void UpdateStage(int newStage, Transform newRespawnPoint)
    {
        if (newStage <= currentStage) return;

        currentStage = newStage;
        Debug.LogWarning($"[StageManager]�������� ����. ���� ��������: {currentStage}");

        //���� �Ŵ������� ���ο� ����������Ʈ �˷���
        if (GameManager.instance != null) GameManager.instance.SetNewRespawnPoint(newRespawnPoint);

        if (TunerManager.Instance != null) TunerManager.Instance.UpdateVolumeForStage(currentStage);

    }

}

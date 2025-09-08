using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("스테이지 상태")]
    [Tooltip("현재 플레이어가 있는 스테이지 번호")]
    public int currentStage = 1;

    private void Awake() //싱글톤 패턴
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }

    //체크포인트 트리거가 이 함수를 호출하여 스테이지를 업데이트
    public void UpdateStage(int newStage, Transform newRespawnPoint)
    {
        if (newStage <= currentStage) return;

        currentStage = newStage;
        Debug.LogWarning($"[StageManager]스테이지 변경. 현재 스테이지: {currentStage}");

        //게임 매니져에게 새로운 리스폰포인트 알려줌
        if (GameManager.instance != null) GameManager.instance.SetNewRespawnPoint(newRespawnPoint);

        if (TunerManager.Instance != null) TunerManager.Instance.UpdateVolumeForStage(currentStage);

    }

}

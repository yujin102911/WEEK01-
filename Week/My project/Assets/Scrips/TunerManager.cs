using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//주파수 조율기의 기능 총괄
public class TunerManager : MonoBehaviour
{
    public static TunerManager Instance; //인스턴스로 만들면 다른 모든 스크립트에서 쉽게 접근 가능 -- TunerManager.instance

    [Header("조율기 상태")]
    public bool isTunerActive = false; //조율기가 현재 켜져있는가?
    public bool isTimerPaused = false; //타이머 일시정지

    [Header("조율기 설정")]
    [Tooltip("조율기가 켜져있는 최대 시간(초)")]
    public float tunerDuration = 10f;
    public int currentCharges = 3; //현재 남은 사용 횟수
    public int maxCharges = 3; //최대 사용 횟수

    [Header("실시간 상태")]
    [Tooltip("조율기 남은 시간")]
    public float currentTime;

    [Header("포스트 프로세싱 (URP)")]
    [Tooltip("조율기를 켰을 때 활성화할 전용 Volume 오브젝트를 연결하세요")]
    public GameObject tunerVolumeObject;
    private ColorAdjustments tunerColorAdjustments;

    private Coroutine tunerCoroutine;

    private void Awake()
    {
        // instance가 자기 자신을 가리키도록 설정 (Singleton 패턴)
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (tunerVolumeObject != null)
        {
            tunerVolumeObject.SetActive(false);
        } //일단 tuner Volume은 끄고 시작
    }

    public void PauseTimer()
    {
        isTimerPaused = true;
        Debug.Log("[TunerManager] 타이머 일시정지");

    }

    public void ResumeTimer()
    {
        isTimerPaused = false;
        Debug.Log("[TunerManager] 타이머 재개");
    }


    //Player가 'Q'를 누르면 작동하는 함수
    public void ToggleTuner()
    {
        //켜져있다면 끄고, 꺼져있다면 킴 (킴 횟수가 남아있을 때만)
        if (isTunerActive)
        {
            DeactivateTuner();
        }
        else if(currentCharges > 0)
        {
            ActivateTuner();
        }
        else
        {
            Debug.Log("조율기 에너지 부족");
        }
    }

    void ActivateTuner()
    {
        isTunerActive = true;
        currentCharges--;
        Debug.Log("조율기 활성화. 남은 횟수: " + currentCharges);

        currentTime = tunerDuration;

        //속도 느리게 1/10
        Time.timeScale = 0.1f;

        if (StageManager.instance != null) UpdateVolumeForStage(StageManager.instance.currentStage);

        if (tunerVolumeObject != null) tunerVolumeObject.SetActive(true);

        //타이머 코루틴을 시작하고, 나중에 중지시키기 위해 변수에 저장
        tunerCoroutine = StartCoroutine(TunerTimer());
    }

    void DeactivateTuner()
    {
        isTunerActive = false ;
        Debug.Log("조율기 비활성화");
        //정상 속도로
        Time.timeScale = 1.0f;

        if (tunerVolumeObject != null) tunerVolumeObject.SetActive(false);

        //만약 실행 중인 코루틴이 있다면 즉시 중지
        if(tunerCoroutine != null)
        {
            StopCoroutine(tunerCoroutine);
            tunerCoroutine = null;
        }

    }

    public void UpdateVolumeForStage(int stageNumber)
    {
        if (tunerVolumeObject == null) return;
        if(tunerVolumeObject.GetComponent<Volume>().profile.TryGet(out tunerColorAdjustments))
        {
            Debug.Log($"[TunerManager] 스테이지 {stageNumber}에 맞는 효과로 업데이트합니다.");
            if (stageNumber == 0) tunerColorAdjustments.postExposure.value = -2f;
            else if (stageNumber == 2) tunerColorAdjustments.postExposure.value = -3f;
            else if (stageNumber == 5) tunerColorAdjustments.postExposure.value = -7f;
        }

    }



    private IEnumerator TunerTimer()
    {
        while (currentTime > 0)
        {
            if (!isTimerPaused)
            {
                currentTime -= Time.unscaledDeltaTime;
            }
            yield return null;
        }
        currentTime = 0;
        if (isTunerActive)
        {
            Debug.Log("조율기 타임아웃");
            DeactivateTuner();
        }

    }

    public void AddCharge(int amount)
    {
        currentCharges = Mathf.Min(currentCharges + amount, maxCharges);
        Debug.Log("조율기 횟수 충전, 현재 횟수: " + currentCharges);

    }


}

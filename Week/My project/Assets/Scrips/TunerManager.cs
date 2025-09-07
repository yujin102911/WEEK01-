using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

//주파수 조율기의 기능 총괄
public class TunerManager : MonoBehaviour
{
    public static TunerManager Instance; //인스턴스로 만들면 다른 모든 스크립트에서 쉽게 접근 가능 -- TunerManager.instance

    [Header("조율기 상태")]
    public bool isTunerActive = false; //조율기가 현재 켜져있는가?

    [Header("조율기 설정")]
    [Tooltip("조율기가 켜져있는 최대 시간(초)")]
    public float tunerDuration = 10f;
    public int currentCharges = 3; //현재 남은 사용 횟수
    public int maxCharges = 3; //최대 사용 횟수

    [Header("실시간 상태")]
    [Tooltip("조율기 남은 시간")]
    public float currentTime;


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

        //타이머 코루틴을 시작하고, 나중에 중지시키기 위해 변수에 저장
        tunerCoroutine = StartCoroutine(TunerTimer());
    }

    void DeactivateTuner()
    {
        isTunerActive = false ;
        Debug.Log("조율기 비활성화");
        //정상 속도로
        Time.timeScale = 1.0f;
        //만약 실행 중인 코루틴이 있다면 즉시 중지
        if(tunerCoroutine != null)
        {
            StopCoroutine(tunerCoroutine);
            tunerCoroutine = null;
        }

    }

    private IEnumerator TunerTimer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.unscaledDeltaTime;
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

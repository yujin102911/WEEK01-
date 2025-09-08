using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

//���ļ� �������� ��� �Ѱ�
public class TunerManager : MonoBehaviour
{
    public static TunerManager Instance; //�ν��Ͻ��� ����� �ٸ� ��� ��ũ��Ʈ���� ���� ���� ���� -- TunerManager.instance

    [Header("������ ����")]
    public bool isTunerActive = false; //�����Ⱑ ���� �����ִ°�?
    public bool isTimerPaused = false; //Ÿ�̸� �Ͻ�����

    [Header("������ ����")]
    [Tooltip("�����Ⱑ �����ִ� �ִ� �ð�(��)")]
    public float tunerDuration = 10f;
    public int currentCharges = 3; //���� ���� ��� Ƚ��
    public int maxCharges = 3; //�ִ� ��� Ƚ��

    [Header("�ǽð� ����")]
    [Tooltip("������ ���� �ð�")]
    public float currentTime;

    [Header("����Ʈ ���μ��� (URP)")]
    [Tooltip("�����⸦ ���� �� Ȱ��ȭ�� ���� Volume ������Ʈ�� �����ϼ���")]
    public GameObject tunerVolumeObject;
    private ColorAdjustments tunerColorAdjustments;

    private Coroutine tunerCoroutine;

    private void Awake()
    {
        // instance�� �ڱ� �ڽ��� ����Ű���� ���� (Singleton ����)
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
        } //�ϴ� tuner Volume�� ���� ����
    }

    public void PauseTimer()
    {
        isTimerPaused = true;
        Debug.Log("[TunerManager] Ÿ�̸� �Ͻ�����");

    }

    public void ResumeTimer()
    {
        isTimerPaused = false;
        Debug.Log("[TunerManager] Ÿ�̸� �簳");
    }


    //Player�� 'Q'�� ������ �۵��ϴ� �Լ�
    public void ToggleTuner()
    {
        //�����ִٸ� ����, �����ִٸ� Ŵ (Ŵ Ƚ���� �������� ����)
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
            Debug.Log("������ ������ ����");
        }
    }

    void ActivateTuner()
    {
        isTunerActive = true;
        currentCharges--;
        Debug.Log("������ Ȱ��ȭ. ���� Ƚ��: " + currentCharges);

        currentTime = tunerDuration;

        //�ӵ� ������ 1/10
        Time.timeScale = 0.1f;

        if (StageManager.instance != null) UpdateVolumeForStage(StageManager.instance.currentStage);

        if (tunerVolumeObject != null) tunerVolumeObject.SetActive(true);

        //Ÿ�̸� �ڷ�ƾ�� �����ϰ�, ���߿� ������Ű�� ���� ������ ����
        tunerCoroutine = StartCoroutine(TunerTimer());
    }

    void DeactivateTuner()
    {
        isTunerActive = false ;
        Debug.Log("������ ��Ȱ��ȭ");
        //���� �ӵ���
        Time.timeScale = 1.0f;

        if (tunerVolumeObject != null) tunerVolumeObject.SetActive(false);

        //���� ���� ���� �ڷ�ƾ�� �ִٸ� ��� ����
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
            Debug.Log($"[TunerManager] �������� {stageNumber}�� �´� ȿ���� ������Ʈ�մϴ�.");
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
            Debug.Log("������ Ÿ�Ӿƿ�");
            DeactivateTuner();
        }

    }

    public void AddCharge(int amount)
    {
        currentCharges = Mathf.Min(currentCharges + amount, maxCharges);
        Debug.Log("������ Ƚ�� ����, ���� Ƚ��: " + currentCharges);

    }


}

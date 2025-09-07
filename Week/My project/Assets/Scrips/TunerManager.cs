using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

//���ļ� �������� ��� �Ѱ�
public class TunerManager : MonoBehaviour
{
    public static TunerManager Instance; //�ν��Ͻ��� ����� �ٸ� ��� ��ũ��Ʈ���� ���� ���� ���� -- TunerManager.instance

    [Header("������ ����")]
    public bool isTunerActive = false; //�����Ⱑ ���� �����ִ°�?

    [Header("������ ����")]
    [Tooltip("�����Ⱑ �����ִ� �ִ� �ð�(��)")]
    public float tunerDuration = 10f;
    public int currentCharges = 3; //���� ���� ��� Ƚ��
    public int maxCharges = 3; //�ִ� ��� Ƚ��

    [Header("�ǽð� ����")]
    [Tooltip("������ ���� �ð�")]
    public float currentTime;


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

        //Ÿ�̸� �ڷ�ƾ�� �����ϰ�, ���߿� ������Ű�� ���� ������ ����
        tunerCoroutine = StartCoroutine(TunerTimer());
    }

    void DeactivateTuner()
    {
        isTunerActive = false ;
        Debug.Log("������ ��Ȱ��ȭ");
        //���� �ӵ���
        Time.timeScale = 1.0f;
        //���� ���� ���� �ڷ�ƾ�� �ִٸ� ��� ����
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

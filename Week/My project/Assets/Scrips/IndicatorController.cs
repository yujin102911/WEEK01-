using UnityEngine;

[System.Serializable]
public class IndicatorController
{
    [Tooltip("�� ����ǥ�� Ȱ��ȭ�� Ʃ�丮�� �ܰ� ��ȣ")]
    public int targetStep;

    [Tooltip("Ȱ��ȭ��ų ����ǥ UI ������Ʈ")]
    public GameObject indicatorObject;

    //����ǥ�� Ȱ��ȭ �ϴ� �Լ�
    public void Activate()
    {
        if(indicatorObject != null)
        {
            indicatorObject.SetActive(true);
        }
    }

    //����ǥ�� ��Ȱ��ȭ �ϴ� �Լ�
    public void Deactivate()
    {
        if(indicatorObject != null)
        {
            indicatorObject.SetActive(false);
        }
    }

}

using UnityEngine;

[System.Serializable]
public class IndicatorController
{
    [Tooltip("이 느낌표가 활성화될 튜토리얼 단계 번호")]
    public int targetStep;

    [Tooltip("활성화시킬 느낌표 UI 오브젝트")]
    public GameObject indicatorObject;

    //느낌표를 활성화 하는 함수
    public void Activate()
    {
        if(indicatorObject != null)
        {
            indicatorObject.SetActive(true);
        }
    }

    //느낌표를 비활성화 하는 함수
    public void Deactivate()
    {
        if(indicatorObject != null)
        {
            indicatorObject.SetActive(false);
        }
    }

}

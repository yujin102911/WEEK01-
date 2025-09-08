using UnityEngine;

public class ConditionalTrigger : MonoBehaviour
{
    [Tooltip("이 트리거를 발동시킬 TutorialManager 단계")]
    public int targetTutorialStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.instance != null && GameManager.instance.isStage1LeverPulled)
            {
                Debug.Log("플레이어가 레버를 당긴 상태로 트리거에 들어옴. UI를 실행합니다.");

                TutorialManager manager = FindAnyObjectByType<TutorialManager>();
                if(manager != null ) { manager.TriggerTutorialStep(targetTutorialStep); }
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("레버를 당기지 않았으므로 UI를 실행하지 않습니다.");
            }
        }
    }


}

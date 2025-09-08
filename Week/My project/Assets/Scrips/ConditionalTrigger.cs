using UnityEngine;

public class ConditionalTrigger : MonoBehaviour
{
    [Tooltip("�� Ʈ���Ÿ� �ߵ���ų TutorialManager �ܰ�")]
    public int targetTutorialStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameManager.instance != null && GameManager.instance.isStage1LeverPulled)
            {
                Debug.Log("�÷��̾ ������ ��� ���·� Ʈ���ſ� ����. UI�� �����մϴ�.");

                TutorialManager manager = FindAnyObjectByType<TutorialManager>();
                if(manager != null ) { manager.TriggerTutorialStep(targetTutorialStep); }
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("������ ����� �ʾ����Ƿ� UI�� �������� �ʽ��ϴ�.");
            }
        }
    }


}

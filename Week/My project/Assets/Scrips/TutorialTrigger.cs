using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public int targetStep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TutorialManager manager = FindAnyObjectByType<TutorialManager>();
            if (manager != null)
            {
                manager.TriggerTutorialStep(targetStep);
                gameObject.SetActive(false);
            }
        }
    }
}

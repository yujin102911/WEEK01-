using UnityEngine;

//Tuner의 사용 횟수를 충전해 줄 수 있는 아이템에 부착하는 스크립트
public class TunerCharger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TunerManager.Instance.AddCharge(1);
            Destroy(gameObject);

        }
    }
}

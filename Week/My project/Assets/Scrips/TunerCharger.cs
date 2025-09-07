using UnityEngine;

//Tuner�� ��� Ƚ���� ������ �� �� �ִ� �����ۿ� �����ϴ� ��ũ��Ʈ
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

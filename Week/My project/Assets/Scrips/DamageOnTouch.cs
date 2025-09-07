using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("[DamageOnTouch]���. ������ ��û");

            if (GameManager.instance != null) GameManager.instance.RespawnPlayer();
            else Debug.LogError("[DamageOnTouch]���� GameManager�� ���ų� Ȥ�� instance�� �������� �ʾҽ��ϴ�.");

        }
    }
}

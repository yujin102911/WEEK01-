using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("[DamageOnTouch]사망. 리스폰 요청");

            if (GameManager.instance != null) GameManager.instance.RespawnPlayer();
            else Debug.LogError("[DamageOnTouch]씬에 GameManager가 없거나 혹은 instance가 설정되지 않았습니다.");

        }
    }
}

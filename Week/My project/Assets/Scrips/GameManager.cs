using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("플레이어 및 리스폰 설정")]
    [Tooltip("플레이어 오브젝트 연결")]
    public GameObject player;

    [Tooltip("리스폰 될 위치를 정하는 오브젝트를 연결")]
    public Transform respawnPoint;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        //디버그 기능 -- M을 누르면 조율기 횟수 충전
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(TunerManager.Instance != null)
            {
                TunerManager.Instance.AddCharge(1);
                Debug.LogWarning("[GameManager]디버그: 조율기 횟수 +1");
            }
        }
    }

    public void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            Debug.Log("[GameManager] 플레이어 리스폰");

            player.SetActive(false);
            player.transform.position = respawnPoint.position;
            player.SetActive(true);

        }
        else
        {
            Debug.LogError("[GameManager] 플레이어 혹은 리스폰 지점이 연결되지 않음");

        }

    }
}

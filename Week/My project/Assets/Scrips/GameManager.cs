using JetBrains.Annotations;
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

    [Header("게임 진행 상태")]
    [Tooltip("플레이어가 특정 레버를 당겼는지 여부를 검사")]
    public bool isStage1LeverPulled = false;

    [Tooltip("플레이어가 주파수 조율기를 사용할 수 있는지")]
    public bool canUseTuner = false;

    private float previousTimeScale = 1f;

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

    public void PauseGame()
    {

        previousTimeScale = Time.timeScale;

        Time.timeScale = 0f;
        Debug.Log("일시정지");
    }
    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale;
        Debug.Log("재개");
    }

    public void SetNewRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
        Debug.Log($"[GameManager] 새로운 리스폰 위치가 {newPoint.name}으로 설정되었습니다.");
    }

}

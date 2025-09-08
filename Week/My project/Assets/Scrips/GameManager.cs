using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("�÷��̾� �� ������ ����")]
    [Tooltip("�÷��̾� ������Ʈ ����")]
    public GameObject player;

    [Tooltip("������ �� ��ġ�� ���ϴ� ������Ʈ�� ����")]
    public Transform respawnPoint;

    [Header("���� ���� ����")]
    [Tooltip("�÷��̾ Ư�� ������ ������ ���θ� �˻�")]
    public bool isStage1LeverPulled = false;

    [Tooltip("�÷��̾ ���ļ� �����⸦ ����� �� �ִ���")]
    public bool canUseTuner = false;

    private float previousTimeScale = 1f;

    private void Awake()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        //����� ��� -- M�� ������ ������ Ƚ�� ����
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(TunerManager.Instance != null)
            {
                TunerManager.Instance.AddCharge(1);
                Debug.LogWarning("[GameManager]�����: ������ Ƚ�� +1");
            }
        }
    }

    public void RespawnPlayer()
    {
        if (player != null && respawnPoint != null)
        {
            Debug.Log("[GameManager] �÷��̾� ������");

            player.SetActive(false);
            player.transform.position = respawnPoint.position;
            player.SetActive(true);

        }
        else
        {
            Debug.LogError("[GameManager] �÷��̾� Ȥ�� ������ ������ ������� ����");

        }

    }

    public void PauseGame()
    {

        previousTimeScale = Time.timeScale;

        Time.timeScale = 0f;
        Debug.Log("�Ͻ�����");
    }
    public void ResumeGame()
    {
        Time.timeScale = previousTimeScale;
        Debug.Log("�簳");
    }

    public void SetNewRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
        Debug.Log($"[GameManager] ���ο� ������ ��ġ�� {newPoint.name}���� �����Ǿ����ϴ�.");
    }

}

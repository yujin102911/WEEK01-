using UnityEngine;

//Tunre에 반응할 객체에 붙여줄 스크립트
public class TunerObject : MonoBehaviour
{

    [Header("오브젝트 타입 설정")]
    [Tooltip("체크하면 조율기 켰을 때만 보임")]
    public bool appearInTunerWorld = true;


    private MeshRenderer meshRenderer;
    //private Collider objCollider;
    private Animator animator;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        //objCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        if (TunerManager.Instance != null)
        {
            HandleObjectState(TunerManager.Instance.isTunerActive);
        }

    }
    private void Update()
    {
        //매 프레임마다 TunerManager의 상태 확인
        HandleObjectState(TunerManager.Instance.isTunerActive);

    }
    void HandleObjectState(bool isTunerOn)
    {
        if (appearInTunerWorld)
        {
            if (meshRenderer != null) meshRenderer.enabled = isTunerOn;
            //if (objCollider != null) objCollider.enabled = isTunerOn;

        }
    }


}

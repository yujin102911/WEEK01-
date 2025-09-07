using UnityEngine;

//Tunre�� ������ ��ü�� �ٿ��� ��ũ��Ʈ
public class TunerObject : MonoBehaviour
{

    [Header("������Ʈ Ÿ�� ����")]
    [Tooltip("üũ�ϸ� ������ ���� ���� ����")]
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
        //�� �����Ӹ��� TunerManager�� ���� Ȯ��
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

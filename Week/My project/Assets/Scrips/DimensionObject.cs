using UnityEngine;

public class DimensionObject : MonoBehaviour
{
    public enum VisibilityState { Visible, Invisible }
    public enum CollisionState { Solid, PassThrough }

    [Header("���� ���� ����")]
    [Tooltip("���� ���迡�� �� ������Ʈ�� ���̳���?")]
    public VisibilityState realWorldVisibility = VisibilityState.Visible;
    [Tooltip("���� ���迡�� �� ������Ʈ�� ����������?")]
    public CollisionState realWorldCollision = CollisionState.Solid;

    [Header("���ļ� ���� ����")]
    [Tooltip("���ļ� ���迡�� �� ������Ʈ�� ���̳���?")]
    public VisibilityState tunerWorldVisibility = VisibilityState.Visible;
    [Tooltip("���ļ� ���迡�� �� ������Ʈ�� ����������?")]
    public CollisionState tunerWorldCollision = CollisionState.Solid;

    private MeshRenderer meshRenderer;
    private Collider objCollider;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        objCollider = GetComponent<Collider>();

    }

    private void Start()
    {
        if (TunerManager.Instance != null) UpdateObjectState(TunerManager.Instance.isTunerActive);

    }

    private void Update()
    {
        if (TunerManager.Instance != null) UpdateObjectState(TunerManager.Instance.isTunerActive);
    }


    private void UpdateObjectState(bool isTunerOn)
    {
        VisibilityState currentVisibility = isTunerOn? tunerWorldVisibility : realWorldVisibility;
        CollisionState currentCollision = isTunerOn? tunerWorldCollision : realWorldCollision;

        if (meshRenderer != null) meshRenderer.enabled = (currentVisibility == VisibilityState.Visible);
        if (objCollider != null) objCollider.enabled = (currentCollision == CollisionState.Solid);

    }
}

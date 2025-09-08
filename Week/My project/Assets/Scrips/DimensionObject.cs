using UnityEngine;

public class DimensionObject : MonoBehaviour
{
    public enum VisibilityState { Visible, Invisible }
    public enum CollisionState { Solid, PassThrough }

    [Header("현실 세계 상태")]
    [Tooltip("현실 세계에서 이 오브젝트가 보이나요?")]
    public VisibilityState realWorldVisibility = VisibilityState.Visible;
    [Tooltip("현실 세계에서 이 오브젝트가 만져지나요?")]
    public CollisionState realWorldCollision = CollisionState.Solid;

    [Header("주파수 세계 상태")]
    [Tooltip("주파수 세계에서 이 오브젝트가 보이나요?")]
    public VisibilityState tunerWorldVisibility = VisibilityState.Visible;
    [Tooltip("주파수 세계에서 이 오브젝트가 만져지나요?")]
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

using UnityEngine;

public class HazardController : MonoBehaviour
{
    [Header("연결 대상")]
    [Tooltip("자식으로 있는 스파크 파티클 시스템을 연결하세요")]
    public ParticleSystem[] sparkEffect;

    private DamageOnTouch damageScript;
    private Collider hazardCollider;

    private void Awake()
    {
        damageScript = GetComponent<DamageOnTouch>();
        hazardCollider = GetComponent<Collider>();

    }
    public void DeactivateHazard()
    {
        Debug.Log($"장애물{gameObject.name}을 비활성화");
        
        if (sparkEffect != null )
        {
            foreach (ParticleSystem effect in sparkEffect)
            {
                if(effect != null) effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }


        if (damageScript != null) damageScript.enabled = false;

        if (hazardCollider != null ) hazardCollider.enabled = false;

    }

}

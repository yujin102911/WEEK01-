using UnityEngine;

public class HazardController : MonoBehaviour
{
    [Header("���� ���")]
    [Tooltip("�ڽ����� �ִ� ����ũ ��ƼŬ �ý����� �����ϼ���")]
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
        Debug.Log($"��ֹ�{gameObject.name}�� ��Ȱ��ȭ");
        
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

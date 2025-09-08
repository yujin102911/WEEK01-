using UnityEngine;

public class LighthouseRotator : MonoBehaviour
{
    [Tooltip("빛이 회전하는 속도")]
    public float rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }


}
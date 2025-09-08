using UnityEngine;

public class LighthouseRotator : MonoBehaviour
{
    [Tooltip("���� ȸ���ϴ� �ӵ�")]
    public float rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }


}
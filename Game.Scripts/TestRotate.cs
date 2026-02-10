using UnityEngine;

public class TestRotate : MonoBehaviour
{
    public float sensitivity = 100f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
    }
}

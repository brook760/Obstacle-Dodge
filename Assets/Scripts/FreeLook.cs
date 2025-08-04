using Unity.Cinemachine;
using UnityEngine;

public class FreeLook : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float xSpeed = 120f;
    public float ySpeed = 80f;

    private float x = 0.0f;
    private float y = 10.0f;

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
        y = Mathf.Clamp(y, -20f, 80f);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}


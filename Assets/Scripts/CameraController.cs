using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float minY = -90f;
    public float maxY = 90f;
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    public Camera cam;

    private float rotationY = 0f;
    private float rotationX = 0f;

    void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() 
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

        rotationY = Mathf.Clamp(rotationY, minY, maxY);

        transform.localEulerAngles = new Vector3(0, rotationX, 0);
        cam.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}

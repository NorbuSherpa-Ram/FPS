using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour
{
    public float xSensitivity=10f;
    public float ySensitivity=10f;
    Transform playerTransform;
    float mouseX;
    float mouseY;
    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform  = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
    }
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X")*xSensitivity *Time.deltaTime ;
        mouseY = Input.GetAxis("Mouse Y")*ySensitivity*Time.deltaTime  ;
        xRotation -= mouseY;
        xRotation  = Mathf.Clamp(xRotation  ,-90, 90);
        transform.localRotation = Quaternion.Euler(xRotation,0,0);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}

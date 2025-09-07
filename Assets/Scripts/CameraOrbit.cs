using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 2, -5);
    public float mouseSensitivity = 3.0f;
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;

    private float yaw;   
    private float pitch; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    void HandleInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, bottomClamp, topClamp);
    }



    void UpdateCameraPosition()
    {
        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 targetPosition = player.position + rotation * offset;

        //Aggiorna dir
        transform.position = targetPosition;
        transform.LookAt(player.position + Vector3.up);
    }
}
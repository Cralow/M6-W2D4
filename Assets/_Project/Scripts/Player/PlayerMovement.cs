using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSmoothness = 0.2f;

    private Rigidbody rb;
    private Camera mainCamera;
    private PlayerAnimation anim;


    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        mainCamera = Camera.main;
        anim = GetComponent<PlayerAnimation>();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    public void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 moveDir = mainCamera.transform.forward * v + mainCamera.transform.right * h;
        moveDir.y = 0;
        if (moveDir.y > 1)
        {
            moveDir.Normalize();
        }

        if (moveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothness);
        }

        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
        anim.UpdateMovementAnimation(v);

    }

}
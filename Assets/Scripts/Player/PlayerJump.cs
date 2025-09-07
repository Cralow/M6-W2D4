using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private UnityEvent onJump;

    public int jumpsRemaining;
    private Rigidbody rb;
    private GroundChecker groundChecker;
    private PlayerAnimation anim;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        groundChecker = GetComponent<GroundChecker>();
        anim = GetComponent<PlayerAnimation>();
        jumpsRemaining = extraJumps;
    }

    private void Update()
    {
        if (groundChecker.IsGrounded)
            jumpsRemaining = extraJumps;

        if (Input.GetKeyDown(KeyCode.Space) && (groundChecker.IsGrounded || jumpsRemaining > 0))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (!groundChecker.IsGrounded)
                jumpsRemaining--;

            anim.OnJump();
            onJump.Invoke();
        }
    }
}
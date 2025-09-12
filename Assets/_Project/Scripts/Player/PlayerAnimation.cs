using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Particle System")]
    [SerializeField] private ParticleSystem _particleSystem;

    [Header("Animator Parameters")]
    [SerializeField] private string paramForward = "forward";
    [SerializeField] private string paramVSpeed = "vSpeed";
    [SerializeField] private string paramJump = "jump";
    [SerializeField] private string paramIsGrounded = "isGrounded";
    [SerializeField] private string paramIsBurning = "isBurning";
    [SerializeField] private float blendFactor = 2f;
    [SerializeField] private float burningTimeReset = 0.5f;

    private Animator anim;
    private Rigidbody rb;
    private GroundChecker groundCheck;
    private PlayerJump jumpComponent;     
    private Coroutine burningResetCoroutine;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        groundCheck = GetComponentInChildren<GroundChecker>();
        jumpComponent = GetComponent<PlayerJump>();

        groundCheck.onIsGroundedChanged.AddListener(OnIsGroundedChanged);
    }

    public void FreeFallAnim() =>
        anim.SetFloat(paramVSpeed, rb.velocity.y);

    public void UpdateMovementAnimation(float velocity) =>
        anim.SetFloat(paramForward, velocity * blendFactor);

    public void OnJump() =>
        anim.SetTrigger(paramJump);

    public void OnIsGroundedChanged(bool grounded)
    {
        anim.SetBool(paramIsGrounded, grounded);

        if (grounded)
        {
            if (burningResetCoroutine != null)
                StopCoroutine(burningResetCoroutine);

            burningResetCoroutine = StartCoroutine(ResetBurningAfterDelay());
        }
    }

    public void OnIsBurningChanged(bool burning)
    {
        anim.SetBool(paramIsBurning, burning);

        if (burning)
        {
            // Disabilita subito il salto
            if (jumpComponent != null)
                jumpComponent.enabled = false;

            if (burningResetCoroutine != null)
            {
                StopCoroutine(burningResetCoroutine);
                burningResetCoroutine = null;
            }
        }
    }

    private IEnumerator ResetBurningAfterDelay()
    {
        yield return new WaitForSeconds(burningTimeReset);
       

        anim.SetBool(paramIsBurning, false);

        if (jumpComponent != null)
            jumpComponent.enabled = true;

        burningResetCoroutine = null;
    }
}
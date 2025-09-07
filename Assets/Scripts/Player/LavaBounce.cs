using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBounce : MonoBehaviour
{
    public float bounceForce = 20f;
    public string lavaTag = "Lava";

    private Rigidbody rb;
    private bool canBounce = true;
    public float bounceCooldown = 0.5f; // per evitare rimbalzi multipli

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.tag == lavaTag && canBounce)
        {
            Bounce();
            gameObject.GetComponent<PlayerJump>().jumpsRemaining = 0;
            gameObject.GetComponent<LifeController>().TakeDamage(1);
            gameObject.GetComponent<PlayerAnimation>().OnIsBurningChanged(true);
        }
    }
 

    void Bounce()
    {
        //velocit� e rimbalzo
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
        canBounce = false;
        Invoke(nameof(ResetBounce), bounceCooldown);
    }

    void ResetBounce()
    {
        canBounce = true;
    }
}
using UnityEngine;
using System.Collections;

public class FallPlatform : MonoBehaviour
{
    public float fallDelay = 0.5f;       // Ritardo prima della caduta
    public float disableDelay = 2f;      // Tempo dopo cui disattivare la piattaforma
    public float resetDelay = 5f;       

    private Rigidbody rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rb.isKinematic = true; // Inizia ferma
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasFallen) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            hasFallen = true;
            Invoke(nameof(Fall), fallDelay);
        }
    }

    private void Fall()
    {
        rb.isKinematic = false;
        StartCoroutine(HandleDisableAndReset());
    }

    private IEnumerator HandleDisableAndReset()
    {
        yield return new WaitForSeconds(disableDelay);

        gameObject.GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(resetDelay);

        ResetPlatform();
    }

    private void ResetPlatform()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        // Riposiziona e resetta
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        hasFallen = false;
    }
}
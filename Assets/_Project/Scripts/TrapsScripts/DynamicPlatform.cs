using System.Collections;
using UnityEngine;

public class DynamicPlatform : MonoBehaviour
{
   [SerializeField] private string PlayerTag ="Player";
    public float resetDuration = 2f;   
    public float resetDelay = 1f;      

    private Rigidbody rb;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Coroutine resetCoroutine;
    private bool playerOnPlatform = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb.isKinematic = true; 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            playerOnPlatform = true;

            if (resetCoroutine != null)
            {
                StopCoroutine(resetCoroutine);
                resetCoroutine = null;
            }

            rb.isKinematic = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(PlayerTag))
        {
            playerOnPlatform = false;

            if (resetCoroutine == null)
                resetCoroutine = StartCoroutine(ResetPlatformAfterDelay());
        }
    }

    private IEnumerator ResetPlatformAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        if (playerOnPlatform)
        {
            resetCoroutine = null;
            yield break;
        }

        rb.isKinematic = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;
        float t = 0f;

        while (t < resetDuration)
        {
            t += Time.deltaTime;
            float lerpT = Mathf.Clamp01(t / resetDuration);

            transform.position = Vector3.Lerp(startPos, initialPosition, lerpT);
            transform.rotation = Quaternion.Lerp(startRot, initialRotation, lerpT);

            yield return null;
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        resetCoroutine = null;
    }
}

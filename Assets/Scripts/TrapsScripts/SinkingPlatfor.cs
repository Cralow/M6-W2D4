using UnityEngine;
using System.Collections;

public class SinkingPlatfor : MonoBehaviour
{
    private float maxSinkAmount = 5f;      // Affondamento massimo
    public float sinkDuration = 3f;       // Quanto ci mette per affondare al massimo
    public float riseDuration = 1f;     // Quanto ci mette per tornare su

    private Vector3 originalPosition;
    private Coroutine currentCoroutine;
    private bool isPlayerOnPlatform = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;

            // Se c'è una coroutine in corso, la fermiamo prima di avviarne una nuova
            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(SinkPlatform());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;

            if (currentCoroutine != null)
                StopCoroutine(currentCoroutine);

            currentCoroutine = StartCoroutine(RisePlatform());
        }
    }

    IEnumerator SinkPlatform()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = originalPosition - new Vector3(0, maxSinkAmount, 0);

        while (elapsed < sinkDuration && isPlayerOnPlatform)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / sinkDuration;
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }
    }

    IEnumerator RisePlatform()
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;

        while (elapsed < riseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / riseDuration;
            transform.position = Vector3.Lerp(startPos, originalPosition, t);
            yield return null;
        }

        // Safety reset
        transform.position = originalPosition;
    }
}

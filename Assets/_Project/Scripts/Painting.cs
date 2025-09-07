using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private Coroutine rippleRoutine;
    [SerializeField] private float rippleTime = 1.5f;
    [SerializeField] private float maxRippleStrength = 0.75f;

    public bool isInPlayerInPainting;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var mat = GetComponent<Renderer>().material;
            mat.SetVector("_RippleCenter", other.ClosestPoint(transform.position));

            if (rippleRoutine != null)
            {
                StopCoroutine(rippleRoutine);
            }

            rippleRoutine = StartCoroutine(DoRipple(mat));
            isInPlayerInPainting = true;
        }
    }

    private IEnumerator DoRipple(Material mat)
    {
        for (float t = 0.0f; t < rippleTime; t += Time.deltaTime)
        {
            mat.SetFloat("_RippleStrength", maxRippleStrength * (1.0f - t / rippleTime));
            yield return null;
        }
    }
}
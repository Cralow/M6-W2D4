using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiVisibility : MonoBehaviour
{
    [SerializeField] private CanvasGroup target;
    [SerializeField] private float interval = 1f;

    void Start()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        while (true)
        {
            target.alpha = 1f;
            yield return new WaitForSeconds(interval);
            target.alpha = 0f;
            yield return new WaitForSeconds(interval);
        }
    }
}
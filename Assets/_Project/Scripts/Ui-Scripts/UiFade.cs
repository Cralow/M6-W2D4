using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFade : MonoBehaviour
{
    [SerializeField] private CanvasGroup uiGroup;

    private void Start()
    {
        uiGroup.alpha = 1f; 
        StartCoroutine(FadeTo(1f, 0f)); 
    }
    public void SetFade(float time, float direction)
    {
        StartCoroutine(FadeTo(time, direction));
    }

    private IEnumerator FadeTo(float fadeDuration,float targetAlpha)
    {
        float startAlpha = uiGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            uiGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            yield return null;
        }

        uiGroup.alpha = targetAlpha;
    }

    public void OnStartButtonPress()
    {
        FadeTo(2f, 1f);
    }
}
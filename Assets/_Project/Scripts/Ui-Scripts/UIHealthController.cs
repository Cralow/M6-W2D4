using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [Header("UI")]
    public Image lifeImage;
    public Sprite[] lifeSprites; // 9 sprites (da 0 a 8 tacche)

    [Header("Durata visibilità")]
    public float visibleDuration = 2f;

    private Coroutine visibilityCoroutine;
    private LifeController lifeController;

    void Awake()
    {
        lifeImage.gameObject.SetActive(false);

        lifeController = GetComponent<LifeController>();
        lifeController.UpdateLifeCanvas += UpdateLifeUI;
    }
    private void Update()
    {

    }
    public void UpdateLifeUI(int currentlife)
    {
        if (lifeImage != null && lifeSprites.Length > currentlife)
        {
            lifeImage.sprite = lifeSprites[currentlife];

            if (visibilityCoroutine != null)
                StopCoroutine(visibilityCoroutine);

            visibilityCoroutine = StartCoroutine(ShowTemporarily());
        }
    }

    IEnumerator ShowTemporarily()
    {
        lifeImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(visibleDuration);
        lifeImage.gameObject.SetActive(false);
    }
}

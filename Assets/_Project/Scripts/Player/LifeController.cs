using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [Header("Vita")]
    public int maxLife = 8;
    public int currentLife = 8;

    [Header("UI")]
    public Image lifeImage;
    public Sprite[] lifeSprites; // 9 sprites (da 0 a 8 tacche)

    [Header("Durata visibilità")]
    public float visibleDuration = 2f;

    private Coroutine visibilityCoroutine;
    [SerializeField] GameOverController GOC;

   

    void Start()
    {
        UpdateLifeUI();
        lifeImage.gameObject.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        UpdateLifeUI();

        if (currentLife <= 0)
        {
            GOC.HandleDeath();
        }
    }
    public void Heal(int amount)
    {
        currentLife += amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        UpdateLifeUI();
    }

    void UpdateLifeUI()
    {
        if (lifeImage != null && lifeSprites.Length > currentLife)
        {
            lifeImage.sprite = lifeSprites[currentLife];

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
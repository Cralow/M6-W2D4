using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [Header("Vita")]
    public int maxLife = 8;
    public int currentLife = 8;

    public Action<int> UpdateLifeCanvas;
    [SerializeField] GameOverController GOC;

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        UpdateLifeCanvas(currentLife);

        if (currentLife <= 0)
        {
            GOC.HandleDeath();
        }
    }
    public void Heal(int amount)
    {
        currentLife += amount;
        currentLife = Mathf.Clamp(currentLife, 0, maxLife);
        UpdateLifeCanvas(currentLife);
    }

   
}
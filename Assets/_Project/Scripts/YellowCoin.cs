using UnityEngine;

public class YellowCoin : MonoBehaviour
{
    [Header("Coin Settings")]
    public int scoreAmount = 1;
    ScoreController scoreController;
    LifeController lifeController;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreController = other.GetComponent<ScoreController>();
            lifeController = other.GetComponent<LifeController>();
            AddScore(scoreController);
            Destroy(gameObject);
        }
    }

    protected virtual void AddScore(ScoreController sC)
    {
        lifeController.Heal(scoreAmount);
        sC.score += scoreAmount;
        sC.UpdateScore();
    }
}
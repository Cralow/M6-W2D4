using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] public int score;
    void Start()
    {
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text=score.ToString();
    }
}

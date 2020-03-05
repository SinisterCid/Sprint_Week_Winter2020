using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text score;
    public Text lives;

    GameObject gameManager;
    GameManager gameManagerScript;
    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.currentUIManager = GetComponent<UIManager>();

        score.text = "Score: " + 0.ToString();
        lives.text = "Lives: " + gameManagerScript.numberOfLives.ToString();
    }

    public void UpdateScoreText(int numberForScore)
    {
        score.text = "Score: " + numberForScore.ToString();
    }

    public void UpdateLivesText(int numberOfLives)
    {
        lives.text = "Lives: " + numberOfLives.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    GameObject gameManager;
    GameManager gameManagerScript;
    Text ScoreText;
    
   

    public int coinPoints;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.collectables.Add(gameObject);
        ScoreText = GetComponent<Text>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameManagerScript.IncreaseScore(coinPoints);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    GameObject gameManager;
    GameManager gameManagerScript;

    public int coinPoints;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameManagerScript.IncreaseScore(coinPoints);
            gameObject.SetActive(false);
        }
    }
}

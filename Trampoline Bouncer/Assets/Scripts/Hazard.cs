using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    GameObject gameManager;
    GameManager gameManagerScript;
    AudioSource deathSound;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        deathSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            gameManagerScript.LoseALife();
            deathSound.Play();
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public int health;
    public float destroyTime;
    public float momentumLoss;
    public float bounceBackLossMultiplier;

    GameObject gameManagerObj;
    GameObject playerObj;
    Rigidbody2D playerRB;
    float velocityBeforePhysicsUpdate;
    Animator anim;


    private void Awake()
    {
        gameManagerObj = GameObject.Find("Game Manager");
        gameManagerObj.GetComponent<GameManager>().breakableObjects.Add(gameObject);
        playerObj = GameObject.Find("Player");
        playerRB = playerObj.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = playerRB.velocity.y;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            health--;
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();

            if (playerScript.hasBounced == true)
            {
                playerScript.baseBounceVelocity = Mathf.Clamp(playerScript.baseBounceVelocity - momentumLoss, 0.5f, Mathf.Infinity);
                playerScript.hasBounced = false;
            }
            if (velocityBeforePhysicsUpdate < 0)
                playerRB.velocity = new Vector3(playerRB.velocity.x, -velocityBeforePhysicsUpdate * bounceBackLossMultiplier);

            if (health == 1)
            {
                anim.SetBool("WasHit", true);
                anim.SetFloat("Break", 2);
                Debug.Log("Animation was changed");
            }

            if (health <= 0)
                //anim.SetFloat("Break", 0);
                gameObject.SetActive(false);
        }
    }
}

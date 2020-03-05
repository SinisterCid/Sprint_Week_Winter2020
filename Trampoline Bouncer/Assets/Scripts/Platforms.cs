using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public float momentumLoss;
    public float bounceBackLossMultiplier;

    GameObject gameManagerObj;
    GameObject playerObj;
    Rigidbody2D playerRB;
    float velocityBeforePhysicsUpdate;

    private void Awake()
    {
        gameManagerObj = GameObject.Find("Game Manager");
        playerObj = GameObject.Find("Player");
        playerRB = playerObj.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = playerRB.velocity.y;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
            if (playerScript.hasBounced == true)
            {
                playerScript.baseBounceVelocity = Mathf.Clamp(playerScript.baseBounceVelocity - momentumLoss, 0.5f, Mathf.Infinity);
                playerScript.hasBounced = false;
            }

            if (velocityBeforePhysicsUpdate < 0)
                playerRB.velocity = new Vector3(playerRB.velocity.x, -velocityBeforePhysicsUpdate * bounceBackLossMultiplier);
        }
    }
}

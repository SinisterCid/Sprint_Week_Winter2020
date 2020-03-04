using System.Collections;
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

    private void Awake()
    {
        gameManagerObj = GameObject.Find("Game Manager");
        gameManagerObj.GetComponent<GameManager>().breakableObjects.Add(gameObject);
        playerObj = GameObject.Find("Player");
        playerRB = playerObj.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocityBeforePhysicsUpdate = playerRB.velocity.y;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            health--;
            PlayerController playerScript = other.gameObject.GetComponent<PlayerController>();
            playerScript.baseBounceVelocity = playerScript.baseBounceVelocity - momentumLoss;
            if (velocityBeforePhysicsUpdate < 0)
                playerRB.velocity = new Vector3(playerRB.velocity.x, -velocityBeforePhysicsUpdate * bounceBackLossMultiplier);

            if (health <= 0)
                gameObject.SetActive(false);
        }
    }
}

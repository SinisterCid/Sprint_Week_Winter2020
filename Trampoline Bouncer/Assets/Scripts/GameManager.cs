using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int numberOfLives;
    float score = 0;
    float highScore;
    public List<GameObject> breakableObjects = new List<GameObject>();
    public GameObject player;
    public Transform playerStartPosition;
    public float startBounceVelocity;

    private void Update()
    {
        CalculateHighScore();
    }

    void CalculateHighScore()
    {
        if (score > highScore)
            highScore = score;
    }

    public void LoseLife()
    {
        numberOfLives--;
        foreach(GameObject breakable in breakableObjects)
        {
            breakable.SetActive(true);
        }
        player.transform.position = playerStartPosition.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<PlayerController>().baseBounceVelocity = startBounceVelocity;
    }
}

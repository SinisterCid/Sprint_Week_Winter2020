using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxLives;

    [Space]
    [Header("Don't touch past here")]
    int numberOfLives;
    float score = 0;
    float highScore;
    public List<GameObject> breakableObjects = new List<GameObject>();
    public List<GameObject> collectables = new List<GameObject>();
    public GameObject player;
    public Transform playerStartPosition;
    public float startBounceVelocity;

    public float highestPlayerHeight;

    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        numberOfLives = maxLives;
    }

    private void Update()
    {
        CalculateHighScore();
        if (player != null)
            CalculateHighestPlayerHeight();
    }


    void CalculateHighestPlayerHeight()
    {
        if(highestPlayerHeight < player.transform.position.y)
        {
            IncreaseScore(Mathf.RoundToInt(player.transform.position.y - highestPlayerHeight));
            highestPlayerHeight = Mathf.RoundToInt(player.transform.position.y);
        }
            
    }

    public void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
    }

    void CalculateHighScore()
    {
        if (score > highScore)
            highScore = score;
    }

    public void LoseALife()
    {
        numberOfLives--;
        if (numberOfLives <= 0)
            LostAllLives();
        else

        foreach(GameObject breakable in breakableObjects)
        {
            breakable.SetActive(true);
            breakable.GetComponent<Breakable>().health = 3;
        }
        foreach(GameObject coin in collectables)
        {
            coin.SetActive(true);
        }

        player.transform.position = playerStartPosition.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<PlayerController>().baseBounceVelocity = startBounceVelocity;
        highestPlayerHeight = playerStartPosition.position.y;
        score = 0;
    }

    //Set up with UI
    public void StartButton()
    {

    }

    public void Options()
    {

    }

    void LostAllLives()
    {
        collectables.Clear();
        breakableObjects.Clear();
        Application.Quit();
    }
}

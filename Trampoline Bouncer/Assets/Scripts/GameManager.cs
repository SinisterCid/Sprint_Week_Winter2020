using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxLives;
    public float waitTimeInSeconds;

    [Space]
    [Header("Don't touch past here")]
    public int numberOfLives;
    public float score = 0;
    float highScore;
    public UIManager currentUIManager;

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
        if (highestPlayerHeight < player.transform.position.y)
        {
            IncreaseScore(Mathf.RoundToInt(player.transform.position.y - highestPlayerHeight));
            highestPlayerHeight = Mathf.RoundToInt(player.transform.position.y);
        }

    }

    public void IncreaseScore(int scoreIncrease)
    {
        score += scoreIncrease;
        currentUIManager.UpdateScoreText(Mathf.RoundToInt(score));
    }

    void CalculateHighScore()
    {
        if (score > highScore)
            highScore = score;
    }

    public void LoseALife()
    {
        numberOfLives--;
        currentUIManager.UpdateLivesText(numberOfLives);
        player.GetComponent<CircleCollider2D>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<TrailRenderer>().enabled = false;

        if (numberOfLives <= 0)
            LostAllLives();
        else
        {
            StartCoroutine(ResetLevel());
        }
    }

    IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(waitTimeInSeconds);

        player.GetComponent<CircleCollider2D>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<TrailRenderer>().enabled = true;
        foreach (GameObject breakable in breakableObjects)
        {
            breakable.GetComponent<BoxCollider2D>().enabled = true;
            breakable.GetComponent<SpriteRenderer>().enabled = true;
            breakable.GetComponent<Breakable>().health = 3;
        }
        foreach (GameObject coin in collectables)
        {
            coin.SetActive(true);
        }

        player.transform.position = playerStartPosition.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<PlayerController>().baseBounceVelocity = startBounceVelocity;
        highestPlayerHeight = playerStartPosition.position.y;
        score = 0;
        currentUIManager.UpdateScoreText(Mathf.RoundToInt(score));
    }

    void LostAllLives()
    {

        collectables.Clear();
        breakableObjects.Clear();
        StartCoroutine(ChangeScreens(0));
        numberOfLives = maxLives;
    }

    public void CrossFinishLine()
    {
        player.GetComponent<PlayerController>().enabled = false;
        collectables.Clear();
        breakableObjects.Clear();
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            StartCoroutine(ChangeScreens(SceneManager.GetActiveScene().buildIndex + 1));
        else
        {
            StartCoroutine(ChangeScreens(0));
        }

    }

    IEnumerator ChangeScreens(int scene)
    {
        yield return new WaitForSeconds(waitTimeInSeconds);
        SceneManager.LoadScene(scene);
    }

    //Set up with UI
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }


}

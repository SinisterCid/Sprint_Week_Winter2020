using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    GameObject gameManager;
    GameManager gameManagerScript;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager");
        gameManagerScript = gameManager.GetComponent<GameManager>();
        gameManagerScript.playerStartPosition = gameObject.transform;
        gameManagerScript.highestPlayerHeight = Mathf.RoundToInt(transform.position.y);
    }
}

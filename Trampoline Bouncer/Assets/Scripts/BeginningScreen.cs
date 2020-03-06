using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginningScreen : MonoBehaviour
{

    bool onStartButton = true;
    public GameObject startArrow;
    public GameObject exitArrow;

    // Update is called once per frame
    void Update()
    {
        ButtonControls();
    }

    void ButtonControls()
    {
        if (Input.GetAxisRaw("Vertical") == 1)
            onStartButton = true;
        else if (Input.GetAxisRaw("Vertical") == -1)
            onStartButton = false;

        if (onStartButton)
        {
            startArrow.SetActive(true);
            exitArrow.SetActive(false);
        }
        else
        {
            startArrow.SetActive(false);
            exitArrow.SetActive(true);
        }

        if (Input.GetKeyDown("left ctrl"))
        {
            if (onStartButton)
                SceneManager.LoadScene(1);
            else Application.Quit();
        }
    }

}

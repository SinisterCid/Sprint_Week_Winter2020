using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningScreen : MonoBehaviour
{

    public Vector3 input;
    public float force;

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(-input *force * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(Input.GetKeyDown("Left Ctrl"))
        {
            //GameManager.StartButton();
        }
    }
}

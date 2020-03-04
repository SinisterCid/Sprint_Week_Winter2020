using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Bounce")]
    public float extraBounceForce;
    public float baseBounceVelocity;

    [Header("Vertical Movement")]
    public float gravity;
    public float terminalVelocity;

    [Header("Horizontal Movement")]
    public float maxSpeed;
    public float minimumMaxSpeed;
    public float maximumMaxSpeed;
    public float acceleration;
    public float deceleration;

    bool gravityOn;

    GameObject gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("Game Manager");
    }

    private void Start()
    {
        rb.velocity = new Vector2(0, baseBounceVelocity);
        gravityOn = true;
        gameManager.GetComponent<GameManager>().player = gameObject;
        gameManager.GetComponent<GameManager>().startBounceVelocity = baseBounceVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        MaxSpeedVariation();
        PlayerMovement();
        if (gravityOn)
            Gravity();
    }

    void PlayerMovement()
    {
        if (Input.GetAxisRaw("Horizontal") == -1 )
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - acceleration * Time.deltaTime, -maxSpeed, maxSpeed), rb.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + acceleration * Time.deltaTime, -maxSpeed, maxSpeed), rb.velocity.y);
        }
        else if (rb.velocity.x < 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + deceleration * Time.deltaTime, -maxSpeed, 0), rb.velocity.y);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x - deceleration * Time.deltaTime, 0, maxSpeed), rb.velocity.y);
        }
    }

    void MaxSpeedVariation()
    {
        maxSpeed = minimumMaxSpeed + Mathf.Clamp(Mathf.Abs(rb.velocity.y), 0, terminalVelocity) / terminalVelocity * (maximumMaxSpeed - minimumMaxSpeed);
    }

    void Gravity()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y - gravity * Time.deltaTime, -terminalVelocity, Mathf.Infinity));
    }

    void Bounce()
    {
        
        rb.velocity = new Vector2(rb.velocity.x, baseBounceVelocity + extraBounceForce);
        baseBounceVelocity = baseBounceVelocity + extraBounceForce;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Bounce();
        }
    }
}
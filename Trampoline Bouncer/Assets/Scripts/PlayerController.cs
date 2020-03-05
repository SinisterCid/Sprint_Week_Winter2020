using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerAnimations anims;
    public ParticleSystem trampolineSparks;
    public ParticleSystem wallSparks;
    public ParticleSystem platformSparks;
    public ParticleSystem breakableSparks;
    public ParticleSystem coinSparks;
    public ParticleSystem hazardSparks;
    public ParticleSystem finishLineSparks;

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

    public bool hasBounced = false;

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
        anims = gameObject.GetComponent<PlayerAnimations>();

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
        if (Input.GetAxisRaw("Horizontal") == -1)
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
        TrampsSparks();
        rb.velocity = new Vector2(rb.velocity.x, baseBounceVelocity + extraBounceForce);
        baseBounceVelocity = baseBounceVelocity + extraBounceForce;
        hasBounced = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Bounce();
            if (transform.eulerAngles.z >= 90 || transform.eulerAngles.z <= -90)
            {
                anims.GroundBounceSide();
            }
            else if (transform.eulerAngles.z <= 90 || transform.eulerAngles.z >= -90)
            {
                {
                    anims.GroundBounceUpDown();
                }
            }
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            WallSparks();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            PlatformSparks();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            BreakableSparks();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            HazardSparks();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FinishLine"))
        {
            FinishLineSparks();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Coin"))
        {
            CoinSparks();
        }
    }


    void TrampsSparks()
    {
        trampolineSparks.Play();
    }
    void WallSparks()
    {
        wallSparks.Play();
    }
    void PlatformSparks()
    {
        platformSparks.Play();
    }
    void BreakableSparks()
    {
        breakableSparks.Play();
    }
    void HazardSparks()
    {
        hazardSparks.Play();
    }
    void CoinSparks()
    {
        coinSparks.Play();
    }
    void FinishLineSparks()
    {
        finishLineSparks.Play();
    }
}
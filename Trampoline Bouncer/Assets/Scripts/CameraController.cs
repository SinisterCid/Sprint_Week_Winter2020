using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Level Settings")]
    public GameObject playerTarget;
    public float levelTopLimit;
    public float levelBottomLimit;

    [Header("Camera Player Settings")]
    public float fromPlayerTopLimit;
    public float fromPlayerBottomLimit;

    private float FollowPercentage;
    private Rigidbody2D targetObject;

    // Start is called before the first frame update
    void Start()
    {
        targetObject = playerTarget.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //How much the player is moving

        FollowPercentage = targetObject.velocity.y;

        //CameraPos = FollowPercentage / FollowSensitivity;

        transform.position = new Vector3(0, Mathf.Clamp(targetObject.transform.position.y + Mathf.Clamp(FollowPercentage, fromPlayerBottomLimit, fromPlayerTopLimit), levelBottomLimit, levelTopLimit), -10);
    }
}
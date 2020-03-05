using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GroundBounceUpDown()
    {
        anim.SetTrigger("Ground_BounceUpDown");
    }
    public void GroundBounceSide()
    {
        anim.SetTrigger("Ground_BounceSide");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerAnimation2 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spr;
    PlayerMovement playerMovement;
    //jump not currently used, but here for future development.
    bool jumpPressed = false;
    Vector2 aimInput;
    int x;
    int y;
    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }
    private void Update()
    {
        anim.SetBool("IsGrounded", playerMovement.isGrounded);
    }
    void ProcessPlayerAimInput()
    {
        //rounds player inputs to ints
        x = Mathf.RoundToInt(aimInput.x);
        y = Mathf.RoundToInt(aimInput.y);

        //sets values for animator to use
        anim.SetInteger("PlayerXInput", x);
        anim.SetInteger("PlayerYInput", y);

        //updates the flip x of the player's sprite render
        if (x > 0)
            spr.flipX = false;
        else if (x < 0)
            spr.flipX = true;
    }
    //updaters for inputs
    public void OnJump(InputValue Value)
    {
        jumpPressed = Value.isPressed;
    }
    public void OnMoveAim(InputValue Value)
    {
        aimInput = Value.Get<Vector2>();
        ProcessPlayerAimInput();
    }
    public void OnFire(InputValue Value)
    {
        anim.SetBool("FirePressed", Value.isPressed);
    }
}

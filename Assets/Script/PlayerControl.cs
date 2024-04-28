using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;


    float walkSpeed = 1.5f;
    float speedLimiter = 0.6f;
    float inputHorizontal;
    float inputVertical;

    Animator animator;
    string currentState;
    const string Player_Walk_top = "Player_animation001";
    const string Player_Walk_bot = "Player_animation_top";
    const string Player_Walk_right = "Player_animation_right";
    const string Player_Walk_left = "Player_animation_left";
    const string Player_Walk_stand = "Player_animation_stand";


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();  
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal != 0 && inputVertical != 0)
            {
                inputHorizontal *= speedLimiter;
                inputVertical *= speedLimiter;
            }

            rb.velocity = new Vector2(inputHorizontal * walkSpeed, inputVertical * walkSpeed);

            if (inputHorizontal < 0)
            {
                ChangeAnimationState(Player_Walk_left);
            }
            else if (inputHorizontal > 0)
            {
                ChangeAnimationState(Player_Walk_right);
            }
            else if (inputVertical < 0)
            {
                ChangeAnimationState(Player_Walk_top);
            }
            else if (inputVertical > 0)
            {
                ChangeAnimationState(Player_Walk_bot);
            }

        }

        else
        {
            rb.velocity = new Vector2(0f, 0f);
            ChangeAnimationState(Player_Walk_stand);
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }

}

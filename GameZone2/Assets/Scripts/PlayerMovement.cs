using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;

	public float runSpeed = 40f;


	float horizontalMove = 0f;
	bool jump = false;

	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
		if (!controller.IsGrounded())
        {
            animator.SetBool("IsJumping", true);
        }
		else
		{
			OnLanding();
		}

        if (Input.GetButtonDown("Jump"))
		{
			if(controller.IsGrounded())
			{
				animator.SetBool("IsJumping", true);
				jump = true;
				Debug.Log("Grounded: " + controller.IsGrounded());
				if(controller.IsGrounded() == false)
				{
					while(controller.IsGrounded() == false) 
					{ 
						StopAnimation();
					}
				}
				ResumeAnimation();
			}
		}
	}
	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}

    public void StopAnimation()
    {
        animator.speed = 0f;
    }

    public void ResumeAnimation()
    {
		animator.speed = 1f;
    }

    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
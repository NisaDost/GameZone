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
			ResumeAnimation();
		}

        if (Input.GetButtonDown("Jump"))
		{
			if(controller.IsGrounded())
			{
				animator.SetBool("IsJumping", true);
				jump = true;
				Debug.Log("Grounded: " + controller.IsGrounded());
				if (!controller.IsGrounded())
				{
					StopAnimation();
				}
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
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
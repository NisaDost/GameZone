using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animator;
	public float runSpeed = 40f;
	public float originalRunSpeed = 40f;

    float horizontalMove = 0f;
	bool jump = false;

	// private float coyoteTime = 20f;		// isGrounded() false olduktan sonra jump yapılabilecek süre
	// private float coyoteTimeCounter;	//yardımcı eleman

    void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
		if (!controller.IsGrounded())
        {
			//coyoteTimeCounter -= Time.deltaTime;
            animator.SetBool("IsJumping", true);
        }
		else
		{
			//coyoteTimeCounter = coyoteTime;

			OnLanding();
			ResumeAnimation();
		}

        if (Input.GetButtonDown("Jump") && controller.IsGrounded() /*coyoteTimeCounter > 0*/)
		{
				animator.SetBool("IsJumping", true);
				jump = true;
				Debug.Log("Grounded: " + controller.IsGrounded());
				if (!controller.IsGrounded())
				{
					StopAnimation();
				}
				else{
					DisableShield();
				}
		}
		// else{
		// 	coyoteTimeCounter = 0;
		// }
	}

    public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
	}
	public void StopAnimation()
	{
		animator.speed = 0f;
	}

		public void DisableShield()
    {
        animator.SetBool("isShielding", false);
        ResetRunSpeed();
    }

    public void ResumeAnimation()
    {
        animator.speed = 1f;
    }
	public float GetRunSpeed()
	{
		return runSpeed;
	}
	public void SetRunSpeed(float newRunSpeed)
	{
		runSpeed = newRunSpeed;
	}
	public void ResetRunSpeed()
	{
		runSpeed = originalRunSpeed;
    }
	void FixedUpdate ()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}
}
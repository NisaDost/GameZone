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

 //   public void StopAnimationShield()
 //   {
	//	animator.SetBool("isShielding", true);
 //       StartCoroutine(WaitTime());
 //   }

	//public IEnumerator WaitTime()
	//{
	//	StopAnimation();
	//	yield return new WaitForSeconds(4f);
 //       ResumeAnimation();
	//}

    public void DisableShield()
    {
        animator.SetBool("isShielding", false);
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
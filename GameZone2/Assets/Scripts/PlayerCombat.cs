using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public CharacterController2D controller;

    public int health = 100;

    // public static float timeLeft = 0.5f;
    // public bool timerOn;

    void Update()
    {
        if(health == 0)
        {
            DeadMovement();
            animator.SetBool("isDead", true);
        }

        // if(Input.GetMouseButtonDown(0))
        // {   
        //     animator.SetBool("isAttack1", true);
        //     Debug.Log("attack1" + animator.GetBool("isAttack"));

        //     Timer();
        //     if(Input.GetMouseButtonDown(0) && timerOn && animator.GetBool("isAttack1")){
                
        //         animator.SetBool("isAttack2", true);
        //         Debug.Log("attack2" + animator.GetBool("isAttack2"));

        //         Timer();
        //         if(Input.GetMouseButtonDown(0) && timerOn && animator.GetBool("isAttack2"))
        //         {   
        //             animator.SetBool("isAttack3", true);
        //             Debug.Log("attack3" + animator.GetBool("isAttack3"));
              
        //         }
        //     }

        //     animator.SetBool("isAttack1", false);
        //     animator.SetBool("isAttack2", false);
        //     animator.SetBool("isAttack3", false);
        // }
    }

    public void DeadMovement()
    {
        movement.SetRunSpeed(0f);
        controller.SetJumpSpeed(0f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(health > 10)
            {
                health -= 25;
                Debug.Log("Health: " + health);
            }
            else if(health == 0) 
            {
                Debug.Log("Geberdin");
                health = 0;
            }
        }
    }
}

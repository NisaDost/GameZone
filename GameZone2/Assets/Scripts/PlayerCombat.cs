using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public CharacterController2D controller;

    public int health = 100;

    void Update()
    {
        if(health == 0)
        {
            DeadMovement();
            animator.SetBool("isDead", true);
        }

        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAttack1");
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public CharacterController2D controller;
    public Skeleton skeleton;
    public int health = 100;
    public int playerDamage = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if(health == 0)
        {
            DeadMovement();
            animator.SetBool("isDead", true);
        }

        if(Input.GetMouseButtonDown(0) && !controller.isDead())
        {
            animator.SetInteger("AttackNo", Random.Range(0, 3));
            animator.SetTrigger("Attack");
        }
    }

    public void DeadMovement()
    {
        controller.setDead(true);
        movement.SetRunSpeed(0f);
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Skeleton") && !animator.GetBool("isShielding"))
        {
            TakeDamage(skeleton.damage);
            Debug.Log("Health: " + health);
            if (health <= 0)
            {
                animator.SetBool("isDead", true);
                DeadMovement();
                health = 0;
            }
        }
    }
    public int TakeDamage(int damage)
    {
        health -= damage;
        return health;
    }
}

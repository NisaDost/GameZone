using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public CharacterController2D controller;
    public Skeleton skeleton;
    public MouseClickTimer clickdiff;
    
    public int health = 100;
    private int attackCounter = 0;
    private float timeDuration = 3f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
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
        
        #region attack combo
        //if (Input.GetMouseButtonDown(0) && controller.isDead() == false)
        //{
        //    if (attackCounter == 0 )
        //    {
        //        Attack1();
        //        attackCounter = 1;
        //    }
        //    else if (attackCounter == 1 && clickdiff.timeDifference <= timeDuration)
        //    {   
        //        Attack2();
        //        attackCounter = 2;
        //    }
        //    else if (attackCounter == 2 && clickdiff.timeDifference <= timeDuration)
        //    {
        //        Attack3();
        //        attackCounter = 0;
        //    }
        //    else{
        //        Attack1();
        //        attackCounter = 0;
        //    }
        //}
        #endregion
    }
    #region attack fonksiyon

    //private void Attack1()
    //{
    //    animator.SetBool("Attack1", true);
    //    animator.SetInteger("attackCount", attackCounter);
    //}

    //private void Attack2()
    //{
    //    animator.SetBool("Attack2", true);
    //    animator.SetInteger("attackCount", attackCounter);
    //}
 
    //private void Attack3()
    //{
    //    animator.SetBool("Attack3", true);
    //    animator.SetInteger("attackCount", attackCounter);
    //}
 
    //private void FinishAttack1()
    //{
    //    animator.SetBool("Attack1", false);
    //}
   
    //private void FinishAttack2()
    //{
    //    animator.SetBool("Attack2", false);
    //}
   
    //private void FinishAttack3()
    //{
    //    animator.SetBool("Attack3", false);
    //}
#endregion 



    public void DeadMovement()
    {
        controller.setDead(true);
        movement.SetRunSpeed(0f);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Skeleton") && !animator.GetBool("isShielding"))
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

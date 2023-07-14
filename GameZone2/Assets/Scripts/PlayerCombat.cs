using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public PlayerMovement movement;
    public CharacterController2D controller;

    public int health = 100;

    public float timeLeft = 0.5f;
    
    void Update()
    {
        if(health == 0)
        {
            DeadMovement();
            animator.SetBool("isDead", true);
        }

        //if (Input.GetMouseButtonDown(0) && !isAttacking)
        //{
        //    StartCoroutine(AttackCombo());
        //}
        #region deneme tahtasi
        //if (Input.GetMouseButtonDown(0))
        //{
        //    animator.Play("Attack1MC");
        //    StartCoroutine(Timer());

        //    if (Input.GetMouseButtonDown(0) && timeLeft > 0)
        //    {
        //        ResetTimer();
        //        animator.Play("Attack2MC");
        //        StartCoroutine(Timer());

        //        if (Input.GetMouseButtonDown(0) && timeLeft > 0)
        //        {
        //            ResetTimer();
        //            animator.Play("Attack3MC");
        //        }
        //    }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Attack1MC");
            Timer();

        }
        else if (Input.GetMouseButtonDown(0) && timeLeft > 0)
        {
            ResetTimer();
            animator.Play("Attack2MC");
            Timer();

        }
        else if (Input.GetMouseButtonDown(0) && timeLeft > 0)
        {
            ResetTimer();
            animator.Play("Attack3MC");
        }
        #endregion
    }

    #region deneme tahtasi
    //private IEnumerator AttackCombo()
    //{
    //    isAttacking = true;

    //    animator.Play("Attack1MC");
    //    yield return StartCoroutine(WaitForTimer());

    //    if (Input.GetMouseButtonDown(0) && timeLeft > 0)
    //    {
    //        ResetTimer();
    //        animator.Play("Attack2MC");
    //        Debug.Log("Sen ne ayaksin");
    //        yield return StartCoroutine(WaitForTimer());
    //    }

    //    if (Input.GetMouseButtonDown(0) && timeLeft > 0)
    //    {
    //        ResetTimer();
    //        animator.Play("Attack3MC");
    //        Debug.Log("Sen ne kafasin");
    //        yield return StartCoroutine(WaitForTimer());
    //    }

    //    isAttacking = false;
    //}
    #endregion

    private IEnumerator Timer()
    {
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        ResetTimer();
    }
    public void ResetTimer()
    {
        timeLeft = 0.5f;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerCombat playerCombat;
    public Animator animator;
    public Rigidbody2D rb;
    public Transform player;
   
    public int mobHealth = 20;
    public float attackRange = 5f;
    private bool facingRight = true;
    public bool isDead = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (PlayerDistanceX() <= attackRange && PlayerDistanceY() <= 1f)
        {
            if (player.position.x < transform.position.x && facingRight)
            {
                Flip();
            }
            else if (player.position.x > transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }
    public float PlayerDistanceX()
    {
        float playerDistanceX = Mathf.Abs(player.position.x - transform.position.x);
        return playerDistanceX;
    }
    public float PlayerDistanceY()
    {
        float playerDistanceY = Mathf.Abs(player.position.y - transform.position.y);
        return playerDistanceY;
    }
    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}

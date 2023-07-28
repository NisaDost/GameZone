using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerCombat playerCombat;

    public Transform pointLeft;
    public Transform pointRight;
    public Transform player;
    private Transform currentWaypoint;
    private Animator animator;
    private Rigidbody2D rb;

    public float speed = 2f;
    public float idleTime = 2f;
    private float attackRange = 0.8f;
    public float detectRangeX = 6f;
    public float detectRangeY = 2f;
    
    public int damage = 8;
    public int mobHealth = 30;
    
    private bool isMovingForward;
    private bool isWaiting = false;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentWaypoint = pointRight;
        animator.SetBool("isRunning", true);
        Debug.Log(transform.position.y + detectRangeY);
    }

    private void Update()
    {
        if (player.position.x > transform.position.x - detectRangeX &&
            player.position.x < transform.position.x + detectRangeX && 
            player.position.y < transform.position.y + detectRangeY &&
            controller.isDead() == false && !isDead)
        {
            Chase();
        }

        else if (!isWaiting && !isDead)
        {
            Vector3 direction = (currentWaypoint.position - transform.position).normalized;

            float moveAmount = speed * Time.deltaTime;

            transform.Translate(direction * moveAmount);

            if (direction.x > 0 && transform.localScale.x < 0 || direction.x < 0 && transform.localScale.x > 0)
                {
                    Flip();
                }

            if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
            {
                StartCoroutine(IdleAndSwitchWaypoint());
            }
        }
    }
    void Chase()
    {
        animator.SetBool("isRunning", true);

        float playerDistanceX = Mathf.Abs(player.position.x - transform.position.x);
        float moveAmount = speed * Time.deltaTime;

        if (playerDistanceX > attackRange)
        {
            Vector2 targetPosition = new Vector2(player.position.x, rb.position.y);
            Vector2 currentPosition = rb.position;
            Vector2 direction = (targetPosition - currentPosition).normalized;

            rb.MovePosition(currentPosition + (direction * moveAmount*2));
        }
        else if (playerDistanceX <= attackRange)
        {

            if (player.position.y > transform.position.y + 0.5f)
            {
                isWaiting = true;
            }
            animator.SetTrigger("Attack");
        }

        if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Weapon"))
        {
            mobHealth -= playerCombat.playerDamage;
            Debug.Log("Mob Health: " + mobHealth);
            if (mobHealth <= 0)
            {
                mobHealth = 0;
                animator.SetBool("isDead", true);
                isDead = true;
            }
        }
    }
    IEnumerator IdleAndSwitchWaypoint()
    {
        isWaiting = true;
        animator.SetBool("isRunning", false);
        yield return new WaitForSeconds(idleTime);
        animator.SetBool("isRunning", true);
        SwitchWaypoint();
        isWaiting = false;
    }

   void SwitchWaypoint()
    {
        if (isMovingForward)
        {            
            currentWaypoint = pointLeft;
        }
        else
        {  
            currentWaypoint = pointRight;
        }
        isMovingForward = !isMovingForward;        
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        if (pointLeft != null && pointRight != null)
        {
            Gizmos.DrawWireSphere(pointLeft.position, 0.5f);
            Gizmos.DrawWireSphere(pointRight.position, 0.5f);
            Gizmos.DrawLine(pointLeft.position, pointRight.position);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public CharacterController2D controller;

    public Transform pointLeft;
    public Transform pointRight;
    public Transform player;
    private Transform currentWaypoint;
    private Animator animator;

    public float speed = 2f;
    public float idleTime = 2f;
    private float attackRange = 0.8f;
    public float detectRange = 6f;
    public int damage = 8;
    
    private bool isMovingForward;
    private bool isWaiting = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentWaypoint = pointRight;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        if (player.position.x > transform.position.x - detectRange && player.position.x < transform.position.x + detectRange && controller.isDead() == false)
        {
            Chase();
        }

        else if (!isWaiting)
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

        if (playerDistanceX > attackRange )
        {
            // Move towards the player only if the player is not within attack range
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0f, 0f).normalized;
            transform.Translate(direction * moveAmount);
        }
        else if (playerDistanceX <= attackRange)
        {
            // Player is within attack range, stop moving and trigger the attack animation
            animator.SetTrigger("Attack");
        }

        // Make sure to flip the skeleton if needed
        if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
        {
            Flip();
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

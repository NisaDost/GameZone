using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public Transform pointLeft;
    public Transform pointRight;
    public Transform player;
    private Transform currentWaypoint;
    private Animator animator;

    public float speed = 1.8f;
    public float idleTime = 2f;
    private float attackRange = 1f;
    private float detectRange = 4f;
    
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
        if (player.position.x > transform.position.x - detectRange && player.position.x < transform.position.x + detectRange)
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

        Vector3 direction = new Vector3(player.position.x - transform.position.x, 0f, 0f).normalized;
        float moveAmount = speed * Time.deltaTime;

        float playerDistance = Vector3.Distance(transform.position, player.position);

        if (playerDistance > attackRange)
        {
            transform.Translate(direction * moveAmount);
        }
        else if (playerDistance < attackRange) // Change to playerDistance < attackRange
        {
            // Stop moving and play the attack animation
            transform.position = Vector3.MoveTowards(transform.position, player.position, 0.2f); // Move the skeleton away from the player to avoid overlapping

            animator.SetTrigger("Attack");
        }

        if (direction.x >= 0 && transform.localScale.x <= 0 || direction.x <= 0 && transform.localScale.x >= 0)
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
    //animasyon kaydırması denemesi
    void AnimationSlide(){
        //Vector3 localPosition = transform.localPosition;
        //localPosition.x += 0.4f;
        //transform.localPosition = localPosition;
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

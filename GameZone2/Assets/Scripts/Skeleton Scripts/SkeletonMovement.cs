using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public Transform pointLeft;
    public Transform pointRight;
    private Transform currentWaypoint;
    
    private Animator animator;
    public float speed = 1.8f;
    public float idleTime = 2f;
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
        if (!isWaiting)
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
   
    IEnumerator IdleAndSwitchWaypoint()
    {
        isWaiting = true;

        animator.SetBool("isRunning", false); // Play the idle animation

        yield return new WaitForSeconds(idleTime); // Wait for the specified idle time

        animator.SetBool("isRunning", true); // Resume the running animation

        SwitchWaypoint(); // Switch the waypoint after waiting

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

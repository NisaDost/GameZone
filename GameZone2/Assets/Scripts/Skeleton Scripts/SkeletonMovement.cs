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

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentWaypoint = pointRight;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        Vector3 direction = (currentWaypoint.position - transform.position).normalized;

        float moveAmount = speed * Time.deltaTime;

        transform.Translate(direction * moveAmount);

        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            
            //Flip();
            //StartCoroutine(IdleTime(idleTime));
            SwitchWaypoint();
        }
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

    private IEnumerator IdleTime(float time)
    {
        //animator.Play("SkeletonIdle");
        yield return new WaitForSeconds(time);
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

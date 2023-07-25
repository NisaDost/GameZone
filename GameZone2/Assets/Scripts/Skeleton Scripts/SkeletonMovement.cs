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
    private float attackRange = 2f;
    
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
        if (player.position.x > pointLeft.position.x && player.position.x < pointRight.position.x)
        {
            ChaseAndAttack();
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
    void ChaseAndAttack()
    {
        animator.SetBool("isRunning", true);

        Vector3 direction = (player.position - transform.position).normalized;

        float moveAmount = speed * Time.deltaTime;

        transform.Translate(direction * moveAmount);

        if (direction.x > 0 && transform.localScale.x < 0 || direction.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance <= attackRange)
        {
            animator.SetTrigger("Attack");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public GameObject pointLeft;
    public GameObject pointRight;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    public float speed = 2f;
    public float idleTime = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointRight.transform;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointRight.transform)
        {
            animator.SetBool("isRunning", false);
            StartCoroutine(IdleTime(idleTime));
            rb.velocity = new Vector2(-speed, 0);
        }
        else if(currentPoint == pointLeft.transform)
        {
            animator.SetBool("isRunning", false);
            StartCoroutine(IdleTime(idleTime));
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 2f && currentPoint == pointRight.transform)
        {
            Flip();
            currentPoint = pointLeft.transform;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 2f && currentPoint == pointLeft.transform)
        {
            Flip();
            currentPoint = pointRight.transform;
        }
    }

    private IEnumerator IdleTime(float idleTime)
    {
        new WaitForSeconds(idleTime);
        yield return null;
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointLeft.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointRight.transform.position, 0.5f);
        Gizmos.DrawLine(pointLeft.transform.position, pointRight.transform.position);
    }
}
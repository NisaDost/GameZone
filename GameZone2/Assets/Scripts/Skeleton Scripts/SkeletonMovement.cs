using System.Collections;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public Transform pointLeft;
    public Transform pointRight;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    public float speed = 2f;
    public float idleTime = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = pointRight;
        animator.SetBool("isRunning", true);
    }

    private void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointRight && point.x < 0)
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
            StartCoroutine(IdleTime(idleTime)); // Add this line to trigger the idle behavior.
        }
        else if (currentPoint == pointLeft && point.x > 0)
        {
            animator.SetBool("isRunning", false);
            rb.velocity = Vector2.zero;
            StartCoroutine(IdleTime(idleTime)); // Add this line to trigger the idle behavior.
        }
        else
        {
            animator.SetBool("isRunning", true);
            rb.velocity = new Vector2((currentPoint == pointRight ? -speed : speed), 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            currentPoint = (currentPoint == pointRight) ? pointLeft : pointRight;
            Flip();
        }
    }

    private IEnumerator IdleTime(float time)
    {
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

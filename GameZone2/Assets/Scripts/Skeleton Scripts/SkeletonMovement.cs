using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float speed = 2f;
    public float pacingRange = 5f;
    public float idleDuration = 2f;

    private Vector3 leftBoundary;
    private Vector3 rightBoundary;
    private Vector3 targetPosition;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component.
    private bool isWalking = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component.

        leftBoundary = transform.position - Vector3.right * (pacingRange / 2f);
        rightBoundary = transform.position + Vector3.right * (pacingRange / 2f);

        targetPosition = rightBoundary;
    }

    private void Update()
    {
        if (isWalking)
        {
            float direction = Mathf.Sign(targetPosition.x - transform.position.x);
            spriteRenderer.flipX = (direction < 0f); // Flip the sprite based on the movement direction.

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                StartCoroutine(IdleAndTurn());
            }
        }
    }

    private IEnumerator IdleAndTurn()
    {
        isWalking = false;

        if (animator != null)
        {
            animator.SetBool("IsWalking", false);
        }

        yield return new WaitForSeconds(idleDuration);

        targetPosition = (targetPosition == leftBoundary) ? rightBoundary : leftBoundary;
        isWalking = true;

        if (animator != null)
        {
            animator.SetBool("IsWalking", true);
        }
    }
}

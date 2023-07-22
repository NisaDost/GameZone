using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    public float pacingSpeed = 2f;
    public float pacingRange = 5f;
    public float idleDuration = 2f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 2f;

    private Vector2 leftBoundary;
    private Vector2 rightBoundary;
    private Vector2 targetPosition; // Add targetPosition for 2D movement.
    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isWalking = true;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        leftBoundary = (Vector2)transform.position - Vector2.right * (pacingRange / 2f); // Convert to Vector2.
        rightBoundary = (Vector2)transform.position + Vector2.right * (pacingRange / 2f); // Convert to Vector2.
        targetPosition = rightBoundary; // Set initial target position.
    }

    private void Update()
    {
        if (!isAttacking)
        {
            DetectPlayer();

            if (isWalking)
            {
                MovePacing();
            }
            else if (player != null)
            {
                MoveTowardsPlayer();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(Attack());
        }
        else
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            spriteRenderer.flipX = (direction < 0f);

            float targetX = player.position.x;
            
            Vector2 targetPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, pacingSpeed * Time.deltaTime);
        }
    }

    private void MovePacing()
    {
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);
        spriteRenderer.flipX = (direction < 0f);

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, pacingSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(IdleAndTurn());
        }
    }

    private void DetectPlayer()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            isWalking = (distanceToPlayer > detectionRange);
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

    private void AnimationFix(){
        if(transform.position.x < 0){
            transfrom.position.x -= 0.5;
        }
        else{
            transfrom.position.x += 0.5;
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Debug.Log("Mob is attacking!");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
}


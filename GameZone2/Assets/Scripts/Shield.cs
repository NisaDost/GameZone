using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public PlayerMovement movement;
    public Animator animator;
    public Transform objectToFollow;
    [SerializeField] float shieldTime = 2f;

    void Update()
    {
        Vector2 targetPosition = new Vector2(objectToFollow.position.x, objectToFollow.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * movement.getRunSpeed());
        
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isShielding", true);
            StartCoroutine(movement.ShowMustGoOn(shieldTime));
            //StopAllCoroutines();
            Debug.Log("SHIELD!");
        }
        animator.SetBool("isShielding", false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public PlayerMovement movement;
    public Transform objectToFollow;
    
    void Update()
    {
        Vector2 targetPosition = new Vector2(objectToFollow.position.x, objectToFollow.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * movement.GetRunSpeed());

        if (Input.GetMouseButtonDown(1))
        {
            movement.animator.SetBool("isShielding", true);
            movement.SetRunSpeed(movement.GetRunSpeed()/5);
            if (movement.animator.GetBool("isShielding") == false)
            {
                movement.ResetRunSpeed();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public PlayerMovement movement;
    public CharacterController2D controller;
    public Transform objectToFollow;
    [SerializeField ] float cooldownTime = 4f;
    public bool inCooldown = false;
    void Update()
    {
        Vector2 targetPosition = new Vector2(objectToFollow.position.x, objectToFollow.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * movement.GetRunSpeed());

        if (Input.GetMouseButtonDown(1) && controller.IsGrounded() && !inCooldown) 
        {
            if(Input.GetMouseButton(1))
            {
                movement.animator.SetBool("isShielding", true);
            }

            movement.SetRunSpeed(movement.GetRunSpeed()/3f);
        }
        else if (Input.GetMouseButtonUp(1) && controller.IsGrounded())
        {
            StartCoroutine(Cooldown(cooldownTime));
            movement.DisableShield();
        }
    }

    public IEnumerator Cooldown(float time)
    {
        inCooldown = true;
        yield return new WaitForSeconds(time);
        inCooldown = false;
    }
}

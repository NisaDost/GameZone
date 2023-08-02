using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEntryPoint : MonoBehaviour
{
    public Mushroom mushroom;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Animator animator;
 
    void Update()
    {
        if (mushroom.PlayerDistanceX() <= mushroom.attackRange && mushroom.PlayerDistanceY() <= 1f)
        {
            animator.SetTrigger("Attack");
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}

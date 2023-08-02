using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public PlayerCombat combat;
    public Mushroom mushroom;
    public GameObject impactEffect;

    public int damage = 4;
    public float speed = 6f;
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerMovement player = hitInfo.GetComponent<PlayerMovement>();
        
        if (hitInfo.gameObject.tag == "Player" || hitInfo.gameObject.CompareTag("Ground"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }   
}

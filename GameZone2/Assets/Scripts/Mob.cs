using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    private int mob1Health = 100 ;


    void Update()
    {
        
    }
    public void OnColliderEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Weapon"))
        {
            mob1Health -= 10 ;  
            Debug.Log("mob1Health:  " + mob1Health);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //3 shoots will kill the enemy
    public int laif = 3;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            laif -= 1;

            if(laif == 0 )
            {
                Destroy(this.gameObject);

            }
            Debug.Log(this.name + " Het");
        }
    }
}

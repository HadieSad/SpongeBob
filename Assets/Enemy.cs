using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 4;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            life -= 1;

            if(life == 0 )
            {
                Destroy(this.gameObject);

            }

            Debug.Log(this.name + " Het");
        }
    }
}

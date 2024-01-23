using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed = 100f;
    void Start()
    {
        
    }

    // called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * BulletSpeed * Time.deltaTime);
    }
}

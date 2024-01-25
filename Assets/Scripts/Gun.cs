using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int Mag_Bullet = 15;

    public int full_Bullets = 75;

    public int Mag_Reload;


    public Sprite Gun_Logo;



    void Start()
    {
        Mag_Reload = Mag_Bullet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

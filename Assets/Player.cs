using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
   
    public GameObject Hands;
    [Header("Player movements:")]
//speed of player walking, looking, jumping and runing. *It may change
    public float WalkS = 7;
    public float LookS = 4;
    public float JumpS = 10;
    public float RunS = 1;
    public bool Walk, Run, Jump;
    private Animator Player_Anim;
    public GameObject Bullet;
    public GameObject Bullet_Point;

    void Start()
    {
        Player_Anim = Hands. GetComponent<Animator>();
    }

    
 
//handle input actions
//Shift key,toggling between walking and running.
//Handle shooting,Left mouse button trigger "Shoot" animation.
//Handle jumping,Space key to jump with force, release the key to stop.
    public void Get_Back_Call_Keys()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run = true;
            Walk = false;
            WalkS *= RunS;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Run = false;
            WalkS /= RunS;
        }

        if(Input.GetMouseButtonDown(0))
        {
            Player_Anim.SetTrigger("Shoot");
            GameObject B = Instantiate(Bullet, Bullet_Point.transform.position, Bullet_Point.transform.rotation);
            Destroy(B, 0.35f);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * JumpS  , ForceMode.Impulse);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }


    }

   
   
    public void Mouse_Looke()
    {

        float MouseX = Input.GetAxisRaw("Mouse X");
        float MouseY = Input.GetAxisRaw("Mouse Y");
        Vector3 Rotate = new Vector3(0, MouseX, 0) * LookS;
        Vector3 Rotate_camera = new Vector3(-MouseY, 0, 0) * LookS;
        Hands.transform.Rotate(Rotate_camera);
        transform.Rotate(Rotate);
        Hands.transform.eulerAngles = new Vector3(Hands.transform.eulerAngles.x, Hands.transform.eulerAngles.y, 0);
    }


    void Update()
    {
        Mouse_Looke();
        Get_Back_Call_Keys();
//control player movement and animation:
//manages mouse look and key input.
//calculate player direction from input axes.
//adjust animation states (Walking and Running).
//move the player in the calculated direction with modified speed.



        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 Player_Derection = new Vector3(Horizontal, 0, Vertical);
        Walk = true;
        if (Player_Derection != Vector3.zero)
        {
            if(Walk == true)
            {
                Player_Anim.SetBool("Walk", true);
            }
            else
            {
                Player_Anim.SetBool("Walk", false);
            }

            if (Run == true)
            {
                Player_Anim.SetBool("Walk", false);

                Player_Anim.SetBool("Run", true);

            }
            else
            {
                Player_Anim.SetBool("Run", false);
            }


        }
        else
        {
            Player_Anim.SetBool("Walk", false);
            Player_Anim.SetBool("Run", false);
        }

        
        transform.Translate(Player_Derection * WalkS * Time.deltaTime);

    }
}

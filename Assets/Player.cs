using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   
    public GameObject[] Hands;

    [Header("Player Movment : \n")]

    public float Walk_Speed = 10;

    public float Run_Speed = 2;

    public float Jump_Speed = 10;

    public float Look_Speed = 3;

    /// <summary>
    /// ///
    /// </summary>
    
    [Header("Player Animation State : \n")]

    public bool Walk, Run, Jump;

    private Animator Player_Anim;


    /// <summary>
    
    /// </summary>
    
    [Header("Wepens : \n")]

    public List<GameObject> Wepens;

    public int Selected_Wepen;

    /// <summary>
    /// 
    /// </summary>

    [Header("Bullet : \n" )]

    public GameObject Bullet;

    public GameObject[] Bullet_Point;

    [Header("Shooting Setting : \n")]

    public bool Is_Reloading = false;

    public bool No_Ammo = false;

    [Header("Ui Items : \n")]

    public Text Ammo_Text;

    public Image Gun_Logo;

    public Image laif_Line;




    [Header("Player Health : \n")]

    public float Player_Laif = 5;

    [Header("Sounds : \n")]

    public AudioSource Shoot_Sound;



    private float Player_Full_Laif;
    void Start()
    {
        Player_Full_Laif = 1 / Player_Laif;

        Time.timeScale = 1;

        laif_Line.fillAmount = 1;

         Player_Anim = Hands[Selected_Wepen].GetComponent<Animator>();
        Set_Gun_Logo();
        Set_Ammo();


    }

    public void Get_Back_Call_Keys()
    {

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Run = true;
            Walk = false;
            Walk_Speed *= Run_Speed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Run = false;
            Walk_Speed /= Run_Speed;
        }

        if(Input.GetMouseButtonDown(0))
        {

            if (No_Ammo == false)
            {

               

                Player_Anim.SetTrigger("Shoot");

                Shoot_Sound.Play();

                GameObject B = Instantiate(Bullet, Bullet_Point[Selected_Wepen].transform.position, Bullet_Point[Selected_Wepen].transform.rotation);

                Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet -= 1;

                Set_Ammo();

                if (Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet == 0)
                {
                    No_Ammo = true;
                    Reload();
                }
              

                Destroy(B, 0.35f);
            }

        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * Jump_Speed  , ForceMode.Impulse);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            No_Ammo = true ;
            Reload();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Cheng_Wepen();
        }

    }   
   
    public void Mouse_Looke()
    {

        float MouseX = Input.GetAxisRaw("Mouse X");
        float MouseY = Input.GetAxisRaw("Mouse Y");



        Vector3 Rotate = new Vector3(0, MouseX, 0) * Look_Speed;

        Vector3 Rotate_camera = new Vector3(-MouseY, 0, 0) * Look_Speed;

        Hands[Selected_Wepen].transform.Rotate(Rotate_camera);


        transform.Rotate(Rotate);

        Hands[Selected_Wepen].transform.eulerAngles = new Vector3(Hands[Selected_Wepen].transform.eulerAngles.x, Hands[Selected_Wepen].transform.eulerAngles.y, 0);


    }

    public void Cheng_Wepen()
    {
        if (Selected_Wepen == 0)
        {
            Selected_Wepen = 1;

            Hands[0].SetActive(false);

            Hands[1].SetActive(true);


        }
        else
        {
            Selected_Wepen = 0;

            Hands[1].SetActive(false);

            Hands[0].SetActive(true);
        }


        Set_Gun_Logo();
    }

    void Update()
    {

       

        Mouse_Looke();
        Get_Back_Call_Keys();


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

        
        transform.Translate(Player_Derection * Walk_Speed * Time.deltaTime);

    }


   
    public void Reload()
    {



        Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets += Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet;

        Player_Anim.SetTrigger("Reload");

        if (Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets == Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload)
        {
            Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets = 0;

            Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet = Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload;

            
            Set_Ammo();
            Thread.Sleep(1000);

            No_Ammo = false;
        }

        if (Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets > Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload)
        {

            Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets -= Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload;

            Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet = Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload;
  Set_Ammo();
            Thread.Sleep(1000);

            No_Ammo = false;
          
        }

        if (Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets < Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Reload)
        {
            Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet += Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets;

            Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets = 0;
      Set_Ammo();
            Thread.Sleep(1000);

            No_Ammo = false;
      
        }

       




    }


    private void OnMouseDown()
    {
        
     
    }

    public void Set_Ammo()
    {
        Ammo_Text.text = Wepens[Selected_Wepen].GetComponent<Gun>().full_Bullets.ToString() + " / " + Wepens[Selected_Wepen].GetComponent<Gun>().Mag_Bullet.ToString();
    }

    public void Set_Gun_Logo()
    {
      
            Gun_Logo.sprite = Wepens[Selected_Wepen].GetComponent<Gun>().Gun_Logo;
     

    }

    public void Set_Laif()
    {
        Player_Laif -= 1;

        laif_Line.fillAmount -= Player_Full_Laif;


        if (Player_Laif == 0)
        {
            Time.timeScale = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Enemy"))
        {
            Set_Laif();
        }
    }


}


// Ã«„Å
// —·Êœ
// »—œ«‘ ‰ „Â„« 
//›Ê Ê‰
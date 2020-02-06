using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour {

    static public float speed=10;
    public float jumpForce;
    public float maxJumpforce=1700;
    bool isGround = true;
    Animator anim;
    public Animator menu;
    AudioSource audiosource;
    public AudioClip jump, speedB,equalizer;
    public float jumpVoice,buffVoice,equalizerVoice;
    public float maxSpeed=14;
    static public bool death = false;
    float oldspeed;
    public GameObject partical;
    public GameObject pointPartical;
    public GameObject mainCamera;


    Rigidbody2D rigid2D;

    void Awake()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;


    }
    
    // Use this for initialization
	void Start ()
    {
        audiosource = GetComponent<AudioSource>();
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        InvokeRepeating("Speedster", 1, 1f);


    }
	
	
    
    // Update is called once per frame
	void Update ()
    {
        rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
            jumpForce = maxJumpforce;
        }
        //Debug.Log(speed);
        //Debug.Log(jumpForce);
        
        

        if (isGround == true)
        {
            if (Input.GetMouseButtonDown(0))
            {


                if (Time.timeScale == 0)
                {

                    Time.timeScale = 1;
                    AudioListener.pause = false;
                }

                else if (Time.timeScale==1)
                {
                    rigid2D.AddForce(Vector2.up * jumpForce);
                    audiosource.PlayOneShot(jump, jumpVoice);
                    //rigid2D.velocity = new Vector2(rigid2D.velocity.x,jumpForce);
                    isGround = false;
                    anim.Play("CubeRotate");
                    anim.SetBool("turn", true);
                    

                    


                }

                
                
                





            }

            
        }

        






    }

    



    public void OnCollisionEnter2D(Collision2D other)
    {
        isGround = true;

        
        
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "speedBuff")
        {
            Destroy(other.gameObject);
            audiosource.PlayOneShot(speedB, buffVoice);
            speed += 0.2f;
            CameraScript.speed += 0.2f;
            UIControl.score += 2;
            Instantiate(pointPartical, this.gameObject.transform.position,transform.rotation);
        }

        if (other.gameObject.tag == "death")
        {
            mainCamera.GetComponent<CameraScript>().increaseDeathNumber();
            Debug.Log("Öldüm");
            speed = 10f;
            CameraScript.speed = 10f;
            UIControl.score = 0;
            Spawner.isFirst = true;
            Destroy(this.gameObject);
            menu.SetBool("end", true);
            UIControl.end = true;
            death = true;

            mainCamera.GetComponent<CameraScript>().showAds();

            print(mainCamera.GetComponent<CameraScript>().getDeathNumber());
        }

        if (other.gameObject.tag == "equalizer")
        {
            audiosource.PlayOneShot(equalizer, equalizerVoice);
            Destroy(other.gameObject);
            StartCoroutine(equal());
        }

        

    }

    public void Speedster()
    {
        //speed += 0.40f;
        //CameraScript.speed += 0.40f;
        //jumpForce += 7;
    }

    IEnumerator equal()
    {
        partical.SetActive(true);
        speed = speed+ 1f;
        yield return new WaitForSeconds(1.2f);
        speed = CameraScript.speed;
        partical.SetActive(false);
    }
    
    
}

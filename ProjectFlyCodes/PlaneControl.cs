using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaneControl : MonoBehaviour
{

    
    //public float rotateSpeed;
    Rigidbody2D rb;
    bool push,slowing,restart;
    public float angular, flyPower, rotatePowerUp, rotatePowerDown,Nspeed;
    public float rotateUpLimit, rotateDownLimit, maxSpeed;
    float rotatePowerUp_P, rotatePowerDown_P, flyPower_P, speed;
    public bool landing=true;
    [Header("Sounds")]
    public AudioClip crash;
    [Range(0.0f,1.0f)]
    public float crashVolume;
    
    AudioSource audioSource;
    [Range(0.0f, 1.0f)]
    public float maxEngineVolume;
    [Range(0.0f, 10.0f)]
    public float smoothUpSoundRate,smoothDownSoundRate;

    public int restartPhase;
    public Image fadePanel;
    public float fadeRate = 4f;
    public bool fade = true;

    Animator anim;
    public GameObject explosionParticle;
    public GameObject restartText;

    // Start is called before the first frame update
    void Start()
    {
        

        rb = GetComponent<Rigidbody2D>();
        resetVariable();
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        //audioSource.volume =0f;
        restartPhase = 0;
        if (fade)
        {
            fadePanel = GameObject.Find("FadePanel").GetComponent<Image>();
            fadePanel.color = Color.black;
            fadePanel.raycastTarget = true;
        }
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        


        Debug.Log(rb.velocity.y);
        //Debug.Log(speed);
        if (Input.GetMouseButtonUp(0))
        {
            //rotateSpeed = 0f;

            push = true;
            slowing = true;
            resetVariable();
            
        }


        if (FindObjectOfType<UIManager>().inMenu == true)
        {

            audioSource.Stop();
            //audioSource.enabled = false;
            this.GetComponent<PlaneControl>().enabled = false;
        }

        if (FindObjectOfType<UIManager>().inMenu == false)
        {
            //audioSource.enabled = true;
        }

        

        if ((Input.GetMouseButton(0))&&(restart==false))
        {
            slowing = false;
            goForward();
            RotateUp();
            //flyUp();
            

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                audioSource.loop = true;
                

            }

            if (audioSource.volume <= maxEngineVolume)
            {

                audioSource.volume += smoothUpSoundRate * Time.deltaTime;
            }
            anim.speed = 1f;
        }
        else
        {
            anim.speed = 0f;
            if (audioSource.volume >= 0f && restart == false)
            {
                audioSource.volume -= smoothDownSoundRate * Time.deltaTime;
            }

            if (audioSource.volume <= 0f)
            {
                audioSource.Stop();
            }
            if (push == true)
            {
                RotateDown();
                

            }
        }
        if (restart == true && restartPhase == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (GameObject.Find("FadePanel")==null)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                //Time.timeScale = 1;
                //Destroy(rb);
                restartPhase++;
                fadePanel.raycastTarget = true;
            }
        }

        
    }

    void FixedUpdate()
    {
        if (restartPhase == 0 && fadePanel.color != Color.clear)
        {
            fadePanel.color -= Color.black * Time.timeScale * fadeRate;
            if (fadePanel.color == Color.clear)
            {
                fadePanel.raycastTarget = false;
            }
        }

        if (restart == true)
        {
            /*if (Input.GetMouseButtonDown(0) && restartPhase == 0)
            {
                restartPhase+=1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }*/
            if(restartPhase==1)
            {
                fadePanel.color += Color.black * Time.timeScale * fadeRate;
                if (fadePanel.color == Color.black)
                {
                    //restartPhase++;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
            if (restartPhase==2)
            {
                fadePanel.color -= Color.black * Time.timeScale * fadeRate;
                if (fadePanel.color == Color.clear)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }



    void RotateUp()
    {
        /*
        if (transform.eulerAngles.z < 90)
        {
            transform.Rotate(Vector3.forward, 20 * Time.deltaTime * angular);
            Debug.Log("1");
        }*/

        if (angular < rotateUpLimit)
        {
            if (angular < 0)
            {
                //Default=200
                
                rotatePowerUp_P += 1000 * Time.deltaTime;
            }
            else  
            {
                rotatePowerUp_P += 100 * Time.deltaTime;
            }
            angular += rotatePowerUp_P * Time.deltaTime;
        }
        
        rb.angularVelocity = angular;

    }

    void RotateDown()
    {
        /*
        if (transform.eulerAngles.z < 290 && transform.eulerAngles.z > 90)
        {

            transform.Rotate(Vector3.forward, 20 * Time.deltaTime * angular);
        }*/
        if (transform.eulerAngles.z > 280 && angular > 20)
        {
            angular = 10;

        }
        else if (transform.eulerAngles.z < 280)
        {
            
            if (angular > -rotateDownLimit)
            {
                //normalVersion of powerDown=100
                rotatePowerDown_P += 10 * Time.deltaTime;
                // the parenthesis below should return either -1 or 1.
                angular += (transform.eulerAngles.z < 120 ? -1 : 1) * rotatePowerDown_P * Time.deltaTime;
            }
            rb.angularVelocity = angular;
        }
        
    }

    void goForward()
    {
        /*rb.AddRelativeForce(Vector2.right * Speed);
        rotateSpeed += 20f * Time.deltaTime;*/

        speed += 100 * Time.deltaTime;
        rb.AddRelativeForce(Vector2.right * speed);

        if (rb.velocity.magnitude > maxSpeed)
        {

            rb.velocity = rb.velocity.normalized * maxSpeed;



        }
        
    }

    void speedDown()
    {
        speed -= 100*Time.deltaTime;
        if(speed < 0)
        {
            speed = 0f;
        }
    }

    void flyUp()
    {
        rb.AddForce(new Vector2(0, flyPower_P));
        if (flyPower_P > 0)
        {
            flyPower_P -= 10 * Time.deltaTime;
        }
        else
        {
            flyPower_P = 0;
        }
    }

    void resetVariable()
    {

        
        rotatePowerUp_P = rotatePowerUp;
        rotatePowerDown_P = rotatePowerDown;
        flyPower_P = flyPower;
        speed = Nspeed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "ground")
        {
            //Time.timeScale = 0;
            if (restartText != null)
            {
                restartText.SetActive(true);
            }
            anim.Play("plane_dead");
            Instantiate(explosionParticle, this.transform.position,this.transform.rotation);
            rb.bodyType = RigidbodyType2D.Static;
            restart = true;
            landing = false;
            Debug.Log("Crash");
            FindObjectOfType<SoundManager>().playSounds(crash,crashVolume);
            audioSource.volume = 0f;
            
        }

        
    }
}

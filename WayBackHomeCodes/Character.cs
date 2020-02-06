using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    static public float moveInput;
    static public Rigidbody2D rb;
    public float speed;

    static public Animator animator;
    static public bool lookLeft, lookRight;
    static public bool control;
    bool canPick = false;
    public GameObject lantern;
    static public bool Lwalk = false;
    public GameObject map;
    bool open=false;
    static public bool Cwalk = false;
    [Space]
    public AudioClip walking;
    AudioSource audiosource;

    // Use this for initialization
	void Start () {
        rb= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lookRight = true;
        lookLeft = false;
        control = true;
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (control == true)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if (moveInput < 0)
            {
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(walking, 0.6f);
                }
                
                if (Lwalk == false)
                {
                    animator.SetBool("walk", true);
                }
                if (Lwalk == true)
                {
                    animator.SetBool("Lwalk", false);
                }

                if (Cwalk == true)
                {
                    animator.SetBool("Cwalk", false);
                }

                if (lookLeft == false)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    lookLeft = true;
                    lookRight = false;
                }


            }
            if (moveInput == 0)
            {
                if (!audiosource.isPlaying)
                {
                    audiosource.Pause();
                }
                if (Lwalk == false)
                {
                    animator.SetBool("walk", false);
                }
                if (Lwalk == true&Cwalk==false)
                {
                    animator.SetBool("Lwalk", true);
                }
                if (Cwalk == true)
                {

                    animator.SetBool("Cwalk", true);
                }

            }
            if (moveInput > 0)
            {
                if (!audiosource.isPlaying)
                {
                    audiosource.PlayOneShot(walking, 0.6f);
                }
                if (Lwalk == false)
                {
                    animator.SetBool("walk", true);
                }
                if (Lwalk == true)
                {
                    animator.SetBool("Lwalk", false);
                }

                if (Cwalk == true)
                {
                    animator.SetBool("Cwalk", false);
                }

                if (lookRight == false)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                    lookLeft = false;
                    lookRight = true;
                }
            }


            if (Input.GetKeyDown("m"))
            {
                toggleMap();
            }

            
            /*if (Input.GetKeyDown("e") && canPick == true)
            {
                lantern.SetActive(true);
                animator.SetTrigger("lantern");
                Destroy(GameObject.FindGameObjectWithTag("lantern"));
                Lwalk = true;
                
            }*/
        }
        
    }

    void toggleMap()
    {
        if (open == false)
        {
            map.SetActive(true);
            open = true;
        }
        else
        {
            map.SetActive(false);
            open = false;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "lantern")
        {
            canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "lantern")
        {
            canPick = false; ;
        }
    }*/
}

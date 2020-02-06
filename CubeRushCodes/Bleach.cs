using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleach : MonoBehaviour {

    public float waitTime;
    public float slowPower;
    bool upTime=false;
    Rigidbody2D rb;
    AudioSource audiosource;
    public AudioClip barrelSound;
    public float soundLevel;
    // Use this for initialization
	void Start () {

         rb = GetComponent<Rigidbody2D>();
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
        /*if (upTime == true)
        {
            StartCoroutine(Slowtime());
        }*/
	}

    public void OnCollisionEnter2D(Collision2D other)
    {
        /*if(other.gameObject.tag== "Player")
        {
            PlayerControl.speed = PlayerControl.speed -5f;
            
            Destroy(this.gameObject);
            upTime = true;
        }*/

        if (other.gameObject.tag == "Player")
        {
            audiosource.PlayOneShot(barrelSound, soundLevel);
            rb.AddForce(new Vector2(10f,Random.Range(5f,13f)), ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(20f,100f));
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Slowtime());
        }
    }

    IEnumerator Slowtime()
    {
        PlayerControl.speed -= 1f;
        yield return new WaitForSeconds(0.6f);
        PlayerControl.speed =CameraScript.speed;


    }
}

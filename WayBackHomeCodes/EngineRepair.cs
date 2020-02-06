using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EngineRepair : MonoBehaviour {

    public GameObject Yazı;
    public Text text;
    public string[] sentences;
    int index;
    bool next;
    public float typingSpeed;
    bool isTrigger = false;
    public Animator ani;
    bool talk = true;
    bool change = false;
    public UnityEvent events;
    AudioSource audiosource;
    public AudioClip speech;
    
    

    // Use this for initialization
    void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (text.text == sentences[index])
        {
            next = true;
        }

        if (Input.GetKeyDown("e") && isTrigger == true )
        {
            Character.control = false;
            StartCoroutine(Type());
            Yazı.SetActive(true);
            isTrigger = false;
        }

        if (Input.GetKeyDown("space") && next == true&talk==true)
        {
            nextSentence();
        }

        if (index == 1&&change==false)
        {
            talk = false;
            StartCoroutine(fade());
            change = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            isTrigger = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {

            isTrigger = false;

        }
    }



    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(speech, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator fade()
    {
        ani.SetTrigger("fade");
        yield return new WaitForSeconds(1.2f);
        ani.SetTrigger("fadeout");
        text.text = "";
        talk = true;
    }


    public void nextSentence()
    {
        next = false;
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            text.text = "";
            events.Invoke();
            index = 0;
            Character.control = true;
            gameObject.GetComponent<EngineRepair>().enabled = false;

        }
    }
}

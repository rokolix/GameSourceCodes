using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FamilyPhoto : MonoBehaviour {

    public GameObject Yazı;
    public Text text;
    public string[] sentences;
    public string[] AfterSentences;
    [Space]
    AudioSource audiosource;
    public AudioClip sound;
    [Space]
    
    public bool eventTalks=false;
     
    public string[] eventspeech;

    int index;
    public float typingSpeed;
    bool next;
    bool over=true;
    public bool after = false;
    bool isTrigger = false;
    public UnityEvent events;
    public bool eventManager=false;
    bool eventContol = false;
    public bool afterTalks=false;
    

    // Use this for initialization
    void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (after == false&eventTalks==false)
        {
            if (text.text == sentences[index])
            {
                next = true;
            }
        }
        

        if (after == true&eventTalks==false)
        {
            
            if (text.text == AfterSentences[index])
            {
                next = true;
            }
        }

        if (eventTalks == true& after==true)
        {

            if (text.text == eventspeech[index])
            {
                next = true;
            }
        }


        if (Input.GetKeyDown("e") && over == true && after == false && isTrigger == true)
        {
            StartCoroutine(Type());
            Yazı.SetActive(true);
            over = false;
            Character.rb.velocity =new Vector2(0,0);
            
            Character.control = false;
            
            
        }

        if (Input.GetKeyDown("space") && next == true && after == false && isTrigger == true)
        {
            NextSentence();
        }

        if (Input.GetKeyDown("e") && over == true && after == true && isTrigger == true&& eventTalks==false)
        {
            StartCoroutine(AfterType());
            Yazı.SetActive(true);
            over = false;
            Character.control = false;
        }

        if (Input.GetKeyDown("space") && next == true && after == true && isTrigger == true&&eventTalks==false)
        {
            AfterNextSentence();
        }

        if (eventContol == true)
        {
            events.Invoke();
            if (eventTalks == true&&over==true)
            {
                StartCoroutine(EventType());
                Yazı.SetActive(true);
                over = false;
                Character.control = false;
                
            }
        }

        if (Input.GetKeyDown("space") && next == true && eventTalks==true && isTrigger == true)
        {
            eventNextSentence();
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
        foreach(char letter in sentences[index].ToCharArray())
        {
            
            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(sound, 1f);
            }
            
            yield return new WaitForSeconds(typingSpeed);
            
        }
    }

    IEnumerator AfterType()
    {
        foreach (char letter in AfterSentences[index].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(sound, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator EventType()
    {
        foreach (char letter in eventspeech[index].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(sound, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public void NextSentence()
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
            over = true;
            index = 0;
            Character.control = true;
            if (eventManager==true )
            {
                if (events != null)
                {
                    Debug.Log("bırb");
                    eventContol = true;
                    
                }


            }
            if (afterTalks == true)
            {
                after = true;
            }
        }
    }

    public void AfterNextSentence()
    {
        next = false;

        if (index < AfterSentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(AfterType());
        }
        else
        {
            text.text = "";
            over = true;
            index = 0;
            Character.control = true;
        }
    }

    public void eventNextSentence()
    {
        next = false;

        if (index < eventspeech.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(EventType());
        }
        else
        {
            text.text = "";
            over = true;
            index = 0;
            eventTalks = false;
            Character.control = true;
        }
    }
}

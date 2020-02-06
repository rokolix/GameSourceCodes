using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CommRoomEvent : MonoBehaviour {

    public CinemachineVirtualCamera vcam;
    public GameObject Yazı;
    public Text text;
    public string[] sentences;
    int index;
    bool next;
    public float typingSpeed;
    bool isTrigger = false;
    public Transform radioTable;
    public GameObject character;
    public float moveSpeed;
    bool moveTime = false;
    bool talk = true;
    bool shrink = false;
    public float shrinkSpeed;
    bool objective = true;
    [Space]
    public GameObject radioo;
    AudioSource audiosource;
    public AudioClip herold,emily;
    bool girl = true;
    // Use this for initialization
    void Start ()
    {

        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isTrigger == true&&objective==true)
        {
            Character.control = false;
            StartCoroutine(Type());
            Character.animator.SetBool("Lwalk", true);
            radioo.GetComponent<FamilyPhoto>().enabled = false;
            Yazı.SetActive(true);
            isTrigger = false;
            objective = false;
        }

        if (text.text == sentences[index])
        {
            next = true;
            if (girl == true)
            {
                girl = false;
            }
            if (index == 1)
            {
                girl = true;
            }
            
        }

        if (Input.GetKeyDown("space") && next == true&&talk==true)
        {
            nextSentence();
        }

        if (index == 2)
        {
            moveTime = true;
            girl = true;
        }

        if (index == 10)
        {
            shrink = true;
        }


        if (shrink == true)
        {
            if(vcam.m_Lens.OrthographicSize <= 5.4f)
            {
                vcam.m_Lens.OrthographicSize += shrinkSpeed * Time.deltaTime;
            }

            if (vcam.m_Lens.OrthographicSize >= 5.4f)
            {
                
                StartCoroutine(wait());
                vcam.m_Lens.OrthographicSize = 5.4f;
                shrink = false;
                
            }
        }

        if (moveTime == true)
        {
            
            character.transform.position = Vector2.MoveTowards(character.transform.position, radioTable.transform.position, moveSpeed * Time.deltaTime);
            Character.animator.SetBool("Lwalk", false);
            talk = false;
            if (Vector2.Distance(character.transform.position, radioTable.transform.position) <=0.7)
            {
                girl = false;
                Character.animator.SetBool("Lwalk", true);
                if (index == 2)
                {
                    
                    nextSentence();

                }
                talk = true;
                moveTime = false;
                
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Character.animator.SetBool("Lwalk", true);
            isTrigger = true;
            
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying&&girl==true)
            {
                audiosource.PlayOneShot(emily, 1f);
            }
            if (!audiosource.isPlaying&&girl==false)
            {
                audiosource.PlayOneShot(herold, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
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
            
            index = 0;
            Character.control = true;
            radioo.GetComponent<FamilyPhoto>().enabled = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        
        
        nextSentence();
        
    }
}

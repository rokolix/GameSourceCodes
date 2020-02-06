using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;

public class ControlPanel : MonoBehaviour {

    public CinemachineVirtualCamera vcam;
    public GameObject Yazı;
    public Text text;
    public string[] sentences;
    public string[] engineSentences;
    public string[] lastSentences;
    int index,Aindex,Lindex;
    bool next;
    public float typingSpeed;
    public float shrinkSpeed;
    public float noiseSpeed;
    bool isTrigger = false;
    static public bool objective1 = false;
    static public bool objective2 = false;
    bool shake1 = false;
    bool completed1 = false;
    private CinemachineBasicMultiChannelPerlin noise;
    public GameObject character;
    public GameObject Vcam;
    public Animator ani,time;
    public Transform nextRoomPosition;
    bool talk = true;
    bool change = false;
    bool fading = true;
    [Space]
    public Text objIndicator;
    public GameObject engine;
    public UnityEvent events;
    AudioSource audiosource;
    public AudioClip Boom, herold, lockwood, ending;
    bool man = false;
    bool end = true;
    public Animator WhiteImage;
    public GameObject ımage;
    public GameObject button, fin;
    // Use this for initialization
    void Start ()
    {
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(objective1);
        Debug.Log(shake1);
        if (text.text == engineSentences[Aindex])
        {
            next = true;
        }

        if (text.text == sentences[index])
        {
            next = true;
        }

        if (text.text == lastSentences[Lindex])
        {
            next = true;
            if (Lindex == 8)
            {
                man = false;
            }
            if (Lindex == 15)
            {
                man = true;
            }
            if (Lindex == 17)
            {
                man = false;
            }
            if (Lindex == 18)
            {
                man = true;
            }
            if (Lindex == 22)
            {
                man = false;
            }
            if (Lindex == 26)
            {
                man = true;
            }
            if (Lindex == 32)
            {
                man = false;
            }
            if (Lindex == 33)
            {

                if (end == true)
                {
                    audiosource.PlayOneShot(Boom, 1f);
                    audiosource.PlayOneShot(ending, 1f);
                    StartCoroutine(credit());
                    end = false;
                }
            }
        }

        if (Input.GetKeyDown("e") && isTrigger == true&& objective1==true)
        {
            Character.control = false;
            StartCoroutine(Type());
            Yazı.SetActive(true);
            isTrigger= false;
        }

        if (Input.GetKeyDown("e") && isTrigger == true && objective2 == true)
        {
            Character.control = false;
            StartCoroutine(lastType());
            Yazı.SetActive(true);
            isTrigger = false;
        }

        if (Input.GetKeyDown("space") && next == true&&objective1==true&&objective2==false&&talk==true)
        {
            nextSentence();
            Debug.Log("Wololo");
        }

        if (Input.GetKeyDown("space") && next == true&&objective1==false&&objective2==false&&talk==true)
        {
            EnextSentence();
        }

        if (Input.GetKeyDown("space") && next == true && objective2 == true&&talk==true&&objective1==false)
        {
            LnextSentence();
        }

        if (index == 1)
        {
            shake1 = true;
        }

        if (shake1==true&&objective1==true)
        {
            if(noise.m_AmplitudeGain <= 2f && vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize >= 4.5f)
            {
                Debug.Log("qwq");
                talk = false;
                noise.m_AmplitudeGain += noiseSpeed;
                vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= shrinkSpeed * Time.deltaTime;

                if(noise.m_AmplitudeGain >= 2f)
                {
                    noise.m_AmplitudeGain = 2f;
                }

                if (vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize <= 4.5f)
                {
                    vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 4.5f;
                }
            }
            if(noise.m_AmplitudeGain >= 2f && vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize <= 4.5f)
            {
                Debug.Log("Patates");
                if (vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize <= 4.5f)
                {
                    StartCoroutine(Dive());
                    shake1 = false;
                    objective1 = false;
                }
                    
                

            }
            
        }
        //LastPart start here
        if (Lindex == 6 && objective2 == true&&change==false&&fading==true)
        {
            StartCoroutine(fade());
            
            fading = false;
            
        }

        if (change == true)
        {
            talk = true;
            Vcam.SetActive(true);
            Debug.Log(next);
            
        }

        if (completed1 == true)
        {
            if(vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize <= 5f)
            {
                vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize += shrinkSpeed * Time.deltaTime;
                text.text = "";
                

            }
            if(vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize >= 5f)
            {
                
                
                vcam.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5f;
                talk = true;
                StartCoroutine(EngineType());
                events.Invoke();
                completed1 = false;
                
            }
            
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
                audiosource.PlayOneShot(herold, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator lastType()
    {
        foreach (char letter in lastSentences[Lindex].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying&&man==false)
            {
                audiosource.PlayOneShot(herold, 1f);
            }
            if (!audiosource.isPlaying&&man==true)
            {
                audiosource.PlayOneShot(lockwood, 0.7f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator EngineType()
    {
        foreach (char letter in engineSentences[Aindex].ToCharArray())
        {
            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(herold, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator Dive()
    {
        noise.m_AmplitudeGain = 8f;
        yield return new WaitForSeconds(1.5f);
        audiosource.PlayOneShot(Boom, 1f);
        noise.m_AmplitudeGain = 0f;
        completed1 = true;
        
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
            
        }
    }

    public void EnextSentence()
    {
        next = false;

        if (Aindex < engineSentences.Length - 1)
        {
            Aindex++;
            text.text = "";
            StartCoroutine(EngineType());
            Debug.Log(index);
        }
        else
        {
            Debug.Log("Mıstır");
            text.text = "";
            events.Invoke();
            engine.GetComponent<FamilyPhoto>().enabled = true;
            Character.control = true;
            Aindex = 0;

        }
    }

    public void LnextSentence()
    {
        next = false;
        if (Lindex < lastSentences.Length - 1)
        {
            Lindex++;
            text.text = "";
            StartCoroutine(lastType());
        }
        else
        {
            text.text = "";

            index = 0;

        }
    }

    IEnumerator fade()
    {
        ani.SetTrigger("fade");
        time.Play("time");
        
        talk = false;
        
        yield return new WaitForSeconds(3f);
        Vcam.SetActive(false);
        man = true;
        Vcam.transform.position = nextRoomPosition.transform.position;
        character.transform.position = nextRoomPosition.transform.position;
        ani.SetTrigger("fadeout");
        change = true;




    }

    IEnumerator credit()
    {
        ımage.SetActive(true);
        WhiteImage.Play("whiteFade");
        yield return new WaitForSeconds(2f);
        button.SetActive(true);
        fin.SetActive(true);
    }

    
}

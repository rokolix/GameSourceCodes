using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Events;
using UnityEngine.PostProcessing;

public class KitchenEvent : MonoBehaviour {

    public CinemachineVirtualCamera vcam;
    public GameObject Yazı;
    public Text text;
    public string[] sentences;
    int index;
    bool next;
    public float typingSpeed;
    public GameObject character;
    public Transform targetPosition;
    bool isTrigger = false;
    bool talk = true;
    bool grow = false;
    public float growSpeed;
    public Animator ani;
    bool change = true;
    bool moving = false;
    bool localScale = true;
    public Text objIndicator;
    public UnityEvent events;
    public GameObject commDoor;
    AudioSource audiosource;
    public AudioClip herold;
    public PostProcessingProfile ppProfile;
    bool vomit = false;
    bool vomit2 = false;
    // Use this for initialization
    void Start () {
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("e")&&isTrigger == true)
        {
            Character.control = false;
            StartCoroutine(Type());

            Yazı.SetActive(true);
            isTrigger = false;
        }

        


        if (text.text == sentences[index])
        {
            next = true;
        }

        if (Input.GetKeyDown("space") && next == true && talk == true)
        {
            nextSentence();
        }


        if (index == 20)
        {
            grow = true;
        }

        if (grow == true && change==true)
        {
            if(vcam.m_Lens.OrthographicSize >= 4.5f)
            {
                vcam.m_Lens.OrthographicSize -= growSpeed * Time.deltaTime;
                talk = false;
            }
            if (vcam.m_Lens.OrthographicSize <= 4.5f)
            {
                if (index == 20)
                {
                    text.text = "";
                    
                    
                }
                
                vcam.m_Lens.OrthographicSize = 4.5f;
                StartCoroutine(fade());
                grow = false;
                change = false;
                Debug.Log("bozuk");
            }
        }

        if (index == 22)
        {
            moving = true;
            talk = false;
        }

        if (index == 23)
        {
            Character.animator.Play("CrippleStop");
            Character.Cwalk = true;
            character.GetComponent<Character>().speed = 1.5f;
            Character.animator.SetBool("Cwalk", true);
        }
        
        
        if (moving == true&&talk==false)
        {
            character.transform.position = Vector2.MoveTowards(character.transform.position, targetPosition.transform.position,2*Time.deltaTime);
            if (Character.Cwalk == false)
            {
                Character.animator.SetBool("Lwalk", false);
            }

            if (Character.lookRight==true)
            {
                if (character.transform.localScale == new Vector3(character.transform.localScale.x, character.transform.localScale.y, character.transform.localScale.z) && localScale == true)
                {
                    Character.lookLeft = true;
                    Character.lookRight = false;
                    character.transform.localScale = new Vector3(-character.transform.localScale.x, character.transform.localScale.y, character.transform.localScale.z);
                    localScale = false;
                }
            }
            
            
            
            




            if (Vector2.Distance(character.transform.position, targetPosition.transform.position) <= 0.7)
            {
                Character.animator.Play("throwUp");
                vomit = true;
                //Karakter kusma anı,post process ve yavas yurume;
                talk = true;
                moving = false;
            }

            //PostProcessEffect
            /*
            if (vomit == true)
            {
                ColorGradingModel.Settings colorgrade = ppProfile.colorGrading.settings;
                ppProfile.colorGrading.enabled = true;
                ChromaticAberrationModel.Settings chromatic = ppProfile.chromaticAberration.settings;
                ppProfile.chromaticAberration.enabled = true;
                if (colorgrade.basic.saturation >= 0.4f&&chromatic.intensity<=1)
                {
                    colorgrade.basic.saturation -= 0.07f*Time.deltaTime;
                    chromatic.intensity += 0.07f*Time.deltaTime;
                    ppProfile.colorGrading.settings = colorgrade;
                    ppProfile.chromaticAberration.settings = chromatic;
                }

                if (colorgrade.basic.saturation <= 0.4f&&chromatic.intensity>=1)
                {
                    colorgrade.basic.saturation = 0.4f;
                    chromatic.intensity = 1f;
                    ppProfile.colorGrading.settings = colorgrade;
                    ppProfile.chromaticAberration.settings = chromatic;
                    vomit = false;
                }


            }

            if (vomit2 == true)
            {


            }*/

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
            commDoor.GetComponent<RoomChange>().enabled = false;
            commDoor.GetComponent<HomeTeleport>().enabled = true;
            Character.control = true;
            gameObject.SetActive(false);
        }
    }

    IEnumerator fade()
    {

        Debug.Log("Fdd");
        ani.SetTrigger("fade");
        yield return new WaitForSeconds(1.2f);
        ani.SetTrigger("fadeout");
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        
        talk = true;

    }
}

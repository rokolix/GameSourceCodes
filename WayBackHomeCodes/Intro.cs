using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public Text text,text2,text3,text4,text5,text6,text7,text8,Talks,morning;
    public string sentence, sentence1, sentence2, sentence3, sentence4, sentence5, sentence6, sentence7,monringSentence;
    public string[] Speechs;
    int Sindex;
    int index=0;
    bool next;
    public float typingSpeed;
    
    bool start=true;
    AudioSource audiosource;
    public AudioClip write,herold;
    public Animator image;
    bool goodbye = false;
    // Use this for initialization
    void Start ()
    {
        audiosource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (start == true)
        {
            StartCoroutine(Type());
            start = false;
        }

        if (morning.text == monringSentence)
        {
            goodbye = true;
            
        }

        if (goodbye == true)
        {
            StartCoroutine(loadScene());
            goodbye = false;
        }
        if (Talks.text == Speechs[Sindex])
        {
            next = true;
        }

        if (Input.GetKeyDown("space") && next == true)
        {
            NextSentence();
        }

        if (text.text == sentence&&index==0)
        {
            StartCoroutine(Type2());
            index++;
        }
        if (text2.text == sentence2 && index==1)
        {
            StartCoroutine(Type3());
            index++;
        }
        if (text3.text == sentence3 && index == 2)
        {
            StartCoroutine(Type4());
            index++;
        }
        if (text4.text == sentence4 && index == 3)
        {
            StartCoroutine(Type5());
            index++;
        }
        if (text5.text == sentence5 && index == 4)
        {
            StartCoroutine(Type6());
            index++;
        }
        if (text6.text == sentence6 && index == 5)
        {
            StartCoroutine(Type7());
            index++;
        }
        if (text7.text == sentence7 && index == 6)
        {
            StartCoroutine(fade());
            index++;
        }

        
        
    }

    IEnumerator Type()
    {
        foreach (char letter in sentence.ToCharArray())
        {

            text.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    IEnumerator Type2()
    {
        foreach (char letter in sentence2.ToCharArray())
        {

            text2.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    IEnumerator Type3()
    {
        foreach (char letter in sentence3.ToCharArray())
        {

            text3.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    IEnumerator Type4()
    {
        foreach (char letter in sentence4.ToCharArray())
        {

            text4.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    IEnumerator Type5()
    {
        foreach (char letter in sentence5.ToCharArray())
        {

            text5.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    IEnumerator Type6()
    {
        foreach (char letter in sentence6.ToCharArray())
        {

            text6.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    IEnumerator Type7()
    {
        foreach (char letter in sentence7.ToCharArray())
        {

            text7.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }
    
    IEnumerator fade()
    {
        
        yield return new WaitForSeconds(2f);
        image.SetBool("baybay", true);
        text.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
        text5.enabled = false;
        text6.enabled = false;
        text7.enabled = false;
        text8.enabled = false;
        StartCoroutine(Speaking());
        
    }
    
    IEnumerator Speaking()
    {
        audiosource.pitch = 1f;
        yield return new WaitForSeconds(2f);
        StartCoroutine(speech());
    }

    IEnumerator speech()
    {
        foreach (char letter in Speechs[Sindex].ToCharArray())
        {

            Talks.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(herold, 1f);
            }

            yield return new WaitForSeconds(0.02f);

        }
    }

    public void NextSentence()
    {
        next = false;

        if (Sindex < Speechs.Length - 1)
        {
            Sindex++;
            Talks.text = "";
            StartCoroutine(speech());
        }
        else
        {
            Talks.text = "";
            StartCoroutine(nextLevel());
            Sindex = 0;
            
            
        }
    }

    IEnumerator nextLevel()
    {
        image.SetBool("open", true);
        yield return new WaitForSeconds(2f);
        StartCoroutine(morningText());
    }

    IEnumerator morningText()
    {
        foreach (char letter in monringSentence.ToCharArray())
        {

            morning.text += letter;
            if (!audiosource.isPlaying)
            {
                audiosource.PlayOneShot(write, 1f);
                audiosource.pitch = Random.Range(1.7f, 4);
            }

            yield return new WaitForSeconds(typingSpeed);

        }
    }

    IEnumerator loadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

}

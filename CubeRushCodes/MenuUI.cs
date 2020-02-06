using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour {


    public Animator startButton, aboutButton, text,back,face,image;
    public Image black;
    AudioSource audioSource;
    public AudioClip buttonSound;
    public float volume;

    // Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void About()
    {
        audioSource.PlayOneShot(buttonSound, volume);
        startButton.SetBool("move", true);
        aboutButton.SetBool("move", true);
        text.SetBool("move", true);
        back.SetBool("move", true);
        face.SetBool("move", true);
    }

    public void Back()
    {
        audioSource.PlayOneShot(buttonSound, volume);
        startButton.SetBool("move", false);
        aboutButton.SetBool("move", false);
        text.SetBool("move", false);
        back.SetBool("move", false);
        face.SetBool("move", false);
    }

    public void starting()
    {
        audioSource.PlayOneShot(buttonSound, volume);
        StartCoroutine(fading());

    }

    IEnumerator fading()
    {
        image.SetBool("fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(1);
    }
}

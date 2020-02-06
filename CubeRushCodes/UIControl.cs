using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {

    static public float score = 0;
    public Text TextScore;
    public Text TextHighScore;
    public Text MenuScore;
    public Text MenuHighScore;
    public static bool end = false;
    public GameObject inGameScore, inGameHighScore;

    // Use this for initialization
	void Start () {
        InvokeRepeating("addScore", 1, 1f);
        TextHighScore.text = "High Score: " + PlayerPrefs.GetFloat("HighScore",0).ToString();

    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        MenuScore.text = "Score: " + PlayerPrefs.GetFloat("Score", 0).ToString();
        MenuHighScore.text = "HighScore: " + PlayerPrefs.GetFloat("HighScore", 0).ToString();
        

        if (end == true)
        {
            inGameScore.SetActive(false);
            inGameHighScore.SetActive(false);
            CancelInvoke();

          

        }
	}

    public void addScore()
    {
        score = score + 1;
        TextScore.text = score.ToString();
        PlayerPrefs.SetFloat("Score", score);

        if (score > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", score);
            TextHighScore.text ="High Score: " + score.ToString();
        }
        
    }

    public void goHome()
    {
        SceneManager.LoadScene(0);
        end = false;
        inGameScore.SetActive(true);
        inGameHighScore.SetActive(true);
        PlayerControl.death = false;

    }


    public void playAgain()
    {
        SceneManager.LoadScene(1);
        end = false;
        inGameScore.SetActive(true);
        inGameHighScore.SetActive(true);
        PlayerControl.death = false;
    }

}


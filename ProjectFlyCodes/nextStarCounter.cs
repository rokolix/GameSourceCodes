using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class nextStarCounter : MonoBehaviour
{

    Text currentStars;
    Text nextStars;

    // Start is called before the first frame update
    void Start()
    {
        currentStars = GetComponent<Text>();
        nextStars = GameObject.Find("nextStar").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStarCount()
    {
        int totalStars = PlayerPrefs.GetInt("level1stars", 0) + PlayerPrefs.GetInt("level2stars", 0) + PlayerPrefs.GetInt("level3stars", 0) + PlayerPrefs.GetInt("level4stars", 0) + PlayerPrefs.GetInt("level5stars", 0) + PlayerPrefs.GetInt("level6stars", 0) + PlayerPrefs.GetInt("level7stars", 0) + PlayerPrefs.GetInt("level8stars", 0) + PlayerPrefs.GetInt("level9stars", 0) + PlayerPrefs.GetInt("level10stars", 0) + PlayerPrefs.GetInt("level11stars", 0) + PlayerPrefs.GetInt("level12stars", 0) + PlayerPrefs.GetInt("level13stars", 0) + PlayerPrefs.GetInt("level14stars", 0) + PlayerPrefs.GetInt("level15stars", 0) + PlayerPrefs.GetInt("level16stars", 0) + PlayerPrefs.GetInt("level17stars", 0) + PlayerPrefs.GetInt("level18stars", 0) + PlayerPrefs.GetInt("level19stars", 0) + PlayerPrefs.GetInt("level20stars", 0) + PlayerPrefs.GetInt("level21stars", 0) + PlayerPrefs.GetInt("level22stars", 0) + PlayerPrefs.GetInt("level23stars", 0) + PlayerPrefs.GetInt("level24stars", 0) + PlayerPrefs.GetInt("level25stars", 0) + PlayerPrefs.GetInt("level26stars", 0) + PlayerPrefs.GetInt("level27stars", 0) + PlayerPrefs.GetInt("level28stars", 0) + PlayerPrefs.GetInt("level29stars", 0) + PlayerPrefs.GetInt("level30stars", 0);
        currentStars.text = totalStars.ToString();

        nextStars.text = "/" + FindObjectOfType<UIManager>().required_Stars(SceneManager.GetActiveScene().buildIndex + 1).ToString();



    }
}

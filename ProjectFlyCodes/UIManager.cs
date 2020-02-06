using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    

    [Header("Button Settings")]
    public Animator playButtonAnim;
    public Animator backButtonAnim;
    public Animator gameTitleAnim;
    public Animator levelSelectViewportAnim;

    [Tooltip("Which level do you want to go ? MainMenu=0")]
    public int levelIndex;

    [Header("In Game - Pause Menu Buttons")]
    public Animator pauseButton;

    public Animator musicButton;
    public Animator soundButton;
    public Animator homeButton;
    
    public Animator retryButton;
    public Animator resumeButton;
    public Animator homeButton2;
    public Animator nextLevelButton;

    public GameObject bar;

    PlaneControl plane;

    StarCollector sC;
    GameObject StarUI;
    GameObject star1C;
    GameObject star2C;
    GameObject star3C;


    public GameObject endScreen;

    public bool levelComplete = false;
    [Header("SoundControl")]
    public bool inMenu=false;
    public AudioClip buttonSound;
    [Range(0.0f, 1.0f)]
    public float buttonVolume;


    AudioSource musicSource;
    AudioSource soundSource;
    AudioSource engineSource;

    public static int musicEnabled = 1;
    public static int soundEnabled = 1;

    Image soundAnim;
    Image musicAnim;

    GameObject nextLevelStarDisplay;

    // Start is called before the first frame update
    void Start()
    {

        soundSource = GameObject.Find("SoundControl").GetComponent<AudioSource>();

        musicEnabled = PlayerPrefs.GetInt("musicEnabled", 1);
        soundEnabled = PlayerPrefs.GetInt("soundEnabled", 1);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            checkReturnToLevelSelect();
            musicEnabled = 1;
            soundEnabled = 1;
            PlayerPrefs.SetInt("musicEnabled", 1);
            PlayerPrefs.SetInt("soundEnabled", 1);

        }
        else
        {
            plane = FindObjectOfType<PlaneControl>();
            star1C = GameObject.Find("endScreen").transform.GetChild(1).gameObject;
            star2C = GameObject.Find("endScreen").transform.GetChild(3).gameObject;
            star3C = GameObject.Find("endScreen").transform.GetChild(5).gameObject;
            endScreen.gameObject.SetActive(false);
            sC = FindObjectOfType<StarCollector>();
            engineSource = GameObject.Find("Plane").GetComponent<AudioSource>();

            nextLevelStarDisplay = GameObject.Find("currentPlayerStar");

            nextLevelStarDisplay.transform.parent = nextLevelButton.gameObject.transform;
            nextLevelStarDisplay.GetComponent<RectTransform>().localPosition = new Vector3(-296, -105, 0);


            Image[] images1 = GameObject.Find("Sound Button").GetComponentsInChildren<Image>();
            foreach (Image image in images1)
            {
                if (image.gameObject.transform.parent.name == "Sound Button")
                {
                    soundAnim = image;
                }
            }

            Image[] images2= GameObject.Find("Music Button").GetComponentsInChildren<Image>();
            foreach (Image image in images2)
            {
                if (image.gameObject.transform.parent.name == "Music Button")
                {
                    musicAnim = image;
                }
            }

            if (musicEnabled == 1)
            {
                musicAnim.sprite = Resources.Load<Sprite>("music_icon_on");
            }
            else
            {
                musicAnim.sprite = Resources.Load<Sprite>("music_icon_off");
            }


            if (soundEnabled == 1)
            {
                soundAnim.sprite = Resources.Load<Sprite>("sound_icon_on");
            }
            else
            {
                soundAnim.sprite = Resources.Load<Sprite>("sound_icon_off");
            }








        }


        musicSource = GameObject.Find("BackGroundSound").GetComponent<AudioSource>();
        

        if(soundEnabled == 1)
        {
            soundSource.enabled = true;
            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                engineSource.enabled = true;
            }
        }
        else
        {
            soundSource.enabled = false;
            if (SceneManager.GetActiveScene().buildIndex > 0)
            {
                engineSource.enabled = false;
            }
        }

        if (musicSource != null)
        {
            if (musicEnabled == 1)
            {
                musicSource.enabled = true;
            }
            else
            {
                musicSource.enabled = false;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //OnClick
    //SceneLoader Methods

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelIndex);

        //Sets Scene tracker to store the info that the player can return to Level select next time they load this scene.
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            FindObjectOfType<SceneTracker>().returnToMenu = true;
        }


    }
    




    //Animation Methods
    public void titleToLevelSelect()
    {
        playButtonAnim.Play("scaleDisappear");
        backButtonAnim.Play("scaleAppear");
        gameTitleAnim.Play("corner");
        levelSelectViewportAnim.Play("slideUpAppear");
        FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
    }

    public void LevelSelectToTitle()
    {
        playButtonAnim.Play("scaleAppear");
        backButtonAnim.Play("scaleDisappear");
        gameTitleAnim.Play("center");
        levelSelectViewportAnim.Play("slideDownDisappear");
        FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
    }


    public void checkReturnToLevelSelect()
    {
        //Checks if the player is returning to Level Select from a level.
        if ((FindObjectOfType<SceneTracker>().returnToMenu == true)&&(SceneManager.GetActiveScene().buildIndex == 0))
        {
            titleToLevelSelect();
        }
        
    }



    //Pause Methods
    //Disable & enable plane and other gameOject scripts here later.
    //Don't use delta.time=0 to pause game because it stops animations of menu buttons.
    public void PauseGame()
    {
        pauseButton.Play("scaleDisappear");

        musicButton.Play("scaleAppear");
        soundButton.Play("scaleAppear");
        homeButton.Play("scaleAppear");

        retryButton.Play("slideUpAppear");
        resumeButton.Play("slideUpAppear");

        
        
        Time.timeScale = 0;
        //AddingSound and Controlling
        inMenu = true;
        FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
        
    }

    public void ResumeGame()
    {
        pauseButton.Play("scaleAppear");

        musicButton.Play("scaleDisappear");
        soundButton.Play("scaleDisappear");
        homeButton.Play("scaleDisappear");

        retryButton.Play("scaleDisappear");
        resumeButton.Play("scaleDisappear");

        plane.enabled = true;
        Time.timeScale = 1;
        inMenu = false;
        FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
    }

    //Restart & Exit Methods

    public void restartLevel()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex);

    }

    public void exitLevel()
    {

        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void loadNextLevel()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(levelIndex+1);
    }

    public void ShowEndScreen()
    {


        int totalStars_ingame = PlayerPrefs.GetInt("level1stars", 0) + PlayerPrefs.GetInt("level2stars", 0) + PlayerPrefs.GetInt("level3stars", 0) + PlayerPrefs.GetInt("level4stars", 0) + PlayerPrefs.GetInt("level5stars", 0) + PlayerPrefs.GetInt("level6stars", 0) + PlayerPrefs.GetInt("level7stars", 0) + PlayerPrefs.GetInt("level8stars", 0) + PlayerPrefs.GetInt("level9stars", 0) + PlayerPrefs.GetInt("level10stars", 0) + PlayerPrefs.GetInt("level11stars", 0) + PlayerPrefs.GetInt("level12stars", 0) + PlayerPrefs.GetInt("level13stars", 0) + PlayerPrefs.GetInt("level14stars", 0) + PlayerPrefs.GetInt("level15stars", 0) + PlayerPrefs.GetInt("level16stars", 0) + PlayerPrefs.GetInt("level17stars", 0) + PlayerPrefs.GetInt("level18stars", 0) + PlayerPrefs.GetInt("level19stars", 0) + PlayerPrefs.GetInt("level20stars", 0) + PlayerPrefs.GetInt("level21stars", 0) + PlayerPrefs.GetInt("level22stars", 0) + PlayerPrefs.GetInt("level23stars", 0) + PlayerPrefs.GetInt("level24stars", 0) + PlayerPrefs.GetInt("level25stars", 0) + PlayerPrefs.GetInt("level26stars", 0) + PlayerPrefs.GetInt("level27stars", 0) + PlayerPrefs.GetInt("level28stars", 0) + PlayerPrefs.GetInt("level29stars", 0) + PlayerPrefs.GetInt("level30stars", 0);

        if (totalStars_ingame >= required_Stars(SceneManager.GetActiveScene().buildIndex + 1))
        {
            nextLevelButton.GetComponent<Button>().interactable = true;
            nextLevelStarDisplay.SetActive(false);
        }
        else
        {
            nextLevelButton.GetComponent<Button>().interactable = false;
            nextLevelStarDisplay.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex == 30)
        {
            nextLevelStarDisplay.SetActive(false);
            nextLevelButton.gameObject.SetActive(false);
        }

        nextLevelStarDisplay.GetComponent<nextStarCounter>().UpdateStarCount();


        endScreen.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        bar.SetActive(false);
        //StarUI.SetActive(false);
        retryButton.Play("slideUpAppear");
        nextLevelButton.Play("slideUpAppear");
        homeButton2.Play("slideUpAppear");

        
        

        

        if (sC.starCounter == 0)
        {
            star1C.SetActive(false);
            star2C.SetActive(false);
            star3C.SetActive(false);
        }
        else if (sC.starCounter == 1)
        {
            star1C.SetActive(true);
            star2C.SetActive(false);
            star3C.SetActive(false);
        }
        else if (sC.starCounter == 2)
        {
            star1C.SetActive(true);
            star2C.SetActive(true);
            star3C.SetActive(false);
        }
        else if (sC.starCounter == 3)
        {
            star1C.SetActive(true);
            star2C.SetActive(true);
            star3C.SetActive(true);
        }



    }

    public void toggleSound()
    {
        if (soundEnabled == 1)
        {
            soundEnabled = 0;
            soundSource.enabled = false;
            engineSource.enabled = false;
            soundAnim.sprite = Resources.Load<Sprite>("sound_icon_off");

        }
        else
        {
            soundEnabled = 1;
            soundSource.enabled = true;
            engineSource.enabled = true;
            soundAnim.sprite = Resources.Load<Sprite>("sound_icon_on");

        }
        FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
        PlayerPrefs.SetInt("soundEnabled", soundEnabled);
    }

    

    public void toggleMusic()
    {

        
            if (musicEnabled == 1)
            {
                musicEnabled = 0;
                musicSource.enabled = false;
                
                musicAnim.sprite = Resources.Load<Sprite>("music_icon_off");

            }
            else
            {
                musicEnabled = 1;
                musicSource.enabled = true;
                musicSource.Play();
                
                musicAnim.sprite = Resources.Load<Sprite>("music_icon_on");
            }
            FindObjectOfType<SoundManager>().playSounds(buttonSound, buttonVolume);
            PlayerPrefs.SetInt("musicEnabled", musicEnabled);
            
        

        
    }

    public void debug_unlockAllLevels()
    {
        PlayerPrefs.SetInt("stars", 60);
        PlayerPrefs.SetInt("level1stars", 3);
        PlayerPrefs.SetInt("level2stars", 3);
        PlayerPrefs.SetInt("level3stars", 3);
        PlayerPrefs.SetInt("level4stars", 3);
        PlayerPrefs.SetInt("level5stars", 3);
        PlayerPrefs.SetInt("level6stars", 3);
        PlayerPrefs.SetInt("level7stars", 3);
        PlayerPrefs.SetInt("level8stars", 3);
        PlayerPrefs.SetInt("level9stars", 3);
        PlayerPrefs.SetInt("level10stars", 3);
        PlayerPrefs.SetInt("level11stars", 3);
        PlayerPrefs.SetInt("level12stars", 3);
        PlayerPrefs.SetInt("level13stars", 3);
        PlayerPrefs.SetInt("level14stars", 3);
        PlayerPrefs.SetInt("level15stars", 3);
        PlayerPrefs.SetInt("level16stars", 3);
        PlayerPrefs.SetInt("level17stars", 3);
        PlayerPrefs.SetInt("level18stars", 3);
        PlayerPrefs.SetInt("level19stars", 3);
        PlayerPrefs.SetInt("level20stars", 3);
        
        PlayerPrefs.SetInt("level21stars", 3);
        PlayerPrefs.SetInt("level22stars", 3);
        PlayerPrefs.SetInt("level23stars", 3);
        PlayerPrefs.SetInt("level24stars", 3);
        PlayerPrefs.SetInt("level25stars", 3);

        PlayerPrefs.SetInt("level26stars", 3);
        PlayerPrefs.SetInt("level27stars", 3);
        PlayerPrefs.SetInt("level28stars", 3);
        PlayerPrefs.SetInt("level29stars", 3);
        PlayerPrefs.SetInt("level30stars", 3);

    }

    public void debug_resetStars()
    {
        PlayerPrefs.SetInt("stars", 0);
        PlayerPrefs.SetInt("level1stars", 0);
        PlayerPrefs.SetInt("level2stars", 0);
        PlayerPrefs.SetInt("level3stars", 0);
        PlayerPrefs.SetInt("level4stars", 0);
        PlayerPrefs.SetInt("level5stars", 0);
        PlayerPrefs.SetInt("level6stars", 0);
        PlayerPrefs.SetInt("level7stars", 0);
        PlayerPrefs.SetInt("level8stars", 0);
        PlayerPrefs.SetInt("level9stars", 0);
        PlayerPrefs.SetInt("level10stars", 0);
        PlayerPrefs.SetInt("level11stars", 0);
        PlayerPrefs.SetInt("level12stars", 0);
        PlayerPrefs.SetInt("level13stars", 0);
        PlayerPrefs.SetInt("level14stars", 0);
        PlayerPrefs.SetInt("level15stars", 0);
        PlayerPrefs.SetInt("level16stars", 0);
        PlayerPrefs.SetInt("level17stars", 0);
        PlayerPrefs.SetInt("level18stars", 0);
        PlayerPrefs.SetInt("level19stars", 0);
        PlayerPrefs.SetInt("level20stars", 0);
        
        PlayerPrefs.SetInt("level21stars", 0);
        PlayerPrefs.SetInt("level22stars", 0);
        PlayerPrefs.SetInt("level23stars", 0);
        PlayerPrefs.SetInt("level24stars", 0);
        PlayerPrefs.SetInt("level25stars", 0);

        PlayerPrefs.SetInt("level26stars", 0);
        PlayerPrefs.SetInt("level27stars", 0);
        PlayerPrefs.SetInt("level28stars", 0);
        PlayerPrefs.SetInt("level29stars", 0);
        PlayerPrefs.SetInt("level30stars", 0);



    }

    public int required_Stars(int a)
    {
        if (a == 0)
        {
            return 0;
        }
        else if (a == 1)
        {
            return 0;
        }
        else if(a == 2)
        {
            return 0;
        }
        else if (a == 3)
        {
            return 3;
        }
        else if (a == 4)
        {
            return 5;
        }
        else if (a == 5)
        {
            return 7;
        }
        else if (a == 6)
        {
            return 9;
        }
        else if (a == 7)
        {
            return 11;
        }
        else if (a == 8)
        {
            return 13;
        }
        else if (a == 9)
        {
            return 15;
        }
        else if (a == 10)
        {
            return 17;
        }
        else if (a == 11)
        {
            return 22;
        }
        else if (a == 12)
        {
            return 24;
        }
        else if (a == 13)
        {
            return 26;
        }
        else if (a == 14)
        {
            return 28;
        }
        else if (a == 15)
        {
            return 30;
        }
        else if (a == 16)
        {
            return 32;
        }
        else if (a == 17)
        {
            return 34;
        }
        else if (a == 18)
        {
            return 36;
        }
        else if (a == 19)
        {
            return 38;
        }
        else if (a == 20)
        {
            return 40;
        }
        else if (a == 21)
        {
            return 42;
        }
        else if (a == 22)
        {
            return 44;
        }
        else if (a == 23)
        {
            return 46;
        }
        else if (a == 24)
        {
            return 48;
        }
        else if (a == 25)
        {
            return 50;
        }
        else if (a == 26)
        {
            return 52;
        }
        else if (a == 27)
        {
            return 54;
        }
        else if (a == 28)
        {
            return 56;
        }
        else if (a == 29)
        {
            return 58;
        }
        else if (a == 30)
        {
            return 60;
        }
        else
        {
            return 0;
        }

    }



}

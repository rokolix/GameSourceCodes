using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour
{

    public int levelScene;

    public Text levelNumber;

    public int requiredStars = 0;

    public GameObject L;
    public Text reqStarText;

    collectedStars starData;

    // Start is called before the first frame update
    void Start()
    {
        starData = FindObjectOfType<collectedStars>();
        
    }

    // Update is called once per frame
    void Update()
    {
        updateLevelData();
    }

    public void setSceneIndex()
    {
        FindObjectOfType<UIManager>().levelIndex = levelScene;
    }


    public void updateLevelData()
    {
        reqStarText.text = requiredStars.ToString();


        if ( starData.totalStars >= requiredStars)
        {
            GetComponent<Button>().interactable = true;
            L.GetComponent<Image>().enabled = false;
            levelNumber.enabled = true;
            L.transform.GetChild(0).GetComponent<Image>().enabled = false;
            L.transform.GetChild(1).GetComponent<Text>().enabled = false;
            GetComponentInChildren<levelStars>().unlocked = true;

        }
        else
        {
            GetComponent<Button>().interactable = false;
            L.GetComponent<Image>().enabled = true;
            levelNumber.enabled = false;
            L.transform.GetChild(0).GetComponent<Image>().enabled = true;
            L.transform.GetChild(1).GetComponent<Text>().enabled = true;
            GetComponentInChildren<levelStars>().unlocked = false;
            
        }
    }
}

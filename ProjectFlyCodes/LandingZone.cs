using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;


public class LandingZone : MonoBehaviour
{
    bool landed,landingPermission;
    GameObject plane;
    
    float landingTime=0f;
    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindGameObjectWithTag("Player");
        
        landingPermission = plane.GetComponent<PlaneControl>().landing;
    }

    // Update is called once per frame
    void Update()
    {
        if (landed == true && landingPermission == true)
        {
            landingTime += Time.deltaTime;
            Debug.Log(landingTime);

            if (landingTime >2f && landed == true && landingPermission == true)
            {
                Debug.Log("Pass the level");
                landed = false;
                plane.GetComponent<PlaneControl>().enabled = false;

                string prefname = "level" + SceneManager.GetActiveScene().buildIndex.ToString() + "stars";
                if (FindObjectOfType<StarCollector>().starCounter > PlayerPrefs.GetInt(prefname, 0))
                {
                    PlayerPrefs.SetInt(prefname, FindObjectOfType<StarCollector>().starCounter);
                }

                FindObjectOfType<UIManager>().ShowEndScreen();
                this.enabled = false;
                AnalyticsEvent.LevelComplete(SceneManager.GetActiveScene().buildIndex);


            }
            
        }
        

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            landed = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            landed = false;

        }
    }

    private void AddStars()
    {

    }

    
}

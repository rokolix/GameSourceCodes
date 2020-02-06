using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour

{
    //Set to level start and end coordinates.

    public int levelStartX = 0;  //default values
    public int levelEndX = 999;


    int levelStartX_C = 0;
    int levelEndX_C = 100;

    float currentProgress = 0;

    public GameObject plane;
    public Slider bar;

    // Start is called before the first frame update
    void Start()
    {
        levelStartX_C = 0;
        levelEndX_C = levelEndX - levelStartX;

    }

    // Update is called once per frame
    void Update()
    {
        currentProgress = plane.transform.position.x - levelStartX;
        if (currentProgress < levelStartX_C)
        {
            currentProgress = 0;
        }
        bar.value = (currentProgress / levelEndX_C);
        
        
    }
}

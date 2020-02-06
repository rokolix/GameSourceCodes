using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time *speed,0);
		
	}
}

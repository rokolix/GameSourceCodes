using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour {

    Light flicker;
    public float minWaitTime;
    public float maxWaitTime;
    
    // Use this for initialization
	void Start () {
        flicker = GetComponent<Light>();
        StartCoroutine(flickering());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator flickering()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            flicker.enabled = !flicker.enabled;
        }
        
    }
}

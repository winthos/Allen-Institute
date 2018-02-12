using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

		
	}

    public void RandomColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        this.GetComponent<Renderer>().material.color = newColor;
    }

}

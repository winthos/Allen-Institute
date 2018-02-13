using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorChanger : MonoBehaviour 
{
    public bool LerpColors = false; //while the player is looking at a ball within range, it will constantly change colors
    public float lerpSpeed = 5f; //speed of the color changing

    //keep track of current and new color to lerp to
    public Color newColor;
    public Color currentColor;

	// Use this for initialization
	void Start () 
    {
        //start with a random color
        RandomColor();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (LerpColors == true)
        {
            //change outline color to cyan and show highlight
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.cyan);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.015f);

            //lerp current color to the new color
            currentColor = Color.Lerp(currentColor, newColor, Mathf.Sin(Time.deltaTime * Mathf.PI * lerpSpeed));
            this.GetComponent<Renderer>().material.SetColor("_Color", currentColor);

            //if finished lerping, reset colors to a new random color
            if (this.GetComponent<Renderer>().material.color == newColor)
            {
                LerpColors = false;
                Color c = new Color(Random.value, Random.value, Random.value, 1.0f);
                currentColor = newColor;
                newColor = c;
                LerpColors = true;
            }
        }

    }

    //ObjectGrabber will call this if this ball is targeted
    public void StartLerpingColor()
    {
        newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        currentColor = this.GetComponent<Renderer>().material.color;
        LerpColors = true;
    }

    //ObjectGrabber will call this when ball is not in focus
    public void StopLerpingColor()
    {
        LerpColors = false;

        //change outline color to black and remove highlight
        gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
        gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.002f);
    }

    //change to a random color instantly
    public void RandomColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        this.GetComponent<Renderer>().material.color = newColor;
    }



}

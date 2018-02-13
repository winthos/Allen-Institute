using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour 
{
    public bool LerpColors = false;
    public float lerpSpeed = 5f;

    public Color newColor;
    public Color currentColor;

    public GrabableObject grabableObjectComp;

	// Use this for initialization
	void Start () 
    {
        grabableObjectComp = gameObject.GetComponent<GrabableObject>();
	}
	
	// Update is called once per frame
	void Update () 
    {
       
        if (LerpColors == true && grabableObjectComp.beingLookedAt == true)
        {

            currentColor = Color.Lerp(currentColor, newColor, Mathf.Sin(Time.deltaTime * Mathf.PI * lerpSpeed));//Time.deltaTime * lerpSpeed);
            this.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
            //this.GetComponent<Renderer>().material.color = Color.Lerp(currentColor, newColor, lerpSpeed);

            if (this.GetComponent<Renderer>().material.color == newColor)
            {
                //LerpColors = false;
                print("test");
                LerpColors = false;
                Color c = new Color(Random.value, Random.value, Random.value, 1.0f);
                currentColor = newColor;
                newColor = c;
                LerpColors = true;
            }
        }

    }

    public void StartLerpingColor()
    {
        newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        currentColor = this.GetComponent<Renderer>().material.color;
        LerpColors = true;
        //ChangingColor();
    }

    public void RandomColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);

        this.GetComponent<Renderer>().material.color = newColor;
    }

    public void StopLerpingColor()
    {
        LerpColors = false;
    }

    public void ChangingColor()
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        Color currentColor = this.GetComponent<Renderer>().material.color;

        if (LerpColors == true)
        {
            this.GetComponent<Renderer>().material.color = Color.Lerp(currentColor, newColor, Time.deltaTime * lerpSpeed);

            if(this.GetComponent<Renderer>().material.color == newColor)
            {
                Color c = new Color(Random.value, Random.value, Random.value, 1.0f);
                currentColor = newColor;
                newColor = c;
            }
        }
    }

}

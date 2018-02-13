using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public GameObject Spawner = null; //the spawner to animate

    public AudioClip MachineSound = null; //sound the spawner makes

	// Use this for initialization
	void Start ()
    {
       Spawner = GameObject.FindWithTag("Spawner");
        UnHighlightButton();
    }

    public void PlaySpawnerAnimation()
    {
        //sound of spawner
        gameObject.GetComponent<AudioSource>().PlayOneShot(MachineSound);

        //play all spawner animations
        Spawner.GetComponent<Animator>().SetTrigger("Play");
        Spawner.GetComponent<SpawnerController>().Spawn();
        this.GetComponent<Animator>().SetTrigger("Play");

    }

    public void HighlightButton()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.cyan);
        gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.015f);
    }

    public void UnHighlightButton()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
        gameObject.GetComponent<Renderer>().material.SetFloat("_Outline", 0.002f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public GameObject Spawner = null;

	// Use this for initialization
	void Start ()
    {
       Spawner = GameObject.FindWithTag("Spawner");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlaySpawnerAnimation()
    {
        Spawner.GetComponent<Animator>().SetTrigger("Play");
        Spawner.GetComponent<SpawnerController>().Spawn();
        this.GetComponent<Animator>().SetTrigger("Play");

        //instantiate rollerball prefab here

        

    }
}

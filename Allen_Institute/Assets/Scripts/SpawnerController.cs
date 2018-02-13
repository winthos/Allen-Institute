using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject SpawnPosition;
    public GameObject Prefab;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Spawn()
    {
        Instantiate(Prefab, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
    }
}

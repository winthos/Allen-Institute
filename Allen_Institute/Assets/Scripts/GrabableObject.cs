using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour 
{
    public float throwForce = 400; //how hard to throw the ball 

    public GameObject HoldPosition; //position the ball is held when picked up

    public bool isHolding = false;

    public AudioClip[] BounceSounds;
    public AudioClip ThrowSound;

    // Use this for initialization
    void Start()
    {
        HoldPosition = GameObject.FindWithTag("HoldPosition");
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding == true)
        {
            //parent this ball to the player's HoldPosition
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.transform.parent = HoldPosition.transform;
            gameObject.transform.position = HoldPosition.transform.position;
            gameObject.GetComponent<Rigidbody>().freezeRotation = true;

            //drop the ball
            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
            }

            //throw the ball
            if (Input.GetMouseButton(1))
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(ThrowSound);
                isHolding = false;
                gameObject.GetComponent<Rigidbody>().AddForce(HoldPosition.transform.forward * throwForce);

            }
        }

        else
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.transform.parent = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if the ball hits anything, play a bouncing sound
        int i = Random.Range(0, BounceSounds.Length - 1);
        gameObject.GetComponent<AudioSource>().PlayOneShot(BounceSounds[i]);

        //if this touches another ball, that ball will change a random color
        if(collision.collider.tag == "Ball")
        {
            collision.collider.GetComponent<BallColorChanger>().RandomColor();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour 
{
    float throwForce = 600;

    public GameObject HoldPosition;
    //public Transform guide;
    public bool isHolding = false;

    public bool readyToChangeColor = true;

    public bool beingLookedAt = false;

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
            //readyToChangeColor = false;

            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            gameObject.transform.parent = HoldPosition.transform;
            gameObject.transform.position = HoldPosition.transform.position;


            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
            }

            //throw the ball
            if (Input.GetMouseButton(1))
            {
                isHolding = false;
                //Debug.Log("Trying to throw");
                gameObject.GetComponent<Rigidbody>().AddForce(HoldPosition.transform.forward * throwForce);
                //readyToChangeColor = false;
            }
        }

        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.transform.parent = null;
        }

    }
    /*
    void OnMouseDown()
    {

        //distance between grabable object ok we can use a raycast here
        if (distance <= 1.5f)
        {
            isHolding = true;
            //guide.transform.position = item.transform.position;
        }
    }
    void OnMouseUp()
    {
        isHolding = false;
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        isHolding = false;

        if(collision.collider.tag == "Ball")// && readyToChangeColor == true)
        {
            collision.collider.GetComponent<ColorChanger>().RandomColor();
        }

        readyToChangeColor = true;
    }
}

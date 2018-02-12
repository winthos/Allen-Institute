using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectGrabber : MonoBehaviour
{

    GameObject mainCamera;

    public float GrabableDistance = 2.5f;

    public GameObject Reticle = null;
    public GameObject InRangeReticle = null;

    bool ReadyToChangeColors = true;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");

        Reticle = GameObject.FindWithTag("Reticle");
        InRangeReticle = GameObject.FindWithTag("InRangeReticle");

        InRangeReticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) && Input.GetMouseButton(1) == false)
        {
            RaycastGrabable();
        }

        RaycastInRange();
    }


    //change the targeting reticle to show we are in range, also change color of object to random new color
    void RaycastInRange()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //if we are in range to pick up
            if (hit.collider.GetComponent<GrabableObject>() && hit.distance <= GrabableDistance)
            {
                InRangeReticle.SetActive(true);
                Reticle.SetActive(false);

                if(ReadyToChangeColors == true)
                {
                    hit.collider.GetComponent<ColorChanger>().RandomColor();
                    ReadyToChangeColors = false;
                }

            }

            //we aren't in range aw
            else
            {
                ReadyToChangeColors = true;
                InRangeReticle.SetActive(false);
                Reticle.SetActive(true);
            }
        }
    }

    void RaycastGrabable()
    {
       // print("trying to raycast");

        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.GetComponent<GrabableObject>() && hit.distance <= GrabableDistance)
            {
                print("found a ball");
                hit.collider.GetComponent<GrabableObject>().isHolding = true;
            }
    
        }
    }
}

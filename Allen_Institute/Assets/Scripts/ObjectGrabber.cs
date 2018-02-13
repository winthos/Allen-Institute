using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectGrabber : MonoBehaviour
{
    public float GrabableDistance = 2.5f;

    GameObject mainCamera;

    bool ReadyToChangeColors = true;

    public GameObject CurrentTarget = null;
    public GameObject CurrentButton = null;

    // Use this for initialization
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        //Left Click to grab a ball that is within range and check if there is a button within range
        if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) == false)
        {
            RaycastGrabable();
            RayCastButton();
        }

        //see if any interactable object in range needs to be highlighted
        RaycastInRange();
    }

    //raycast to check if a ball is in range
    void RaycastGrabable()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<GrabableObject>() && hit.distance <= GrabableDistance)
            {
                hit.collider.GetComponent<GrabableObject>().isHolding = true;
            }
        }
    }

    //raycast to see if button is in range
    void RayCastButton()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Button")
            {
                hit.transform.GetComponent<ButtonManager>().PlaySpawnerAnimation();
            }
        }

    }

    //change the targeting reticle to show we are in range, also change any ball within range and targeted to start changing colors
    void RaycastInRange()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //BALL SELECTION//////////////////////////////////////////////////////////////////////
            //check if the object is a Ball with the GrabableObject component
            if (hit.collider.GetComponent<GrabableObject>() && hit.distance <= GrabableDistance)
            {
                if (CurrentTarget != null)
                {
                    if (hit.transform != CurrentTarget.transform)
                    {
                        //first turn off the lerping color, then new target
                        //print("differentname");
                        CurrentTarget.GetComponent<BallColorChanger>().StopLerpingColor();
                        ReadyToChangeColors = true;
                    }
                }

                //if i have a current target, 
                //if the name of this target is different than the new target

                CurrentTarget = hit.transform.gameObject;

                //make current target look at something different
                if (ReadyToChangeColors == true)
                {

                    hit.collider.GetComponent<BallColorChanger>().StartLerpingColor();

                    ReadyToChangeColors = false;
                }
            } 

            //Nothing in range is grabable, change reticle to normal
            else
            {
                if (CurrentTarget != null)
                {
                    //any currently targeted ball stop changing color now
                    CurrentTarget.GetComponent<BallColorChanger>().StopLerpingColor();
                }

                ReadyToChangeColors = true;
            }

            //BUTTON SELECTION/////////////////////////////////////////////////////////////////////////////////////////
            //if player is looking at a button, show highlighted reticle and highlight button to show it is interactable
            if (hit.transform.tag == "Button")
            {
                if (CurrentButton != null)
                {
                    if (hit.transform != CurrentButton.transform)
                    {
                        hit.transform.GetComponent<ButtonManager>().UnHighlightButton();
                    }
                }

                CurrentButton = hit.transform.gameObject;
                hit.transform.GetComponent<ButtonManager>().HighlightButton();
            }

            else
            {
                if (CurrentButton != null)
                {
                    CurrentButton.GetComponent<ButtonManager>().UnHighlightButton();
                }
            }

        }
    }


}

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

    public GameObject CurrentTarget = null;

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
        if(Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) == false)
        {
            RaycastGrabable();
        }

        RaycastInRange();

        if(Input.GetMouseButtonUp(0))
        {
           /* if (CurrentTarget != null)
            {
                CurrentTarget.GetComponent<ColorChanger>().StopLerpingColor();
            }*/
        }
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


                if (CurrentTarget != null)
                {
                    if (hit.transform.name != CurrentTarget.transform.name)
                    {
                        //first turn off the lerping color, then new target
                        print("differentname");
                        CurrentTarget.GetComponent<ColorChanger>().StopLerpingColor();
                        ReadyToChangeColors = true;
                    }
                }

                //if i have a current target, 
                //if the name of this target is different than the new target

                CurrentTarget = hit.transform.gameObject;

                InRangeReticle.SetActive(true);
                Reticle.SetActive(false);

                //make current target look at something different
                if (ReadyToChangeColors == true)
                {
                    //print("hellow");
                    hit.collider.GetComponent<ColorChanger>().StartLerpingColor();

                    if (hit.collider.GetComponent<GrabableObject>().beingLookedAt == false)
                        hit.collider.GetComponent<GrabableObject>().beingLookedAt = true;
                    ReadyToChangeColors = false;
                }



            }

            //we aren't in range aw
            else
            {
                if (CurrentTarget != null)
                {
                    CurrentTarget.GetComponent<ColorChanger>().StopLerpingColor();
                    CurrentTarget.GetComponent<GrabableObject>().beingLookedAt = false;
                }

                ReadyToChangeColors = true;
                InRangeReticle.SetActive(false);
                Reticle.SetActive(true);
                //hit.collider.GetComponent<ColorChanger>().StopLerpingColor();
            }
        }

        else
        {
            if (CurrentTarget != null)
            {
                CurrentTarget.GetComponent<ColorChanger>().StopLerpingColor();
                CurrentTarget.GetComponent<GrabableObject>().beingLookedAt = false;
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

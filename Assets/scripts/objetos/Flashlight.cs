using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject lightDetect;
    bool on;

    // Start is called before the first frame update
    void Start()
    {
        on = false;
        flashlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!on && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(true);
            on = true;


            RaycastHit HitInfo;
            if (Physics.Raycast(flashlight.transform.position, flashlight.transform.forward, out HitInfo))
            {
                lightDetect.transform.position = (HitInfo.point);
            }
        }
        else if (on && Input.GetKeyDown(KeyCode.F))
        {   
            flashlight.SetActive(false);
            on = false;
        }
    }
}

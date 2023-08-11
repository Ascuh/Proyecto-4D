using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;

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
        }
        else if (on && Input.GetKeyDown(KeyCode.F))
        {
            flashlight.SetActive(false);
            on = false;
        }
    }
}

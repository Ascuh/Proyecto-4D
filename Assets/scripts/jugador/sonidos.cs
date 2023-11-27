using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonidos : MonoBehaviour
{
    public AudioSource caminar, correr;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                caminar.enabled = false;
                correr.enabled = true;
            }
            else
            {
                caminar.enabled = true;
                correr.enabled = false;
            }
        }
        else
        {
            caminar.enabled = false;
            correr.enabled = false;
        }
    
}
}

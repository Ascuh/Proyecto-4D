using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candado : MonoBehaviour
{
    public static bool Candado1 = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Llave.llave1 && RaycastCam.tocandoCand && Input.GetMouseButtonDown(0))
        {
            Candado1 = false;
            Destroy(gameObject);
        }
    }
}
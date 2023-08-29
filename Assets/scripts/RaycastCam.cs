using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{

    public Transform Camara;

    public float DistanciaRay;

    public static bool tocando;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // en el inspector aparece un raycast para que lo podamos ver
        Debug.DrawRay(Camara.position, Camara.forward * DistanciaRay, Color.red);

        RaycastHit toco;

        // se fija si hay un objeto con la layer "Objeto" tocando el raycast (delante de la camara)
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Objeto")))
        {
            tocando = true;
        }   
        
           
        

        else
        {
            tocando = false;
        }

            


    }
}

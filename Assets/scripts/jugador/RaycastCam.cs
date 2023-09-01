using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{

    public Transform Camara;

    public float DistanciaRay;

    public static bool tocandoObj;

    public static bool tocandoLlave;

    public static bool tocandoCand;

    bool tocandoPuerta;
    bool abierta;

    public Animator Puerta;
    public GameObject texto;

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
            tocandoObj = true;
        }   
        
        else
        {
            tocandoObj = false;
        }

        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Llave")))
        {
            tocandoLlave = true;
        }

        else
        {
            tocandoLlave = false;
        }

        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Candado")))
        {
            tocandoCand = true;
        }

        else
        {
            tocandoCand = false;
        }

        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Puerta")))
        {
            texto.SetActive(true);
            tocandoPuerta = true;
        }

        else
        {
            texto.SetActive(false);
            tocandoPuerta = false;
        }

        if (tocandoPuerta && Input.GetKeyDown(KeyCode.E) &&!abierta)
        {
            abierta = true;
            Puerta.SetBool("abierto", true);
            Puerta.SetBool("cerrado", false);
        }
        else if (tocandoPuerta && Input.GetKeyDown(KeyCode.E) && abierta)
        {
            abierta = false;
            Puerta.SetBool("abierto", false);
            Puerta.SetBool("cerrado", true);
        }


    }
    void abrir()
    {
        Puerta.SetBool("abierto", true);
        Puerta.SetBool("cerrado", false);
    }
    void cerrar()
    {
        Puerta.SetBool("abierto", false);
        Puerta.SetBool("cerrado", true);
    }
}

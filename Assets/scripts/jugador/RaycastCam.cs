using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RaycastCam : MonoBehaviour
{

    public Transform Camara;

    public float DistanciaRay;

    public static bool tocandoObj;

    public static bool tocandoLlave;

    public static bool tocandoCand;

    public static bool tocandoPuerta;

    public GameObject textoPuerta;
    public GameObject puertaBloqueada;

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

        if(Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay))
        {
            // Verificar si el rayo impactó en un objeto
            GameObject hitObject = toco.collider.gameObject;

            // Obtener el nombre del objeto
            string objectName = hitObject.name;
        }

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
            textoPuerta.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
            toco.collider.gameObject.GetComponent<Puerta>().Interactuar();
            }
        }
        else
        {
            textoPuerta.SetActive(false);
        }

        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("PuertaCand")))
        {
            if (!Candado.Candado1)
            {
            puertaBloqueada.SetActive(false);
            }
            else
            {
            puertaBloqueada.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E) && !Candado.Candado1)
            {
                toco.collider.gameObject.GetComponent<Puerta>().Interactuar();
            }

        }
        else
        {
            puertaBloqueada.SetActive(false);
        }
    }

}

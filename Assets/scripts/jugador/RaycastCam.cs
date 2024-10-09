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
    public static bool tocandoPuerta;
    public static bool tocandoPerilla;
    public static bool tocandoGenerador;
    public static bool lastimado;

    bool fogPrendida;

    public Llave llave1;
    public Llave llave2;
    public Llave llave3;

    string objectName;

    bool Candado1 = true;
    bool Candado2 = true;
    bool Candado3 = true;

    public Animator pantallaNegra;
    public Animator sangre;

    public GameObject fuego;
    public GameObject textoPuerta;
    public GameObject textoCajon;
    public GameObject textoGenerador;
    public GameObject puertaBloqueada;
    public GameObject puertaFinal;
    public GameObject prenderFogata;

    public PlayerCam ScriptCam;
    public AdministradorEscenas adminEscenas;

    public AudioSource quemado;

    // Start is called before the first frame update
    void Start()
    {
     ;
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
            objectName = hitObject.name;
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

        //script de las 3 llaves
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Llave")))
        {
            tocandoLlave = true;
            if (!Llave.equipped && tocandoLlave && Input.GetKeyDown(KeyCode.E) && !Llave.slotFull)
            {
                if (objectName == "llave1")
                {
                    llave1.PickUp();
                    Llave.llave1 = true;
                }
                    
                else if (objectName == "llave2")
                {
                    llave2.PickUp();
                    Llave.llave2 = true;
                }

                else if (objectName == "llave3")
                {
                    llave3.PickUp();
                    Llave.llave3 = true;
                }
            }
        }

        else
        {
            tocandoLlave = false;
        }

        if (Llave.llave1 && Input.GetKeyDown(KeyCode.Q))
        {
            llave1.Drop();
        }
        if (Llave.llave2 && Input.GetKeyDown(KeyCode.Q))
        {
            llave2.Drop();
        }
        if (Llave.llave3 && Input.GetKeyDown(KeyCode.Q))
        {
            llave3.Drop();
        }

        //script del candado
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Candado")))
        {
            tocandoCand = true;
            if (Llave.llave1 && tocandoCand && Input.GetMouseButtonDown(0) && objectName == "candado1")
            {
                toco.collider.gameObject.GetComponent<Candado>().candado();
                Candado1 = false;
            }
            if (Llave.llave2 && tocandoCand && Input.GetMouseButtonDown(0) && objectName == "candado2")
            {
                toco.collider.gameObject.GetComponent<Candado>().candado();
                Candado2 = false;
            }
            if (Llave.llave3 && tocandoCand && Input.GetMouseButtonDown(0) && objectName == "candado3")
            {
                toco.collider.gameObject.GetComponent<Candado>().candado();
                Candado3 = false;
            }
        }

        else
        {
            tocandoCand = false;
        }

        //script de las puertas
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

        //script de la puerta con candado
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("PuertaCand")))
        {
            if (!Candado1 && !Candado2 && !Candado3)
            {
            puertaBloqueada.SetActive(false);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    toco.collider.gameObject.GetComponent<Puerta>().Interactuar();
                }
            }
            else
            {
            puertaBloqueada.SetActive(true);
            }

        }
        else
        {
            puertaBloqueada.SetActive(false);
        }

        //script de la puerta final
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("PuertaFinal")))
        {
            puertaFinal.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(tiempoPuertaFinal());
                puertaFinal.SetActive(false);
            }
        }
        else
        {
            puertaFinal.SetActive(false);
        }

            //script de la fogata
            if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Fogata")))
        {
            if (!fogPrendida)
            {
                prenderFogata.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartCoroutine(tiempoFogata());
                }
            }
        }

        else
        {
            prenderFogata.SetActive(false);
        }

        //script de los cajones
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Cajon")))
        {
            if(textoCajon != null)
            {
                textoCajon.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    toco.collider.gameObject.GetComponent<Cajon>().Interactuar();
                }
                }
                else
                {
                    textoCajon.SetActive(false);
                }
            }


        //script de las luces
        if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Perilla")))
        {
            tocandoPerilla = true;
            if (tocandoPerilla && Input.GetMouseButtonDown(0))
            {
                  toco.collider.gameObject.GetComponent<Perilla>().activarPerilla();
            }

        }
        else
        {
            tocandoPerilla = false;
        }

            if(textoGenerador != null)
            {
                //script del generador
                if (Physics.Raycast(Camara.position, Camara.forward, out toco, DistanciaRay, LayerMask.GetMask("Generador")))
                {

                    tocandoGenerador = true;
                    if (!Generador.generador)
                    {
                    textoGenerador.SetActive(true);
                    }
                    else
                    {
                        textoGenerador.SetActive(false);
                    }

                    if (tocandoGenerador && Input.GetKeyDown(KeyCode.E))
                    {
                        toco.collider.gameObject.GetComponent<Generador>().funcionamiento();
                    }

                }
                else
                {
                    textoGenerador.SetActive(false);
                    tocandoGenerador = false;
                }
            }


    }

    IEnumerator tiempoFogata()
    {
        pantallaNegra.SetBool("prendida", true);
        ScriptCam.enabled = false;
        yield return new WaitForSeconds(1);
        fuego.SetActive(true);
        fogPrendida = true;
        prenderFogata.SetActive(false);
        yield return new WaitForSeconds(2);
        pantallaNegra.SetBool("prendida", false);
        ScriptCam.enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Fogata") && fogPrendida)
        {
            StartCoroutine(tiempoSangre());
        }
    
    }

    IEnumerator tiempoSangre()
    {
        if (lastimado)
        {
            adminEscenas.MainMenu();
        }
        else
        {
            quemado.enabled = true;
            sangre.SetBool("lastimado", true);
            lastimado = true;
            yield return new WaitForSeconds(5);
            sangre.SetBool("lastimado", false);
            lastimado = false;
            quemado.enabled = false;
        }
    }
    IEnumerator tiempoPuertaFinal()
    {
        pantallaNegra.SetBool("prendida", true);
        yield return new WaitForSeconds(1.5F);
        adminEscenas.CasaFinal();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perilla : MonoBehaviour
{

    public GameObject luz;
    public GameObject perillaPrendida;
    public GameObject perillaApagada;
    bool prendida;


    private void Start()
    {
        perillaApagada.SetActive(true);
        perillaPrendida.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Generador.generador)
        {
            luz.SetActive(false);
        }
    }

    public void activarPerilla()
    {
        if (!prendida)
        {
            perillaPrendida.SetActive(true);
            perillaApagada.SetActive(false);
            luz.SetActive(true);
            prendida = true;
        }
        else
        {
            perillaPrendida.SetActive(false);
            perillaApagada.SetActive(true);
            luz.SetActive(false);
            prendida = false;
        }
    }
}

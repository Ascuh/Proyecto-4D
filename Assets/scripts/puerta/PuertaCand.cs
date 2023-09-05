using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaCand : MonoBehaviour
{

    public bool abierta;

    public Animator animPuerta;

    void Start()
    {
        animPuerta = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void Interactuar()
    {
        if (abierta)
        {
            abierta = false;
            animPuerta.SetBool("abierto", false);
            animPuerta.SetBool("cerrado", true);
        }
        else
        {
            abierta = true;
            animPuerta.SetBool("abierto", true);
            animPuerta.SetBool("cerrado", false);
        }
    }
}

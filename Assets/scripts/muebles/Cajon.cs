using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cajon : MonoBehaviour
{

    public bool abierta;

    public Animator animCajon;

    void Start()
    {
        animCajon = GetComponent<Animator>();
    }

    void Update()
    {

    }
    public void Interactuar()
    {
        if (abierta)
        {
            abierta = false;
            animCajon.SetBool("abierto", false);
            animCajon.SetBool("cerrado", true);
        }
        else
        {
            abierta = true;
            animCajon.SetBool("abierto", true);
            animCajon.SetBool("cerrado", false);
        }
    }
}

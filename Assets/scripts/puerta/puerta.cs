using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{

    public Animator Puerta;
    public static GameObject texto;

    private void Start()
    {
        
    }
    public void abrir()
    {
        Puerta.SetBool("abierto", true);
        Puerta.SetBool("cerrado", false);
    }

    public void cerrar()
    {
        Puerta.SetBool("abierto", false);
        Puerta.SetBool("cerrado", true);
    }
    


}

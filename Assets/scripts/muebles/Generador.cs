using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{

    public static bool generador;

    // Update is called once per frame
    void Update()
    {
        
    }


    public void funcionamiento()
    {
        StartCoroutine(tiempoGenerador());
    }
    IEnumerator tiempoGenerador()
    {
        generador = true;
        yield return new WaitForSeconds(180);
        generador = false;
    }
}

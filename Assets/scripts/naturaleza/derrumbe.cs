using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class derrumbe : MonoBehaviour
{
    public CamaraSacudida camaraSacudida;
    public GameObject piedras;
    public GameObject lluvia;
    bool derrumbo;

    public AdministradorEscenas adminEscenas;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que tocó tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            // Llama a la función para activar el GameObject
            Derrumbe();
        }
    }

 

    private void Derrumbe()
    {
        if (!derrumbo)
        {
            derrumbo = true;
            piedras.SetActive(true);
            lluvia.SetActive(false);
            camaraSacudida.IniciarSacudida(3, 0.5f);
         //   RenderSettings.fog = false;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class taser : MonoBehaviour
{
    // Referencias a los GameObjects
    public GameObject ruidoTaser;
    public GameObject efectoTaser;

    public float cooldownLighting;  // Tiempo de espera
    public float cooldownGun;  // Tiempo de espera

    // Variable para controlar si el taser est� en cooldown
    private bool puedeUsarTaser = true;

    [SerializeField] taserRaycast TaserRaycast;

    void Update()
    {
        // Si se presiona el click izquierdo y no est� en cooldown
        if (Input.GetMouseButtonDown(0) && puedeUsarTaser)
        {
            puedeUsarTaser = false;

            // Activar el GameObject del ruidoTaser
            ruidoTaser.SetActive(true);
            efectoTaser.SetActive(true);

            // Iniciar cooldown
            StartCoroutine(IniciarCooldownLighting());
            StartCoroutine(IniciarCooldownGun());
            TaserRaycast.DetectarMonstruo();
        }
    }

    IEnumerator IniciarCooldownLighting()
    {
        yield return new WaitForSeconds(cooldownLighting);  // Esperar ? segundos
        efectoTaser.SetActive(false);  // Desactivar el ruidoTaser despu�s del cooldown
    }
    IEnumerator IniciarCooldownGun()
    {
        yield return new WaitForSeconds(cooldownGun);  // Esperar ? segundos
        ruidoTaser.SetActive(false);  // Desactivar el ruidoTaser despu�s del cooldown
        puedeUsarTaser = true;  // Permitir el uso nuevamente
    }
}
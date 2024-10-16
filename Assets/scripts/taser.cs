using System.Collections;
using UnityEngine;
using TMPro;  // Importa el namespace para TextMeshPro

public class taser : MonoBehaviour
{
    // Referencias a los GameObjects
    public GameObject ruidoTaser;
    public GameObject efectoTaser;

    public float cooldownLighting;  // Tiempo de espera
    public float cooldownGun;  // Tiempo de espera

    // Variable para controlar si el taser está en cooldown
    private bool puedeUsarTaser = true;

    [SerializeField] taserRaycast TaserRaycast;

    // Referencia al texto de la UI que mostrará el contador (TextMeshPro)
    public TextMeshProUGUI textoCooldown;

    void Update()
    {
        // Si se presiona el click izquierdo y no está en cooldown
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
        efectoTaser.SetActive(false);  // Desactivar el ruidoTaser después del cooldown
    }

    IEnumerator IniciarCooldownGun()
    {
        float tiempoRestante = cooldownGun;

        // Mientras el cooldown no haya terminado
        while (tiempoRestante > 0)
        {
            // Mostrar solo el número del tiempo restante en el texto
            textoCooldown.text = tiempoRestante.ToString("F1");  // Mostramos con 1 decimal

            // Reducir el tiempo restante
            yield return new WaitForSeconds(0.1f);  // Actualiza el contador cada 0.1 segundos
            tiempoRestante -= 0.1f;
        }

        // Cooldown completado, borra el texto
        textoCooldown.text = "";  // Limpia el texto cuando el cooldown termina
        ruidoTaser.SetActive(false);  // Desactivar el ruidoTaser después del cooldown
        puedeUsarTaser = true;  // Permitir el uso nuevamente
    }
}

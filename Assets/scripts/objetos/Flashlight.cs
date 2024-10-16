using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    private bool prendida = false;
    private float contador = 0f;
    [SerializeField] int bateriaPorFase;
    [SerializeField] GameObject[] estadoBateria;
    private int estadoActualBateria = 0;

    private Light flashlightLight;

    private int intensidadActual;
    private int randomInt;

    private bool yaPaso;
    // Start is called before the first frame update
    void Start()
    {
        flashlight.SetActive(false);
        estadoBateria[estadoActualBateria].SetActive(true);
        flashlightLight = flashlight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            prendida = !prendida;
            flashlight.SetActive(prendida);
        }

        if (prendida)
        {
            contador += Time.deltaTime;
        }

        if (contador >= bateriaPorFase)
        {
            contador = 0f;

            estadoBateria[estadoActualBateria].SetActive(false);

            if (estadoActualBateria < estadoBateria.Length - 1)
            {
                estadoActualBateria++;
            }
            estadoBateria[estadoActualBateria].SetActive(true);
        }

        if (estadoActualBateria == 1)
        {
            flashlightLight.intensity = 3f;
        }
        else if (estadoActualBateria == 2 && !yaPaso)
        {
            flashlightLight.intensity = 2f;
            intensidadActual = 2;
            yaPaso = true;
            StartCoroutine(cooldownApagon(3, 7, 1));
        }
        else if (estadoActualBateria == 3 && !yaPaso)
        {
            flashlightLight.intensity = 1f;
            intensidadActual = 1;
            yaPaso = true;
            StartCoroutine(cooldownApagon(2, 5, 2));
        }
        else if (estadoActualBateria == 4)
        {
            flashlightLight.intensity = 0f;
        }
    }

    IEnumerator cooldownApagon(int min, int max, int duracion)
    {
        flashlightLight.intensity = 0;
        yield return new WaitForSeconds(duracion);
        flashlightLight.intensity = intensidadActual;

        int randomInt = Random.Range(min, max); // El límite superior es exclusivo
        yield return new WaitForSeconds(randomInt);  // Esperar ? segundos
        yaPaso = false;
    }

}

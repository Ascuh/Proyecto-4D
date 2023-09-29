using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraSacudida : MonoBehaviour
{
    private Transform transformacionCamara;
    private Vector3 posicionOriginal;
    private float duracionSacudida = 0f;
    private float magnitudSacudida = 0.1f;
    private float velocidadAmortiguamiento = 2.0f;

    private void Awake()
    {
        transformacionCamara = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        posicionOriginal = transformacionCamara.localPosition;
    }

    private void Update()
    {
        if (duracionSacudida > 0)
        {
            transformacionCamara.localPosition = posicionOriginal + Random.insideUnitSphere * magnitudSacudida;

            duracionSacudida -= Time.deltaTime * velocidadAmortiguamiento;
        }
        else
        {
            duracionSacudida = 0f;
            transformacionCamara.localPosition = posicionOriginal;
        }
    }

    public void IniciarSacudida(float duracion, float magnitud)
    {
        duracionSacudida = duracion;
        magnitudSacudida = magnitud;
    }
}

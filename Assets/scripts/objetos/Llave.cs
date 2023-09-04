using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Llave : MonoBehaviour
{
    MeshRenderer meshrenderer;
    public GameObject Llave2d;
    public static bool llave1 = false;


    private void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }
    public void Agarro()
    {
        llave1 = true;
        Llave2d.SetActive(true);
        meshrenderer.enabled = false;
    }

    public void Solto()
    {
        llave1 = false;
        Llave2d.SetActive(false);
        meshrenderer.enabled = true;
    }
}

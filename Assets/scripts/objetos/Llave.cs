using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Llave : MonoBehaviour
{
    MeshRenderer meshrenderer;
    public GameObject Llave2d;

    private void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }
    public void Agarro()
    {
        Llave2d.SetActive(true);
        meshrenderer.enabled = false;
    }

    public void Solto()
    {
        Llave2d.SetActive(false);
        meshrenderer.enabled = true;
    }
}

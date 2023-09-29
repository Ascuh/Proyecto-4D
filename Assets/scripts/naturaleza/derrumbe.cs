using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class derrumbe : MonoBehaviour
{
    public CamaraSacudida camaraSacudida;
    public GameObject piedras;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        piedras.SetActive(true);
        camaraSacudida.IniciarSacudida(3, 0.5f);
    }
}

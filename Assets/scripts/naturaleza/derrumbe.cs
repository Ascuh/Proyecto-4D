using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class derrumbe : MonoBehaviour
{
    public CamaraSacudida camaraSacudida;
    public GameObject piedras;
    bool derrumbo;
    bool tiemblaPoco;
    bool tiemblaMucho;

    public string derrumb = "Derrumbe";
    public string tiembPoco = "tiemblaPoco";
    public string tiembMucho = "tiemblaMucho";
    public string muert = "Muerte";
    public string saliCueva = "salidaCueva";

    public AdministradorEscenas adminEscenas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(derrumb))
        {
            Derrumbe();
        }
        if (other.CompareTag(tiembPoco))
        {
            TiemblaPoco();
        }
        if (other.CompareTag(tiembMucho))
        {
            TiemblaMucho();
        }
        if (other.CompareTag(muert))
        {
            adminEscenas.MainMenu();
        }
        if (other.CompareTag(saliCueva))
        {
            SalidaCueva();
        }

    }

    private void Derrumbe()
    {
        if (!derrumbo)
        {
            derrumbo = true;
            piedras.SetActive(true);
            camaraSacudida.IniciarSacudida(3, 0.5f);
            RenderSettings.fog = false;
        }
    }
    private void TiemblaPoco()
    {
        if (!tiemblaPoco)
        {
            tiemblaPoco = true;
            camaraSacudida.IniciarSacudida(100, 0.02f);
        }
        else
        {
            tiemblaPoco = false;
            camaraSacudida.IniciarSacudida(0, 0);
        }
    }
    private void TiemblaMucho()
    {
        if (!tiemblaMucho)
        {
            tiemblaMucho = true;
            camaraSacudida.IniciarSacudida(100, 0.08f);
        }
        else
        {
            tiemblaMucho = false;
            camaraSacudida.IniciarSacudida(100, 0.02f);
        }
    }
    private void SalidaCueva()
    {
        piedras.SetActive(false);
        RenderSettings.fog = true;
    }
}

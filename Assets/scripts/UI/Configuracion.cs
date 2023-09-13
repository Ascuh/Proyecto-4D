using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Configuracion : MonoBehaviour
{

    public AudioMixer audioMixer;
  public void Volumen (float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

   public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
}

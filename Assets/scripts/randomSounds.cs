using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSounds : MonoBehaviour
{

    [SerializeField] GameObject randomSound1;
    [SerializeField] GameObject randomSound2;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IniciarSonidosRandom());
    }

    IEnumerator IniciarSonidosRandom()
    {
        yield return new WaitForSeconds(20f);
        randomSound1.SetActive(true);
        yield return new WaitForSeconds(30f);
        randomSound2.SetActive(true);
        yield return new WaitForSeconds(5f);
        randomSound1.SetActive(false);
        randomSound2.SetActive(false);
        StartCoroutine(IniciarSonidosRandom());
    }
}

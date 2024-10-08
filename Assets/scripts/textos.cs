using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textos : MonoBehaviour
{

    [SerializeField] GameObject iniciotxt;
    [SerializeField] GameObject ara�atxt;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Inicio());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Inicio()
    {
        iniciotxt.SetActive(true);
        yield return new WaitForSeconds(3f);
        iniciotxt.SetActive(false);
    }

    public IEnumerator Ara�a()
    {
        ara�atxt.SetActive(true);
        yield return new WaitForSeconds(3f);
        ara�atxt.SetActive(false);
    }
}

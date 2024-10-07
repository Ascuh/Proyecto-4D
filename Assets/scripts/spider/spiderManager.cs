using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spiderManager : MonoBehaviour
{

    public int arañasPegadas;
    public GameObject spider;
    private bool estasPorMorir;
    public GameObject player;
    public GameObject muerteAraña;
    public GameObject dolorAraña;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 

        if (player == null)
        {
            Debug.LogError("No se encontró un GameObject con el tag 'Player'");
        }

        arañasPegadas = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (estasPorMorir)
        {
            if (arañasPegadas == 1)
            {
                StartCoroutine(moristePorAraña());
            }
        }
        else
        {
            if (arañasPegadas == 1)
            {
                StartCoroutine(arañaAndate());
                arañasPegadas = 0;
                estasPorMorir = true;
            }

        }
    }

    private IEnumerator arañaAndate()
    {
        dolorAraña.SetActive(true);
        spider.SetActive(true);
        yield return new WaitForSeconds(3f);
        dolorAraña.SetActive(false);
        yield return new WaitForSeconds(2f);
        spider.SetActive(false);
    }
    private IEnumerator moristePorAraña()
    {
        muerteAraña.SetActive(true);
        yield return new WaitForSeconds(3f);
        muerteAraña.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}

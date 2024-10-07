using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class spiderManager : MonoBehaviour
{

    public int ara�asPegadas;
    public GameObject spider;
    private bool estasPorMorir;
    public GameObject player;
    public GameObject muerteAra�a;
    public GameObject dolorAra�a;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 

        if (player == null)
        {
            Debug.LogError("No se encontr� un GameObject con el tag 'Player'");
        }

        ara�asPegadas = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (estasPorMorir)
        {
            if (ara�asPegadas == 1)
            {
                StartCoroutine(moristePorAra�a());
            }
        }
        else
        {
            if (ara�asPegadas == 1)
            {
                StartCoroutine(ara�aAndate());
                ara�asPegadas = 0;
                estasPorMorir = true;
            }

        }
    }

    private IEnumerator ara�aAndate()
    {
        dolorAra�a.SetActive(true);
        spider.SetActive(true);
        yield return new WaitForSeconds(3f);
        dolorAra�a.SetActive(false);
        yield return new WaitForSeconds(2f);
        spider.SetActive(false);
    }
    private IEnumerator moristePorAra�a()
    {
        muerteAra�a.SetActive(true);
        yield return new WaitForSeconds(3f);
        muerteAra�a.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}

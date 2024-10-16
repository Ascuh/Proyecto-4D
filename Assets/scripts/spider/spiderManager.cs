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
    public GameObject moriste;
    [SerializeField] textos Textos;
    PlayerMovement playerMovement;
    [SerializeField] GameObject flashlight;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<PlayerMovement>();
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
        StartCoroutine(Textos.Araña());
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
        moriste.SetActive(true);
        playerMovement.die();
        flashlight.SetActive(false);
        yield return new WaitForSeconds(3f);
        muerteAraña.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

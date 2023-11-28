using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AdministradorEscenas : MonoBehaviour
{

    public GameObject pantallaDeCarga;
    public void MainScene()
    {
        // SceneManager.LoadScene("MainScene");
        StartCoroutine(loadSceneAsync("name"));
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void CasaFinal()
    {
        SceneManager.LoadScene("CasaFinal");
    }

    IEnumerator loadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        pantallaDeCarga.SetActive(true);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            CasaFinal();
        }
    }
}

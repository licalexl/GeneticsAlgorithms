using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pausa : MonoBehaviour
{
    public GameObject panelPausa;
    public void Pausa() 
    {
        Time.timeScale = 0;
        panelPausa.SetActive(true);
        
    }

    public void Despausa() 
    
    {
        Time.timeScale = 1;
        panelPausa.SetActive(false);
    }

    public void Reiniciar()

    {
        Time.timeScale = 1;
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
     
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }
}

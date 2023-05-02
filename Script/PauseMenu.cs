using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;

    public GameObject UI;

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }        
    }
    public void Resume()
    {
        UI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    void Pause()
    {
        UI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void ToQuit()
    {
        Application.Quit();
    }
}

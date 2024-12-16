using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
public class PauseMenuScript : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public FirstPersonController firstPersonController;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        Cursor.lockState = CursorLockMode.None;
        this.firstPersonController.cameraCanMove = false;
    }


    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.firstPersonController.cameraCanMove = true;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    public Canvas canvas;
    public string mainMenuName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                UnPauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        canvas.enabled = true;
    }

    private void UnPauseGame() 
    {
        Time.timeScale = 1;
        isPaused = false;
        canvas.enabled = false;
    }

    public void OnExitPressed()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuName);
    }

    public void OnResumePressed()
    {
        UnPauseGame();
    }

}

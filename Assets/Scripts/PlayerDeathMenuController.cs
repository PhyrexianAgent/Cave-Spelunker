using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathMenuController : MonoBehaviour
{
    private const string MAIN_MENU_SCENE_NAME = "Main Menu";

    public static PlayerDeathMenuController instance;

    [SerializeField] private string currentLevelName = "Level 1";

    public Animation anim;

    private void Awake()
    {
        instance = this;
    }

    public void PlayerDied()
    {
        Debug.Log("playing");
        anim.Play();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentLevelName);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }
}

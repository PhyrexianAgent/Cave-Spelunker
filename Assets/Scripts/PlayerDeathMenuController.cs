using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeathMenuController : MonoBehaviour
{
    private const string MAIN_MENU_SCENE_NAME = "Main Menu";
    private static string nextLevelName = "Level 1"; //first value is for whatever the first level the player can play's name is

    public static PlayerDeathMenuController instance;

    Scene scene;

    public Animator anim;
    public TextMeshProUGUI text;
    public CanvasGroup button_canvas;

    private float timer = 0;

    private bool is_active = false;
    

    private void Awake()
    {
        instance = this;
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //anim.Play();
            }
        }
    }

    public void LevelStart()
    {
        Debug.Log("level started");
    }

    public void StartLevelEnd(string nextLevelName)
    {
        PlayerDeathMenuController.nextLevelName = nextLevelName;
        is_active = true;
        anim.SetTrigger("End Level");
    }

    public void PlayerDied()
    {
        //anim.Play();
        //SceneManager.LoadScene(scene.name);
        anim.SetTrigger("Player Died");
        is_active = true;
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void PlayerWon()
    {
        text.text = "That is all for this current version of the game.";
        anim.SetTrigger("Player Won");
        //timer = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(scene.name);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
    }

    public static string GetCurrentLevel()
    {
        return nextLevelName;
    }

    public bool GetIsActive()
    {
        return is_active;
    }
}

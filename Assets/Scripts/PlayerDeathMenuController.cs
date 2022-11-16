using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeathMenuController : MonoBehaviour
{
    private const string MAIN_MENU_SCENE_NAME = "Main Menu";

    public static PlayerDeathMenuController instance;

    Scene scene;

    public Animator anim;
    public TextMeshProUGUI text;

    private float timer = 0;
    private string nextLevelName;

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

    public void StartLevelEnd(string nextLevelName)
    {
        this.nextLevelName = nextLevelName;
        anim.SetTrigger("End Level");
    }

    public void PlayerDied()
    {
        //anim.Play();
        //SceneManager.LoadScene(scene.name);
        anim.SetTrigger("Player Died");
    }

    public void ChangeLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }

    public void PlayerWon()
    {
        //text.text = "That is all for this current version of the game.";
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
}

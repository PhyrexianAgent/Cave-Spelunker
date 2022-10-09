using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeathMenuController : MonoBehaviour
{
    private const string MAIN_MENU_SCENE_NAME = "Main Menu";

    public static PlayerDeathMenuController instance;

    [SerializeField] private string currentLevelName = "Level 1";

    public Animation anim;
    public TextMeshProUGUI text;

    private float timer = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                anim.Play();
            }
        }
    }

    public void PlayerDied()
    {
        anim.Play();
    }

    public void PlayerWon()
    {
        text.text = "That is all for this current version of the game.";
        timer = 1;
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

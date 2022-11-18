using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainMenuController : MonoBehaviour
{
    public const string LEVEL_START_NAME = "Level ";

    public Canvas mainButtons;
    public Canvas levelPickButtons;
    public Button[] levelButtons;
    void Start()
    {
        EnableLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        //add end game code here
    }

    public void PlayGame()
    {
        mainButtons.enabled = false;
        levelPickButtons.enabled = true;
    }

    public void ReturnToMain()
    {
        mainButtons.enabled = true;
        levelPickButtons.enabled = false;
    }

    public void StartLevel(int levelNum)
    {
        string levelName = LEVEL_START_NAME + Convert.ToString(levelNum);
        SceneManager.LoadScene(levelName);
    }

    private void EnableLevelButtons()
    {
        string[] currentLevelSplit = PlayerDeathMenuController.GetCurrentLevel().Split(" ");
        int levelSplitNum = Convert.ToInt32(currentLevelSplit[1]);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i < levelSplitNum;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameScript : MonoBehaviour
{
    [SerializeField] private string firstLevelSceneName = "Level 1";
    public void WasClicked()
    {
        SceneManager.LoadScene(firstLevelSceneName); // note that when adding new scenes add them to the corretc area in build settings.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelZoneBehaviour : MonoBehaviour
{
    public string nextLevelName;
    public bool isFinalLevel = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*
            CameraFollow.instance.followPlayer = false;
            if(SceneManager.GetActiveScene().name == "Level 1")
            {
                SceneManager.LoadScene("Level 2");
            } else if(SceneManager.GetActiveScene().name == "Level 2")
            {
                SceneManager.LoadScene("Level 3");
            } else
            {
                PlayerDeathMenuController.instance.PlayerWon();
            }*/
            if (!isFinalLevel)
            {
                PlayerDeathMenuController.instance.StartLevelEnd(nextLevelName);
                //SceneManager.LoadScene(nextLevelName);
            }
            else
            {
                PlayerDeathMenuController.instance.PlayerWon();
            }
        }
    }
}

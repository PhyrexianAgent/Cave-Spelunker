using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainScript : MonoBehaviour
{
    public void WasClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

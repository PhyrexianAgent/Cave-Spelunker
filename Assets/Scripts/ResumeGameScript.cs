using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGameScript : MonoBehaviour
{
    public Canvas canvas;
    public void WasClicked()
    {
        Time.timeScale = 1;
        canvas.enabled = false;
    }
}

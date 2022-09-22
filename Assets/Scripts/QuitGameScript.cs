using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameScript : MonoBehaviour
{
    public void WasPressed()
    {
        Application.Quit(); // this will quite the game (when not run from editor).
        UnityEditor.EditorApplication.isPlaying = false; // Is used to quit game from editor. Can be removed if causes issues when compiled and ran.
    }
}

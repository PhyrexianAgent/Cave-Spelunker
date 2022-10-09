using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathMenuController : MonoBehaviour
{

    public static PlayerDeathMenuController instance;


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
}

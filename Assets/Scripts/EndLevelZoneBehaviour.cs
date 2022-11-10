using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelZoneBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CameraFollow.instance.followPlayer = false;
            PlayerDeathMenuController.instance.PlayerWon();
        }
    }
}

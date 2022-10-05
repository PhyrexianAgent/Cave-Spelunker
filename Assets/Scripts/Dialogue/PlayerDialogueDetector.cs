using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{
    float timer;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if(DialogueManager.instance.visible == false)
            {
                DialogueManager.instance.SetVisible(collision.gameObject);
                if (collision.gameObject.GetComponent<DialogueText>().lockPosition)
                {
                    LockPlayerPosition(collision.gameObject.GetComponent<DialogueText>().playTime);
                }
            }
        }
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            Player.instance.positionLocked = false;
            
        }
    }

    private void LockPlayerPosition(float playTime)
    {
        Player.instance.positionLocked = true;
        timer = playTime;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if (DialogueManager.instance.visible)
            {
                DialogueManager.instance.SetInvisible();
                if (collision.gameObject.GetComponent<DialogueText>().isDestroyable())
                {
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}

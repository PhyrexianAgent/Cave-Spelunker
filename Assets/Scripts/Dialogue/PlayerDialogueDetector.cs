using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{
    bool dialogEnded = false;
    float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if(DialogueManager.instance.visible == false)
            {
                DialogueManager.instance.SetVisible(collision.gameObject);
                timer = collision.gameObject.GetComponent<DialogueText>().playTime;
                dialogEnded = collision.gameObject.GetComponent<DialogueText>().playTime <= 0;
                Player.instance.ChangePlayerDialogLock(collision.gameObject.GetComponent<DialogueText>().lockPosition);
            }
        }
    }

    private void Update()
    {
        if (!dialogEnded) {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                
                Player.instance.ChangePlayerDialogLock(false);
                //Player.instance.positionLocked = false;
                DialogueManager.instance.SetInvisible();
                dialogEnded = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{
    float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if(DialogueManager.instance.visible == false)
            {
                DialogueManager.instance.SetVisible(collision.gameObject);
                timer = collision.gameObject.GetComponent<DialogueText>().playTime;
                Player.instance.positionLocked = collision.gameObject.GetComponent<DialogueText>().lockPosition;
            }
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Player.instance.positionLocked = false;
            DialogueManager.instance.SetInvisible();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if(DialogueManager.instance.visible == false)
            {
                DialogueManager.instance.SetVisible(collision.gameObject);
            }
        }
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

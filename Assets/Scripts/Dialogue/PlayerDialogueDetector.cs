using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{
    public static PlayerDialogueDetector instance;

    bool dialogEnded = false;
    float timer;

    private GameObject triggeredDialog;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if (!collision.gameObject.GetComponent<DialogueText>().triggered || !collision.gameObject.GetComponent<DialogueText>().isDestroyable())
            {
                StartCoroutine(TriggerDialog(collision.gameObject.GetComponent<DialogueText>()));
                collision.gameObject.GetComponent<DialogueText>().triggered = true;
            }
        }
    }

    public IEnumerator TriggerDialog(DialogueText obj)
    {
        Player.instance.ChangePlayerDialogLock(obj.lockPosition);
        DialogueManager.instance.skippable = obj.lockPosition;
        yield return new WaitForSeconds(obj.timeToStart);
        timer = obj.playTime;
        dialogEnded = obj.playTime <= 0;
        DialogueManager.instance.SetVisible(obj.gameObject);
        
        
        //if (obj.isDestroyable())
            //Destroy(obj.gameObject);
    }

    private void Update()
    {
        if (!dialogEnded) {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                DialogueManager.instance.SetInvisible();
                dialogEnded = true;
            }
        }
    }
}

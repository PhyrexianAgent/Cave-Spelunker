using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogueDetector : MonoBehaviour
{
    public static PlayerDialogueDetector instance;

    bool dialogEnded = false;
    float timer;

    private void Awake()
    {
        instance = this;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DialogueTrigger"))
        {
            if(DialogueManager.instance.visible == false)
            {
                StartCoroutine(TriggerDialog(collision.gameObject.GetComponent<DialogueText>()));
                //TriggerDialog(collision.gameObject.GetComponent<DialogueText>());
            }
        }
    }

    public IEnumerator TriggerDialog(DialogueText obj)
    {
        Player.instance.ChangePlayerDialogLock(obj.lockPosition);
        yield return new WaitForSeconds(obj.timeToStart);
        DialogueManager.instance.SetVisible(obj.gameObject);
        timer = obj.playTime;
        dialogEnded = obj.playTime <= 0;
        
        if (obj.isDestroyable())
            Destroy(obj.gameObject);
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

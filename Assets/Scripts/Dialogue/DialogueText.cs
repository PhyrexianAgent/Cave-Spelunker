using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{

    [SerializeField]
    private bool destroyAfterPlay = false;
    [TextArea(15, 20)]
    public string message;
    [SerializeField]
    public bool lockPosition = false;
    [SerializeField]
    public float playTime = 0.0f;

    public float timeToStart = 0;

    public bool triggered = false;

    public bool isDestroyable()
    {
        return destroyAfterPlay;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("entered dialog");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // if (destroyAfterPlay && timeToStart == 0)
        //{
            //Debug.Log(timeToStart);
          //  Destroy(gameObject);
        //}
    }
}

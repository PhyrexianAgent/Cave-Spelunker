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

    public bool isDestroyable()
    {
        return destroyAfterPlay;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyAfterPlay)
        {
            Destroy(gameObject);
        }
    }
}

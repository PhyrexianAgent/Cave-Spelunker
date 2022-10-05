using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText : MonoBehaviour
{

    [SerializeField]
    private bool destroyAfterPlay = false;
    public string message;

    public bool isDestroyable()
    {
        return destroyAfterPlay;
    }
}

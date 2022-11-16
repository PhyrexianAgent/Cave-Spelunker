using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    private void Awake()
    {
        instance = this;
    }
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    Image backdrop;
    public bool visible = false;
    public bool skippable = false;
    char[] displayText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && visible && skippable)
        {
            SetInvisible();
        }
    }

    public void SetVisible(GameObject trigger)
    {
        this.visible = true;
        text.gameObject.SetActive(visible);
        backdrop.gameObject.SetActive(visible);
        if (visible)
        {
            text.text = "";
        }
        //Debug.Log();
        StartCoroutine(DisplayText(trigger.gameObject.GetComponent<DialogueText>().message));
    }

    public void SetInvisible()
    {
        StopAllCoroutines();
        this.visible = false;
        text.text = "";
        text.gameObject.SetActive(visible);
        backdrop.gameObject.SetActive(visible);
        Player.instance.ChangePlayerDialogLock(false);
        //Debug.Log("killing dialog");
    }

    IEnumerator DisplayText(string info)
    {
        displayText = info.ToCharArray();
        for (int z = 0; z < displayText.Length; z++)
        {
            text.text += displayText[z];
            yield return new WaitForSeconds(0.05f);
        }
    }
}

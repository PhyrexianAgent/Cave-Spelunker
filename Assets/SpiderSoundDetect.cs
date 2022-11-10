using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSoundDetect : MonoBehaviour
{
    public SpiderController controller;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            controller.BeginDescent();
        }
    }

    private void BeginDescent()
    {

    }
}

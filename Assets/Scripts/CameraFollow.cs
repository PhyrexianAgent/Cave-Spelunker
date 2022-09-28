using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    //public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
            Vector3 temp = transform.position;
            temp.x = playerTransform.position.x;
            transform.position = temp;
            temp.y = playerTransform.position.y;
            transform.position = temp;
    }
}

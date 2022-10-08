using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling2 : MonoBehaviour
{
    [SerializeField]
    Transform falling1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            other.gameObject.transform.position = falling1.transform.position;
        }
    }
}

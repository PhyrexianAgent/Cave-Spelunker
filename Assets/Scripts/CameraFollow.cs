using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform playerTransform;
    //public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -1); // have to set z to -1 becuase unity is not a 2d game engine. It runs 2d game in a 3d space (which I despise)
    }
}

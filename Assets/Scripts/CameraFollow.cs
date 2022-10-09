using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    public Transform player;

    public bool followPlayer = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
            transform.position = new Vector3(player.position.x, player.position.y, -1); // have to set z to -1 becuase unity is not a 2d game engine. It runs 2d game in a 3d space (which I despise)
    }
}

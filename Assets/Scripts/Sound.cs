using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private bool isQuietSound;

    private float damage = 0;

    public void GenerateSound(float damage, float radius, bool isQuietSound = false)
    {
        CircleCollider2D coll = GetComponent<CircleCollider2D>();
        this.damage = damage;
        this.isQuietSound = isQuietSound;
        coll.radius = radius;
        coll.enabled = true;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isQuietSound)
        {
            DoTriggerActions(collision.tag, collision.gameObject);
        }
        else
        {
            DoQuietActions(collision.tag, collision.gameObject);
        }
        
    }

    private void DoQuietActions(string tag, GameObject obj)
    {
        switch (tag)
        {
            case "Bat Listener":
                obj.GetComponent<BatListenerController>().owner.HeardquietSound(damage);
                break;
        }
    }

    private void DoTriggerActions(string tag, GameObject obj)
    {
        switch (tag)
        {
            case "Stalactite":
                obj.GetComponent<StalactiteBehaviour>().TakeDamage(damage);
                break;
            case "Spider Listener":
                if (!obj.GetComponent<SpiderSoundDetect>().controller.CompareState(SpiderController.SpiderState.Dropping))
                    obj.GetComponent<SpiderSoundDetect>().controller.BeginDescent();
                break;
            case "Bat Listener":
                if (obj.GetComponent<BatListenerController>().Awakenable())
                    obj.GetComponent<BatListenerController>().AwakenBat();
                break;
        }
    }
}

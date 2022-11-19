using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteBehaviour : MonoBehaviour
{
    private const float HEALTH_MAX = 10;

    
    [SerializeField] private float fallGravity = 1;

    [SerializeField] private Sounds[] sounds;

    private float health = HEALTH_MAX;
    private bool isFalling = false;
    void Start()
    {
        SetSoundsStartVals();
    }

    private void SetSoundsStartVals()
    {
        foreach (Sounds sound in sounds)
        {
            sound.source = AudioManager.instance.gameObject.AddComponent<AudioSource>();
            sound.SetSource();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(HEALTH_MAX);
        }*/
    }

    public void TakeDamage(float damage)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        health -= damage;
        if (health <= 0)
        {
            isFalling = true;
            body.constraints = RigidbodyConstraints2D.None;
            body.gravityScale = fallGravity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFalling)
        {
            
            Destroy(gameObject);
            PlayRandSound();
            //AudioManager.instance.Play("Stalactite");
        }
    }

    private void PlayRandSound()
    {
        int randNum = Random.Range(0, sounds.Length);
        sounds[randNum].source.Play();
        Debug.Log(sounds[randNum].volume);
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }
}

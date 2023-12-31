
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMushroomPowerup : BasePowerup
{
    // setup this object's type
    // instantiate variables
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.MagicMushroom;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && spawned)
        {
            // 2x score multiplier
            GameManager.instance.scoreMultiplier = 2;

            StartCoroutine(PlayAudioThenDestroy());



        }
        else if (col.gameObject.layer == 10) // else if hitting Pipe, flip travel direction
        {
            if (spawned)
            {
                goRight = !goRight;
                rigidBody.AddForce(Vector2.right * 3 * (goRight ? 1 : -1), ForceMode2D.Impulse);

            }
        }
    }
    private IEnumerator PlayAudioThenDestroy()
    {
        AudioSource powerupAudio = gameObject.GetComponent<AudioSource>();
        powerupAudio.PlayOneShot(powerupAudio.clip);
        yield return new WaitUntil(() => !powerupAudio.isPlaying);
        DestroyPowerup();

    }
    // interface implementation
    public override void SpawnPowerup()
    {
        spawned = true;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        rigidBody.AddForce(Vector2.right * 3, ForceMode2D.Impulse); // move to the right
    }


    // interface implementation
    public override void ApplyPowerup(MonoBehaviour i)
    {
        // TODO: do something with the object
        Debug.Log("collide");
        // DestroyPowerup();

    }
}
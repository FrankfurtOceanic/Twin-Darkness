using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;


    public AudioSource SoundSource;
    public AudioClip HitSound;
    public AudioClip DeathSound;
    [Range(0f, 5f)]
    public float volume =1f;
    public float delay = 0.05f;

    public void TakeDamage(float DamageAmount)
    {
        
        Health -= DamageAmount;
        if (Health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(delaySound(delay, HitSound));
        }
    }

    private void Die()
    {
        StartCoroutine(delaySound(delay, DeathSound));
        Destroy(gameObject, delay+0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delaySound(float seconds, AudioClip AC)
    {
        yield return new WaitForSeconds(seconds);
        SoundSource.PlayOneShot(AC, volume);
    }

    IEnumerator delaySoundAtPoint(float seconds, AudioClip AC)
    {
        yield return new WaitForSeconds(seconds);
        AudioSource.PlayClipAtPoint(AC, transform.position, volume);
    }

}

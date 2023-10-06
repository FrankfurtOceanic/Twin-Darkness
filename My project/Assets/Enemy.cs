using Q3Movement;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 50f;
    protected float initialHealth;


    public AudioSource SoundSource;
    public AudioClip HitSound;
    public AudioClip DeathSound;
    [Range(0f, 5f)]
    public float volume =1f;
    public float delay = 0.05f;

    public float points = 100f;  //points added to score on kill
    public float gaugePercent = 0.1f; //percentage of gauge added to berserk meter
    public Q3PlayerController player;


    public void Start()
    {
        initialHealth = Health;
        player = GameManager.Player.GetComponent<Q3PlayerController>();
    }

    public virtual void TakeDamage(float DamageAmount)
    {
        
        Health -= DamageAmount;
        if (Health <= 0)
        {
            player.EnemyKilled(gaugePercent, points);
            Die();
        }
        else
        {
            StartCoroutine(delaySound(delay, HitSound));
        }
    }

    protected void Die()
    {
        StartCoroutine(delaySound(delay, DeathSound));
        Destroy(gameObject, delay+0.5f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Q3PlayerController>().hit();
            Die();
        }
    }


    protected IEnumerator delaySound(float seconds, AudioClip AC)
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

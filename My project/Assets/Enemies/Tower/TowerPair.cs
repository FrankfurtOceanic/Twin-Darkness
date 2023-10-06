using Q3Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPair : MonoBehaviour
{
    [SerializeField] float slowPercent = -0.1f;
    [SerializeField] ParticleSystem hitEffect = null;
    private Collider BarrierCollider;
    private LineRenderer BarrierLR;

    [SerializeField] private TowerIndividual Tow1;
    [SerializeField] private TowerIndividual Tow2;

    public AudioSource SoundSource;
    public AudioClip DeathSound;
    [Range(0f, 5f)]
    public float volume = 1f;
    public float delay = 0.05f;

    public float points = 100f;  //points added to score on kill
    public float gaugePercent = 0.1f; //percentage of gauge added to berserk meter
    public Q3PlayerController player;

    private bool BarrierActive = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Player.GetComponent<Q3PlayerController>();
        BarrierCollider = GetComponent<Collider>();
        BarrierLR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Tow1.incapacitated && Tow2.incapacitated)
        {
            Die();
        }
        if(Tow1.incapacitated || Tow2.incapacitated)
        {
            if (BarrierActive)
            {
                DisableWall();
            }
            
        }
        else if (!BarrierActive)
        {
            ActivateWall();
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        
        if (trigger.gameObject.tag == "Player")
        {
            trigger.gameObject.GetComponent<Q3PlayerController>().slow(slowPercent);

            //play particle effect
            if(hitEffect != null)
            {
                Instantiate(hitEffect, trigger.transform.position, Quaternion.identity, transform);
            }
        }
    }

    private void ActivateWall()
    {
        BarrierActive = true;
        BarrierCollider.enabled = true;
        BarrierLR.enabled = true;

    }

    private void DisableWall()
    {
        BarrierActive = false;
        BarrierLR.enabled = false;
        BarrierCollider.enabled = false;
    }
    protected void Die()
    {
        StartCoroutine(delaySound(delay, DeathSound));
        Destroy(gameObject, delay + 0.5f);
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

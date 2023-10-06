using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIndividual : Enemy
{
    // Start is called before the first frame update
    public bool incapacitated;
    public Animator anim;
    private AnimationClip respawnClip;
    private MeshRenderer meshRenderer;
    public float respawnSeconds = 5f;

    private void Start()
    {
        base.Start();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void TakeDamage(float DamageAmount)
    {

        Health -= DamageAmount;
        if (Health <= 0)
        {
            player.EnemyKilled(gaugePercent, points);
            Incapacitate();
        }
        else
        {
            StartCoroutine(delaySound(delay, HitSound));
        }
    }

    private void Incapacitate()
    {
        incapacitated= true;
        meshRenderer.enabled = false;
        Debug.Log("Tower Executed");
        StartCoroutine(Respawn(respawnSeconds));
    }

    private void Outcapacitate()
    {
        incapacitated = false;
        meshRenderer.enabled = true;
        Health = initialHealth;
        Debug.Log("Tower Active");
    }

    private IEnumerator Respawn(float respawnSeconds)
    {
        anim.SetTrigger("Respawn");
        yield return new WaitForSeconds(respawnSeconds);
        Outcapacitate();
    }

}

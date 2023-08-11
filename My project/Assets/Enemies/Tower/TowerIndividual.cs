using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerIndividual : Enemy
{
    // Start is called before the first frame update
    private TowerPair tp;
    public bool incapacitated;

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
    }
}

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

    private bool BarrierActive;
    // Start is called before the first frame update
    void Start()
    {
        BarrierCollider = GetComponent<Collider>();
        BarrierLR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        BarrierCollider.enabled = true;
        BarrierLR.enabled = true;

    }

    private void DisableWall()
    {
        BarrierLR.enabled = true;
        BarrierCollider.enabled = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gauge : MonoBehaviour
{
    [SerializeField] private float GaugeAmount = 100f;
    [Range(0f, 1f)]
    [SerializeField] private float startPercentage = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float decayPercentRate = 0.016666f; //defaut value of 1.6666% results in a full decay in 60 seconds

    private float currentAmount;

    // Start is called before the first frame update
    void Start()
    {
        currentAmount = startPercentage * GaugeAmount;
    }

    // Update is called once per frame
    void Update()
    {
        //continuing decay
        currentAmount -= decayPercentRate * GaugeAmount * Time.deltaTime;
        currentAmount = Mathf.Max(currentAmount, 0);
        Debug.Log("GaugeAmount = " + currentAmount);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="frac">Fraction of the gauge to add. Can be negative to remove</param>
    public void addPercentage(float frac)
    {
        currentAmount += frac * GaugeAmount;
    }
}

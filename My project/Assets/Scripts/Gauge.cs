using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField] private float GaugeMaximum = 100f;
    [Range(0f, 1f)]
    [SerializeField] private float startPercentage = 0.5f;
    [Range(0f, 1f)]
    [SerializeField] private float decayPercentRate = 0.016666f; //defaut value of 1.6666% results in a full decay in 60 seconds
    [SerializeField] private Image bar;

    [SerializeField] private float fullThreshold = 0.99f;

    private float currentAmount;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        //continuing decay
        currentAmount -= decayPercentRate * GaugeMaximum * Time.deltaTime;
        currentAmount = Mathf.Max(currentAmount, 0);
        Debug.Log("GaugeAmount = " + currentAmount);
        bar.fillAmount = currentAmount / GaugeMaximum;
    }

    /// <summary>
    /// Adds a percentage of the total gauge to the current value
    /// </summary>
    /// <param name="frac">Fraction of the gauge to add. Can be negative to remove</param>
    public void addPercentage(float frac)
    {
        currentAmount = Mathf.Min(currentAmount + frac * GaugeMaximum, GaugeMaximum);
    }

    public void addAmount(float amount)
    {
        currentAmount = Mathf.Min(currentAmount + amount, GaugeMaximum);
    }

    public void Set(float total, float percent)
    {
        currentAmount = total * percent;
        GaugeMaximum = total;
    }

    public bool isFull()
    {
        Debug.Log("GaugeAmount = " + currentAmount);
        return (currentAmount / GaugeMaximum) > fullThreshold;
    }

    /// <summary>
    /// Resets the current gauge amount to the default percentage.
    /// </summary>
    public void Reset()
    {
        currentAmount = startPercentage * GaugeMaximum;
    }
}

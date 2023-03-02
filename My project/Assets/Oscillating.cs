using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillating : MonoBehaviour
{
    public float offset=0;
    public float speed=1;
    public float amplitude=1;
    public float rotAmplitude = 180;
    private Vector3 initialPos;
    public bool Rotation = false;
    public float degreesPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float heightChange = initialPos.y + amplitude * Mathf.Sin(offset + Time.time * speed);
        transform.position = new Vector3(initialPos.x, heightChange, initialPos.z);

        if (Rotation)
        {
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        }
    
    }
}

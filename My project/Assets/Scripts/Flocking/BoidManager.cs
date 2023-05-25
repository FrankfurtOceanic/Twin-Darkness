using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    public List<GameObject> Boids = new List<GameObject>();

    [SerializeField] private GameObject prefab;

    //parameters
    [Range(0f, 1f)]
    public float sepRadiusCoef = 1;
    [Range(0f, 5f)]
    public float sepStrength = 1;
    [Range(0f, 5f)]
    public float aliStrength = 1;
    [Range(0f, 5f)]
    public float cohStrength = 1;
    [Range(0f, 15f)]
    public float targetStrength = 1;

    [Range(0f, 10f)]
    public float rangeRadius = 3;

    [Range(0.01f, 50f)]
    [SerializeField] public float speed = 1;
    [Range(0.01f, Mathf.PI*10)]
    public float rotationSpeed = Mathf.PI / 4;

    public Transform target; //Where the boids should go to
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            spawnBoid();
        }

        foreach (GameObject boid in Boids)
        {
            boid.GetComponent<Boid>().CalcNextDirection();
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject boid in Boids)
        {
            Vector3 dir = boid.GetComponent<Boid>().TransferDirection();
            boid.GetComponent<Boid>().moveBoid();
            //boid.transform.position += dir * speed * Time.deltaTime;

            // Draw a ray pointing at our target in
            Debug.DrawRay(boid.transform.position, dir, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            boid.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    void spawnBoid()
    {
        GameObject boid = Instantiate(prefab, transform.position, Quaternion.identity);
        boid.AddComponent<Boid>();
        boid.GetComponent<Boid>().BM = this;

        Boids.Add(boid);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public BoidManager BM;
    

    public Vector3 direction;
    private Vector3 nextDirection;



    // Start is called before the first frame update
    private void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    public void CalcNextDirection()
    {
        //Calculate 
        Vector3 separate = Vector3.Normalize(Separation());
        Vector3 align = Vector3.Normalize(Align());
        Vector3 cohesion = Vector3.Normalize(Cohesion());
        Vector3 targetDir = Vector3.Normalize(Target());


        Debug.DrawRay(transform.position, separate, Color.magenta);
        Debug.DrawRay(transform.position, align, Color.blue);
        Debug.DrawRay(transform.position, cohesion, Color.green);

        nextDirection = Vector3.zero;
        nextDirection += separate * BM.sepStrength;
        nextDirection += align * BM.aliStrength;
        nextDirection += cohesion * BM.cohStrength;
        nextDirection += targetDir * BM.targetStrength;

        nextDirection = Vector3.Normalize(nextDirection);

    }

    public Vector3 TransferDirection()
    {
        direction = Vector3.RotateTowards(transform.forward, nextDirection, BM.rotationSpeed * Time.deltaTime, 0.0f);
        return direction;
    }


    private Vector3 Separation()
    {

        Vector3 sumDif = Vector3.zero;
        foreach (GameObject boid in BM.Boids)
        {
            if (boid != this)
            {
                if (Vector3.Distance(transform.position, boid.transform.position) <= BM.rangeRadius * BM.sepRadiusCoef)
                {
                    sumDif += transform.position - boid.transform.position;
                }
            }
        }
        return sumDif;
    }

    private Vector3 Align()
    {
        Vector3 avgDir = Vector3.zero;
        foreach (GameObject boid in BM.Boids)
        {
            if (boid != this)
            {
                if (Vector3.Distance(transform.position, boid.transform.position) <= BM.rangeRadius)
                {
                    avgDir += boid.GetComponent<Boid>().direction;
                }
            }
        }
        return avgDir;
    }

    private Vector3 Cohesion()
    {
        Vector3 avgPos = Vector3.zero;
        int count = 0;
        foreach (GameObject boid in BM.Boids)
        {
            if(boid != this)
            {
                if (Vector3.Distance(transform.position, boid.transform.position) <= BM.rangeRadius)
                {
                    avgPos += boid.transform.position;
                    count++;
                }
            }
        }

        if(count == 0)
        {
            return avgPos; //return vec3 0 if there are no nearby boids
        }

        else
        {
            return avgPos / count - transform.position; //if there are nearby boids, return the direction from the boid to the center of neighbors.
        }
        
    }

    private Vector3 Target()
    {
        if (BM.target != null)
        {
            return BM.target.position - transform.position;
        }

        else
        {
            return Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        BM.Boids.Remove(gameObject);
    }


}

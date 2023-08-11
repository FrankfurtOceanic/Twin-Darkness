using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public BoidManager BM;
    

    public Vector3 direction;
    private Vector3 nextDirection;
    private Rigidbody rb;



    // Start is called before the first frame update
    private void Start()
    {
        direction = transform.forward;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void CalcNextDirection()
    {
        //Calculate 
        Vector3 separate = Separation();
        Vector3 align = Vector3.Normalize(Align());
        Vector3 cohesion = Vector3.Normalize(Cohesion());
        Vector3 targetDir = Vector3.Normalize(Target());


        Debug.DrawRay(transform.position, separate * BM.sepStrength, Color.magenta);
        Debug.DrawRay(transform.position, align * BM.aliStrength, Color.blue);
        Debug.DrawRay(transform.position, cohesion * BM.cohStrength, Color.green);
        Debug.DrawRay(transform.position, targetDir * BM.targetStrength, Color.cyan);

        nextDirection = Vector3.zero;
        nextDirection += separate * BM.sepStrength;
        nextDirection += align * BM.aliStrength;
        nextDirection += cohesion * BM.cohStrength;
        nextDirection += targetDir * BM.targetStrength;

        nextDirection = Vector3.Normalize(nextDirection);
        Debug.DrawRay(transform.position, targetDir, Color.yellow);


    }

    public Vector3 TransferDirection()
    {
        //direction = Vector3.RotateTowards(transform.forward, nextDirection, BM.rotationSpeed, 0.0f);
        //direction = Vector3.Slerp(transform.forward, nextDirection, BM.rotationSpeed);
        return nextDirection;
    }


    private Vector3 Separation()
    {

        Vector3 sumDif = Vector3.zero;
        int count = 0;
        foreach (GameObject boid in BM.Boids)
        {
            if (boid != gameObject)
            {
                if (Vector3.Distance(transform.position, boid.transform.position) <= BM.rangeRadius * BM.sepRadiusCoef)
                {
                    Vector3 dif = transform.position - boid.transform.position;
                    sumDif += dif * (1 - dif.magnitude/(BM.rangeRadius* BM.sepRadiusCoef));
                    count++;
                }
            }
        }
        if(count > 0 )
        {
            return sumDif / count;
        }

        return sumDif;
    }

    private Vector3 Align()
    {
        Vector3 avgDir = Vector3.zero;
        foreach (GameObject boid in BM.Boids)
        {
            if (boid != gameObject)
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
            if(boid != gameObject)
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

    public void moveBoid()
    {
        direction = transform.forward;
        rb.velocity = direction * BM.speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, BM.rangeRadius);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, BM.rangeRadius * BM.sepRadiusCoef);
    }


}

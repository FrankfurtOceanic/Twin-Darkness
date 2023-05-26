using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CircleRolling : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    [SerializeField] private float degPerSec;

    private float zOffset;
    private Rigidbody rb;
    private float angleOffset;
    private int ccw = 1;

    private Vector3 cross;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        zOffset = transform.position.z;


        //determine if object should rotate clockwise or counter clockwise

        cross = Vector3.Cross(transform.position - center.position, transform.up);
        if (Vector3.SignedAngle(cross, transform.forward, transform.up) > 90)
        {
            ccw = -1; //if the angle is opposing the cross product, rotate in the other way
        }


    }
    private void Update()
    {
        transform.RotateAround(center.position, Vector3.down, degPerSec * Time.deltaTime * ccw);
    }

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(center.position, Vector3.right * radius * ccw);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + cross);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class Camera_Snapping : MonoBehaviour
{
    public float height_pixels = 16;

    public float x_unit, y_unit;

    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        RotationChange();
        cam = GetComponent<Camera>();
        
    }

    void Update()
    {
        transform.position = new Vector3(Snap(transform.position.x, true), transform.position.y, Snap(transform.position.z, false));
    }

    /// <summary>
    /// Returns the 
    /// </summary>
    /// <param name="value"> The value to be snapped</param>
    /// <param name="x"> whethere or not this is for the x component of the transform of the y component</param>
    /// <returns></returns>
    public float Snap(float value, bool x)
    {
        float units = x ? x_unit : y_unit;
       
        return Mathf.Floor(value/units) * units;
    }

    public void RotationChange()
    {
        CalculateUnitLength(true);
        CalculateUnitLength(false);

    }

    void CalculateUnitLength(bool x)
    {
        if (x)
        {

            float angle = Mathf.Cos(Mathf.PI / 4 - transform.rotation.eulerAngles.y);



            x_unit = (cam.orthographicSize * 2 / height_pixels) / (angle != 0 ? angle : 1) ;
        }

        else
        {
            y_unit = (cam.orthographicSize * 2 / height_pixels) / Mathf.Cos(Mathf.PI / 4 -transform.rotation.eulerAngles.x);
        }
    }

}

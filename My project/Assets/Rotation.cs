using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            //m_MoveInput.x // [-1, 1]
            //m_Camera.transform.rotation

            float zRotTarget = Input.GetAxisRaw("Horizontal") * -45
                ;
            //float zRot = Mathf.SmoothDamp(camera.localRotation.eulerAngles.z, zRotTarget, ref tiltVelocity, 0.3f);
            Quaternion targetRotation = Quaternion.Euler(this.transform.localRotation.eulerAngles.x,
                this.transform.localRotation.eulerAngles.y,
                zRotTarget);

            Debug.Log(targetRotation.eulerAngles);
            //Debug.Log(m_Camera.transform.localRotation);

            this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, targetRotation, 1 * Time.deltaTime);
    }
}

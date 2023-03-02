using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1f;
    public float rotationSpeed = 1f;
    public ParticleSystem particle;
    public float particleAngleTrigger = 60f;
    private CharacterController CharController;
    void Start()
    {
        CharController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalIn = Input.GetAxis("Horizontal");
        float verticalIn = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontalIn, 0, verticalIn);
        float magnitude = Mathf.Clamp01(moveDir.magnitude) * speed;
        moveDir.Normalize();

        CharController.SimpleMove(moveDir * magnitude);
        
        if(moveDir != Vector3.zero)
        {

            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);

            if (Mathf.Abs(Quaternion.Angle(transform.rotation, toRotation)) > particleAngleTrigger) playEffect(); 

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void playEffect()
    {
        particle.Play();
    }
}

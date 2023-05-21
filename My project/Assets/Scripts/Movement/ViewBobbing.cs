using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(PosFollow))]

public class ViewBobbing : MonoBehaviour
{
    [SerializeField] private float YIntensity;
    [SerializeField] private float XIntensity;

    [SerializeField] private float Frequency;
    [SerializeField] private float SettleSpeed;
    [SerializeField] private CharacterController cc;

    private PosFollow FollowInstance;
    private Vector3 OriginOffset;
    private float SinTime;
    // Start is called before the first frame update
    void Start()
    {
        FollowInstance = GetComponent<PosFollow>();
        OriginOffset = FollowInstance.Offset;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));

        if (cc.isGrounded)
        {

            if (input.magnitude > 0f)
            {
                SinTime += Time.deltaTime * Frequency;
                SinTime -= 2 * Mathf.PI * Mathf.Floor(SinTime / (2 * Mathf.PI));
                float sinAmountY = -Mathf.Abs(YIntensity * Mathf.Sin(SinTime));
                Vector3 sinAmountX = FollowInstance.transform.right * XIntensity * Mathf.Cos(SinTime);

                FollowInstance.Offset = new Vector3
                {
                    x = OriginOffset.x,
                    y = OriginOffset.y + sinAmountY,
                    z = OriginOffset.z
                };

                FollowInstance.Offset += sinAmountX;
            }
            else
            {
                SinTime = Mathf.Lerp(SinTime / (2 * Mathf.PI), Mathf.Round(SinTime / (2 * Mathf.PI)), Time.deltaTime * SettleSpeed);
                FollowInstance.Offset = Vector3.Lerp(FollowInstance.Offset, Vector3.zero, Time.deltaTime * SettleSpeed);
            }
        }
        
    }
}

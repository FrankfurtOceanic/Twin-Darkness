using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFPSControl : MonoBehaviour
{
    [SerializeField] private  float fps = 24;
    private Animator animator;
    private bool playing;
    private float timeSinceLastFrame = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        animator.StopPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.StopPlayback();
        timeSinceLastFrame += Time.deltaTime;
        if (timeSinceLastFrame > 1 / fps)
        {
            animator.enabled = true;
            animator.Update(timeSinceLastFrame);
            animator.enabled = false;
            
            timeSinceLastFrame = 0;
        }


    }
}

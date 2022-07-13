using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimScript : MonoBehaviour
{
    Animator animator;
    public GameObject cameraobj;
    Animator camAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) Debug.LogError("Animator Component Not Found!");

        if (cameraobj == null) Debug.LogError("Camera not foudn!");
        camAnimator = cameraobj.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null)
        {
            // sprinting
            float ShiftClicked = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Fire3");
            float Horizontal = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Horizontal");
            float Vertical = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Vertical");
            float RightClick = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Fire2");
            float Reload = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Reload");


            if (Reload > 0)
            {

                animator.SetBool("isADS", false);
                camAnimator.SetBool("isADS", false);

                animator.SetBool("isRunning", false);
                camAnimator.SetBool("isRunning", false);

            }
            if (ShiftClicked > 0 && (Horizontal != 0 || Vertical != 0))
            {
                if (RightClick > 0)
                {
                    animator.SetBool("isADS", true);
                    animator.SetBool("isRunning", false);

                    camAnimator.SetBool("isADS", true);
                    camAnimator.SetBool("isRunning", false);
                }
                else
                {
                    animator.SetBool("isRunning", true);
                    camAnimator.SetBool("isRunning", true);

                    animator.SetBool("isADS", false);
                    camAnimator.SetBool("isADS", false);
                }
            }
            else
            {
                animator.SetBool("isRunning", false);
                camAnimator.SetBool("isRunning", false);
            }

            // ADSing
            if (RightClick > 0)
            {
                //animator.SetBool("isADS", true);
                //animator.SetBool("isRunning", false);

                //camAnimator.SetBool("isADS", true);
                //camAnimator.SetBool("isRunning", false);
            }
            else
            {
                //animator.SetBool("isADS", false);
                //camAnimator.SetBool("isADS", false);
            }



        }
    }

    public void OnStartFiring()
    {

    }

    public void OnStopFiring()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PlayerPhysicalMovement : MonoBehaviour
{
//--------------------------------------------------------------
// SUMMARY
// Handles:
//  - Player WASD
//  - Jumping


//----------------------------------------
// INSPECTOR Attributes

    [Header("Object References")]
    public Transform head;
    public Transform upperbody;
    public Transform lowerbody;
    


    [Space(10)]
    [Header("Public Properties")]
    public float walkSpeedMultiplier;
    public float airWalkSpeedMultiplier;
    public float crouchSpeedMultiplier = 0.3f;
    public float sprintSpeedMultiplier = 0.5f;
    public float slowdownMultipler;
    public float jumpHeight = 5f;
    
    [Space(10)]
    [Header("Debug Properties")]
    public float Speed;
    public float MaxSpeed;
    public bool isSprinting;
    public bool isJumping;
    public bool isCrouching;


    //----------------------------------------
    // PRIVATE
    private float movementMultiplier = 1;
    public Vector3 Pos { get { return transform.position; } }
    public Quaternion RotationXYZ { get { return head.rotation; } }
    public Quaternion RotationXZ { get { return transform.rotation; } }

    float vertical;
    float horizontal;
    float axisMagnitude;
    float jump;
    float crouch;
    float sprint;
    bool isGrounded;
    Rigidbody rb;

    //----------------------------------------
    // PUBLIC FIELDS
    public float MovementMultiplier
    {
        get { return movementMultiplier; }
        set { movementMultiplier = Mathf.Max(0, value); }
    }
    public bool IsGrounded { get { return isGrounded; } }

//----------------------------------------------------
// METHODS

//----------------------------------------
// Start is called before the first frame update
    void Start()
    {
    // Defining Parts of Body
        this.FindObject(ref head, "Head");
        this.FindObject(ref upperbody, "Upper Abdoment");
        this.FindObject(ref lowerbody, "Lower Abdoment");

    // Getting this body's rigidbody.
        rb = this.FindComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);

    // Defining Quick Transform Readonly
    }



//--------------------------------------------------------------
// Update is called once per frame
    void Update()
    {

    //----------------------------------------------
    // Retrieving Input
        vertical = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Vertical");
        horizontal = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Horizontal");
        axisMagnitude = a.GetMaxAxis(horizontal, vertical);
        jump = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Jump");
        crouch = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Crouch");
        sprint = PauseMenu.GameIsPaused ? 0f : Input.GetAxis("Fire3");
        MaxSpeed = walkSpeedMultiplier/slowdownMultipler;

    //----------------------------------------------
    // Checking if Grounded
        (_, isGrounded) = this.CheckGrounded(
            origin: Pos + Vector3.up * 0.1f,
            direction: Vector3.down);

    //----------------------------------------------
    // Applying Movement Forces
        Vector3 moveDirection = RotationXZ * new Vector3(horizontal, 0, vertical).normalized;
        Vector3 moveForce = moveDirection * axisMagnitude;
        Debug.DrawRay(transform.position, moveForce * 5);

        if (sprint == 0) isSprinting = false;
        if (crouch != 1) isCrouching = false;

        if (isGrounded)
        {

        // Check for Sprinting / Crouching
            if(sprint > 0 && !isCrouching)
            {
                isSprinting = true;
                movementMultiplier = sprintSpeedMultiplier * sprint;
            }


            if(crouch > 0 && !isSprinting)
            {
                isCrouching = true;
                movementMultiplier = crouchSpeedMultiplier * crouch;
            }

        // Reset Position if not both
            if(!isCrouching && !isSprinting)
            {
                isCrouching = false;
                movementMultiplier = 1f;
            }

        // apply ground movement
            if(!PauseMenu.GameIsPaused) rb.AddForce(moveForce * walkSpeedMultiplier * movementMultiplier);

        // if jump, then apply upward velocity.
            if (jump > 0 && !isJumping)
            {
                Debug.Log("Jumped");
                rb.AddForce(
                    x: 0,
                    y: -rb.velocity.y + jumpHeight * jump,
                    z: 0,
                    mode: ForceMode.VelocityChange);
                isJumping = true;
            }


        // apply friction
            if(!PauseMenu.GameIsPaused) 
                rb.AddForce(
                    x: -rb.velocity.x * slowdownMultipler,
                    y: 0,
                    z: -rb.velocity.z * slowdownMultipler);

        }
        else
        {
        // apply air movement
            if(!PauseMenu.GameIsPaused) 
                rb.AddForce(moveForce * airWalkSpeedMultiplier);

        // apply air friction
            Vector3 groundSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            if(!PauseMenu.GameIsPaused) 
                if (groundSpeed.magnitude >= MaxSpeed){
                    groundSpeed = groundSpeed.normalized * MaxSpeed;
                    //Debug.Log("Slow Down!");
                    rb.velocity = new Vector3(
                        x: groundSpeed.x,
                        y: rb.velocity.y,
                        z: groundSpeed.z);
            }

        // Reset Jumping Status if Falling
            if (rb.velocity.y <= 0 && isJumping)  isJumping = false;
        }

        // Document the speed.
        Speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;

    //----------------------------------------------
    // Animate Body + Rotate Body + (To-do: GUN)
        upperbody.localRotation = Quaternion.Euler(
            x: 30 * crouch, 
            y: horizontal * 20, 
            z: 0);
        upperbody.localPosition = new Vector3(
            x: 0, 
            y: 1.5f - (1.5f - 1.3f) * crouch,
            z: -0.25f * crouch);

        lowerbody.localRotation = Quaternion.Euler(
            x: -30 * crouch, 
            y: horizontal * 20, 
            z: 0);
        lowerbody.localPosition = new Vector3(
            x: 0, 
            y: 0.5f - (0.5f - 0.45f) * crouch, 
            z: -0.25f * crouch);

        head.localPosition = new Vector3(
            x: 0, 
            y: 2f - (2f - 1.75f) * crouch, 
            z: 0);
    }
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))] // enforces dependency on character controller
[AddComponentMenu("Control Script/FPS Input")]  // add to the Unity editor's component menu
public class FPSInput : MonoBehaviour
{
    private Animator anim;
    private bool isCurrentlySleeping = false;
    public float runSpeed = 9.0f;  

    public float jumpSpeed = 15.0f;
    public float terminalVelocity = -20.0f;
    private float vertSpeed;
    // movement sensitivity
    public float speed = 6.0f;
    // sitting
    private bool isCurrentlySitting = false;
    // gravity setting 
    public float gravity = -9.8f;
    private bool isCurrentlyEating = false;

    // reference to the character controller
    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        // get the character controller component
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = speed;  // Default to walking speed
        anim.SetBool("IsRunning", false);  // Default to not running

        // Check if the player is moving forward (using W key or UpArrow) and holding down the shift key
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed;  // Set speed to running speed
            anim.SetBool("IsRunning", true);  // Trigger running animation
        }

        float deltaX = Input.GetAxis("Horizontal") * currentSpeed;
        float deltaZ = Input.GetAxis("Vertical") * currentSpeed;

   
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        // Rotate character based on A or D key
        float rotationSpeed = 50.0f; 
        if (Mathf.Abs(deltaX) > 0.1f)
        {
            transform.Rotate(0, deltaX * rotationSpeed * Time.deltaTime, 0);
        }


        // sleeping
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCurrentlySleeping)
            {
                // Wake up from sleep
                anim.SetBool("IsSleeping", false);
                isCurrentlySleeping = false;
            }
            else
            {
                // Go to sleep
                anim.SetBool("IsSleeping", true);
                isCurrentlySleeping = true;
            }
        }

        // sitting
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isCurrentlySitting)
            {
                // Wake up from sleep
                anim.SetBool("IsSitting", false);
                isCurrentlySitting = false;
            }
            else
            {
                // Go to sleep
                anim.SetBool("IsSitting", true);
                isCurrentlySitting = true;
            }
        }

        // eating
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isCurrentlyEating)
            {
                // Wake up from sleep
                anim.SetBool("IsEating", false);
                isCurrentlyEating = false;
            }
            else
            {
                // Go to sleep
                anim.SetBool("IsEating", true);
                isCurrentlyEating = true;
            }
        }


        // If there's movement input, set IsWalking to true, otherwise set it to false
        anim.SetBool("IsWalking", Mathf.Abs(deltaX) > 0.1f || Mathf.Abs(deltaZ) > 0.1f);

        if (Input.GetButtonDown("Jump") && charController.isGrounded)
        {
            vertSpeed = jumpSpeed;
            anim.SetBool("IsJumping", true);
        }
        else if (!charController.isGrounded)
        {
            vertSpeed += gravity * 5 * Time.deltaTime;
            if (vertSpeed < terminalVelocity)
            {
                vertSpeed = terminalVelocity;
            }
        }
        else
        {
            anim.SetBool("IsJumping", false);
        }
        // make diagonal movement consistent
        movement = Vector3.ClampMagnitude(movement, speed);

        // add gravity in the vertical direction
        movement.y = gravity;
        movement.y = vertSpeed;
        // ensure movement is independent of the framerate
        movement *= Time.deltaTime;

        // transform from local space to global space
        movement = transform.TransformDirection(movement);

        // pass the movement to the character controller
        charController.Move(movement);
    }
}

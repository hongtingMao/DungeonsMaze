using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float runSpeed = 9.0f;
    public float jumpSpeed = 15.0f;
    public float terminalVelocity = -20.0f;
    public float gravity = -9.8f;
    public float originalSpeed;
    public float originalRunSpeed;
    public float originalJumpSpeed;

    private float vertSpeed;
    private bool isCurrentlySleeping = false;
    private bool isCurrentlySitting = false;
    private bool isCurrentlyEating = false;
    private bool isRestoringSpeed = false;
    private CharacterController charController;
    private Animator anim;
    private PlayerAttributes playerAttributes;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerAttributes = GetComponent<PlayerAttributes>();
        if (playerAttributes == null)
        {
            Debug.LogError("PlayerAttributes component is missing on the GameObject!");
        }
        originalSpeed = speed;
        originalRunSpeed = runSpeed;
        originalJumpSpeed = jumpSpeed;



    }

    void Update()
    {
        HandleMovement();
        HandleActions();
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetRunSpeed(float newRunSpeed)
    {
        runSpeed = newRunSpeed;
    }

    public void SetJumpSpeed(float newJumpSpeed)
    {
        jumpSpeed = newJumpSpeed;
    }

    public void RestoreOriginalJumpSpeed()
    {
        jumpSpeed = originalJumpSpeed;
        Debug.Log("Jump speed restored to original: " + jumpSpeed);
    }


    public void ResetRestoringSpeedFlag()
    {
        isRestoringSpeed = false;
    }

    public void RestoreOriginalSpeed()
    {
        speed = originalSpeed;
        runSpeed = originalRunSpeed;

    }

    private void HandleMovement()
    {
        float currentSpeed = speed;  // Default to walking speed
        anim.SetBool("IsRunning", false);  // Default to not running
        playerAttributes.SetRunning(false);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
    Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            ResetActions();
        }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Input.GetKey(KeyCode.LeftShift) && playerAttributes.Stamina > 0)
        {
            currentSpeed = runSpeed;  // Set speed to running speed
            anim.SetBool("IsRunning", true);  // Trigger running animation
            playerAttributes.SetRunning(true);
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

        // Jumping
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

        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        charController.Move(movement);

        // If there's movement input, set IsWalking to true, otherwise set it to false
        anim.SetBool("IsWalking", Mathf.Abs(deltaX) > 0.1f || Mathf.Abs(deltaZ) > 0.1f);
    }

    private void ResetActions()
    {
        anim.SetBool("IsEating", false);
        anim.SetBool("IsSitting", false);
        anim.SetBool("IsSleeping", false);
        isCurrentlyEating = false;
        isCurrentlySitting = false;
        isCurrentlySleeping = false;
        playerAttributes.SetSitting(false);
        playerAttributes.SetSleeping(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player collided with: " + other.gameObject.name);
        if (other.CompareTag("Food"))
        {
            FoodItem food = other.GetComponent<FoodItem>();
            if (food)
            {
                Debug.Log("Attempting to consume food: " + other.gameObject.name);
                food.Consume();
            }
        }
    }


    private void HandleActions()
    {
        // Sleeping
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCurrentlySleeping)
            {
                anim.SetBool("IsSleeping", false);
                isCurrentlySleeping = false;
                playerAttributes.SetSleeping(false);
            }
            else
            {
                anim.SetBool("IsSleeping", true);
                isCurrentlySleeping = true;
                playerAttributes.SetSleeping(true);
            }
        }

        // Sitting
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isCurrentlySitting)
            {
                anim.SetBool("IsSitting", false);
                isCurrentlySitting = false;
                playerAttributes.SetSitting(false);
            }
            else
            {
                anim.SetBool("IsSitting", true);
                isCurrentlySitting = true;
                playerAttributes.SetSitting(true);
            }
        }

        // Eating
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (isCurrentlyEating)
            {
                anim.SetBool("IsEating", false);
                isCurrentlyEating = false;
            }
            else
            {
                anim.SetBool("IsEating", true);
                isCurrentlyEating = true;
            }
        }
    }
}

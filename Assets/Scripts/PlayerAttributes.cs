using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerAttributes : MonoBehaviour
{
    public float HP = 100;
    public float Stamina = 100;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI StaminaText;
    public Slider staminaSlider;
    public Slider hpSlider;
    private PlayerInput playerInput;
    private float speedBoostDuration = 10f; // Duration of the speed boost

    private float walkStaminaDrain = 1f;
    private float runStaminaDrain = 20f;
    private float idleStaminaRecovery = 1f;
    private float sitStaminaRecovery = 5f;
    private float sleepStaminaRecovery = 10f;
    public TextMeshProUGUI CupcakeText;

    private bool isRunning = false;
    private bool isSitting = false;
    private bool isSleeping = false;
    private CharacterController charController;
    private Animator anim;
    private float originalSpeed;
    private float originalRunSpeed;
    private float originalJumpSpeed;

    private bool isRestoringSpeed = false;

    // chest
    public int CupcakeInventory = 0;

    // For Stamina color change
    private Image staminaFillImage;
    private Color normalColor = new Color(1f, 0.8549f, 0.4745f); // FFDA79
    private Color zeroStaminaColor = Color.red;

    void Start()
    {
        charController = GetComponent<CharacterController>();
        anim = transform.GetChild(0).GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        originalRunSpeed = playerInput.runSpeed;
        originalJumpSpeed = playerInput.jumpSpeed;


        // Initialize staminaFillImage from the stamina slider
        if (staminaSlider)
            staminaFillImage = staminaSlider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        UpdateStamina();
        UpdateUI();
    }

    public void ConsumeFood(string foodType)
    {
        switch (foodType)
        {
            case "Apple":
                HP += 30;
                break;
            case "Pumpkin":
                Debug.Log("Pumpkin consumed!");
                SpeedUp();
                break;
            case "Burger":
                Stamina += 50;
                break;
            case "Pepper":
                HP -= 30;
                break;
            case "Cupcake":
                CupcakeInventory += 1;
                UpdateCupcakeUI(); 
                break;
        }
    }

    private void UpdateCupcakeUI()
    {

        if (CupcakeText != null)
        {
            Debug.Log("Cupcake consumed!");
            CupcakeText.text = $"{CupcakeInventory}/6";
        }
    }




    private void SpeedUp()
    {
        isRestoringSpeed = true;
        Debug.Log("Speeding up!");
        playerInput.SetSpeed(playerInput.originalSpeed * 2); // Double the player's speed
        playerInput.SetRunSpeed(playerInput.originalRunSpeed * 2); // Double the player's run speed
        playerInput.SetJumpSpeed(playerInput.originalJumpSpeed * 1.5f); // Increase the player's jump speed
        StartCoroutine(SpeedBoostDuration());
    }

    private IEnumerator SpeedBoostDuration()
    {
      
        yield return new WaitForSeconds(speedBoostDuration);
    
        playerInput.RestoreOriginalSpeed(); // Restore the player's speed and run speed after the duration
        playerInput.RestoreOriginalJumpSpeed(); // Restore the player's jump speed after the duration
        isRestoringSpeed = false;

    }




private void UpdateStamina()
    {
        float deltaX = Mathf.Abs(Input.GetAxis("Horizontal"));
        float deltaZ = Mathf.Abs(Input.GetAxis("Vertical"));
        bool isMoving = deltaX > 0 || deltaZ > 0;

        if (isRunning && Stamina > 0)
        {
            Stamina -= runStaminaDrain * Time.deltaTime;
        }
        else if (isSitting && !isMoving)
        {
            Stamina += sitStaminaRecovery * Time.deltaTime;
        }
        else if (isSleeping && !isMoving)
        {
            Stamina += sleepStaminaRecovery * Time.deltaTime;
        }
        else if (!isMoving)
        {
            Stamina += idleStaminaRecovery * Time.deltaTime;
        }

        if (Stamina <= 0)
        {
            Stamina = 0;
            isRunning = false;
            playerInput.SetSpeed(playerInput.originalSpeed * 0.1f);

            // Decrease HP when Stamina reaches 0
            HP -= 5 * Time.deltaTime;
        }
        else if (Stamina >= 100 && !isRestoringSpeed)
        {
            Stamina = 100;
            playerInput.RestoreOriginalSpeed();
        }


        Stamina = Mathf.Clamp(Stamina, 0, 100);
        HP = Mathf.Clamp(HP, 0, 100);
    }


    private void UpdateUI()
    {
        if (HPText != null)
            HPText.text = ((int)HP).ToString();

        if (StaminaText != null)
            StaminaText.text = ((int)Stamina).ToString();

        if (staminaSlider != null)
        {
            staminaSlider.value = Stamina;

            // Change color based on Stamina value
            if (Stamina <= 0)
                staminaFillImage.color = zeroStaminaColor;
            else if (Stamina >= 100)
                staminaFillImage.color = normalColor;
        }

        if (hpSlider != null)
            hpSlider.value = HP;
    }

    public void SetRunning(bool running)
    {
        if (running && Stamina <= 0)
        {
            isRunning = false; // If stamina is 0, don't allow running
            anim.SetBool("IsRunning", false);  // Set running animation to false
            anim.SetBool("IsWalking", true);   // Set walking animation to true
        }
        else
        {
            isRunning = running;
        }
    }

    public void SetSitting(bool sitting)
    {
        isSitting = sitting;
    }

    public void SetSleeping(bool sleeping)
    {
        isSleeping = sleeping;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StaminaAndHealth : MonoBehaviour
{
    public Image staminaBar;
    public Image healthBar;
    public float maxStamina = 10;
    public float maxHealth = 10;
    public float walkStaminaDepletionRate = 1f;
    public float runJumpStaminaDepletionRate = 1f;
    public float staminaRecoveryRate = 1f;

    private float currentStamina;
    private float currentHealth;

    private bool isMoving;

    private void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        isMoving = Mathf.Abs(horizontalInput) > 0 || Mathf.Abs(verticalInput) > 0;

        if (isMoving)
        {
            UseStamina(walkStaminaDepletionRate * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                UseStamina(runJumpStaminaDepletionRate);
            }
        }
        else
        {
            RecoverStamina(staminaRecoveryRate * Time.deltaTime);
        }

        if (currentStamina <= 0)
        {
            UseHealth(staminaRecoveryRate * Time.deltaTime);
        }

        UpdateUI();
    }

    private void UseStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina);
    }

    private void RecoverStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina + amount, 0, maxStamina);
    }

    private void UseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
    }

    private void UpdateUI()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}

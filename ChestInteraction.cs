using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public PlayerInventory playerInventory; // Assign this in the Inspector
    private Animator ChestOpen;

    // Use this for initialization
    void Start()
    {
        ChestOpen = transform.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInventory != null) // Check if the playerInventory reference is not null
            {
                if (playerInventory.Cupcake >= 5)
                {
                    ChestOpen.SetTrigger("Open");
                    EndGame();
                }
                else
                {
                    Debug.Log("You need 5 cupcakes to open this chest.");
                }
            }
            else
            {
                Debug.LogError("PlayerInventory reference is not assigned.");
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit(); 
    }
}


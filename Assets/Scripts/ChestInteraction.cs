using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;  // <-- Add this line
using UnityEngine.SceneManagement;



public class ChestInteraction : MonoBehaviour
{
    public PlayerAttributes playerAttributes; // Assign this in the Inspector
    private Animator ChestOpen;
    private AudioSource ChestSound;
    private AudioClip ChestClip;
    private bool playerNearChest = false;

    public string nextLevelName;

    void Start()
    {
        ChestOpen = GetComponent<Animator>();
        ChestSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playerNearChest && Input.GetKeyDown(KeyCode.F))
        {
            TryOpenChest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearChest = false;
        }
    }

    private void TryOpenChest()
    {
        if (playerAttributes != null) // Check if the playerInventory reference is not null
        {
            if (playerAttributes.CupcakeInventory >= 5)
            {
                ChestOpen.SetTrigger("Open");
                PlayChestSound();
                LoadNextLevel();
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

    private void PlayChestSound()
    {
        if (ChestSound != null && ChestClip != null)
        {
            ChestSound.PlayOneShot(ChestClip);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName); // Load the next level by name
    }

}

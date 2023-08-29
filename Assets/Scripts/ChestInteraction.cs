using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;  
using UnityEngine.SceneManagement;

public class ChestInteraction : MonoBehaviour
{
    private PlayerAttributes playerAttributes;
    private Animator ChestOpen;
    private AudioSource ChestSound;
    private AudioClip ChestClip;
    private bool playerNearChest = false;
    public string nextLevelName;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

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
            if (playerAttributes.CupcakeInventory >= 5) // Modified this line
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

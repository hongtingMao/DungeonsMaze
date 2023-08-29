using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutChest : MonoBehaviour
{
    private PlayerAttributes playerAttributes;
    private Animator ChestOpen;
    private AudioSource ChestSound;
    private AudioClip ChestClip;

    // Use this for initialization
    void Start()
    {
        // Find the player using its tag and get its PlayerAttributes component
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerAttributes = player.GetComponent<PlayerAttributes>();
        }
        else
        {
            Debug.LogError("Player object not found in the scene.");
        }

        ChestOpen = transform.parent.GetComponent<Animator>();
        ChestSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerAttributes != null) // Check if the playerAttributes reference is not null
            {
                ChestOpen.SetTrigger("Open");
                PlayChestSound();
            }
            else
            {
                Debug.LogError("PlayerAttributes reference is not assigned.");
            }
        }
    }

    private void PlayChestSound()
    {
        if (ChestSound != null && ChestClip != null)
        {
            ChestSound.PlayOneShot(ChestClip);
        }
    }
}

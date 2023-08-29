using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutChest : MonoBehaviour
{
    public PlayerAttributes playerAttributes; // Assign this in the Inspector
    private Animator ChestOpen;
    private AudioSource ChestSound;
    private AudioClip ChestClip;

    // Use this for initialization
    void Start()
    {
        ChestOpen = transform.parent.GetComponent<Animator>();
        ChestSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerAttributes != null) // Check if the playerInventory reference is not null
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


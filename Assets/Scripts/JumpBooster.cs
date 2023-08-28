using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class JumpBooster : MonoBehaviour
{
    public float jumpBoostStrength = 2.0f; // Adjust this value to control how much the jump speed is increased.

    private void OnTriggerEnter(Collider other)
    {
        FirstPersonController playerController = other.GetComponent<FirstPersonController>();

        // Check if the collider belongs to the player and the FirstPersonController is attached.
        if (playerController != null)
        {
            // Increase the player's jump speed.
            playerController.m_JumpSpeed *= jumpBoostStrength;

            // Optional: Play a sound or visual effect when the booster is triggered.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FirstPersonController playerController = other.GetComponent<FirstPersonController>();

        // Check if the collider belongs to the player and the FirstPersonController is attached.
        if (playerController != null)
        {
            // Restore the player's original jump speed.
            playerController.m_JumpSpeed /= jumpBoostStrength;
        }
    }
}

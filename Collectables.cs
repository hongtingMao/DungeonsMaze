using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();   
        
        if (inventory != null )
        {
            inventory.CupcakeCollected();
            gameObject.SetActive(false);
        }
    }
}
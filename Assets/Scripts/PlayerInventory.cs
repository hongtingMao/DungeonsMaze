using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int Cupcake { get; private set; }

    public void CupcakeCollected()
    {
        Cupcake++;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    Animator DoorControl;
    bool doorOpen;

    // Use this for initialization
    void Start()
    {
        doorOpen= false;
        DoorControl = transform.parent.GetComponent<Animator>();
    }

    // an event function that is called when an object enters the trigger zone
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") // check the tag
        {
            doorOpen= true;
            DoorControl.SetTrigger("Open");
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        DoorControl.SetTrigger("Close");
        doorOpen= false;
    }
}

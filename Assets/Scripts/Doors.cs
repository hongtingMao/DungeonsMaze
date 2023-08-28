using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {
    Animator doorAnim;
    bool doorOpen;

	// Use this for initialization
	void Start () {
        doorOpen = false;
        doorAnim = GetComponent<Animator>();
	}
    
    // an event function that is called when an object enters the trigger zone
    void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.tag == "Player")  // check the tag
        {
            doorOpen = true;
            doorAnim.SetTrigger("Open");
           // Debug.Log("Player Entered Trigger Zone");      
        }
    }

    // an event function that is called when an object leaves the trigger zone
    private void OnTriggerExit(Collider col)
    {
        if(doorOpen==true)
        {
            doorOpen = false;
            doorAnim.SetTrigger("Close");
        }

       // Debug.Log("Exit Triger Zone");
    }

}

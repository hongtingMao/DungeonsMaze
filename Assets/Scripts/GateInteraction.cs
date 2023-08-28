using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateInteraction : MonoBehaviour
{
    Animator GateControl;
    bool gateOpen;

    // Use this for initialization
    void Start()
    {
        gateOpen = false;
        GateControl = transform.parent.GetComponent<Animator>();
    }

    // an event function that is called when an object enters the trigger zone
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player") // check the tag
        {
            gateOpen = true;
            GateControl.SetTrigger("Open");
            StartCoroutine(CloseGateAfterDelay());
        }
    }

    private IEnumerator CloseGateAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        GateControl.SetTrigger("Close");
        gateOpen = false;
    }
}

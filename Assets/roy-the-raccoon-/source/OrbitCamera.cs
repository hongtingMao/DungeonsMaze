using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    // the orbit target (e.g., the player GameObject)
    [SerializeField] Transform target;

    // rotation sensitivity
    public float rotSpeed = 1.5f;
    private float rotX;     // vertical rotation
    private float rotY;     // horizontal rotation
    private Vector3 offset; // offset from the target

    // Start is called before the first frame update
    void Start()
    {
        // get transform component's yaw

        rotX = transform.eulerAngles.x;
        rotY = transform.eulerAngles.y;

        // compute offset of camera from the target
        offset = target.position - transform.position;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        // yaw based on horizontal mouse movement
        rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        rotX -= Input.GetAxis("Mouse Y") * rotSpeed * 3;
        rotX = Mathf.Clamp(rotX, -45f, 45f);  // Clamping the vertical rotation to prevent over-rotation
        // convert from Euler angles to quaternions
        
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0);
        // set the camera's position based on the offset
        transform.position = target.position - (rotation * offset);

        // camera looking at the target
        transform.LookAt(target);
    }

}

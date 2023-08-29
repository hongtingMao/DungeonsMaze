using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class followP : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private NavMeshAgent agent;
    public Animator aiAnim;
    private Transform aiTransform; // AIµÄTransform
    Vector3 dest;
    private float previousYPosition;
    private bool isCurrentlySitting = false;
    private bool isFollowingPlayer = false;
    private bool isCurrentlyEating = false;

    public float followDistance = 3.0f; // follow destance

    public AudioClip walkingSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //charController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        aiTransform = transform;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingSound;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = player.position - aiTransform.position;
        directionToPlayer.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        float distanceToPlayer = Vector3.Distance(aiTransform.position, player.position);
        if (distanceToPlayer <= followDistance)
        {
            aiTransform.rotation = targetRotation;//facce to player

            if (Input.GetKeyDown(KeyCode.F)) // press f to follow player
            {
                isFollowingPlayer = true;
            }
        }

        if (isFollowingPlayer)
        {
            //walk follow player
            dest = player.position;
            agent.destination = dest;

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("idle");
                audioSource.Stop();

            }
            else
            {
                ResetActions();
                aiAnim.ResetTrigger("idle");
                aiAnim.SetTrigger("walk");                
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = walkingSound;
                    audioSource.PlayOneShot(walkingSound);
                }
            }
            //sitting
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.C))
            {
                if (isCurrentlySitting)
                {
                    // Wake up from sleep
                    aiAnim.SetBool("IsSitting", false);
                    isCurrentlySitting = false;
                }
                else
                {
                    // Go to sleep
                    aiAnim.SetBool("IsSitting", true);
                    isCurrentlySitting = true;
                }
            }

            // eating
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (isCurrentlyEating)
                {
                    // Wake up from sleep
                    aiAnim.SetBool("IsEating", false);
                    isCurrentlyEating = false;
                }
                else
                {
                    // Go to sleep
                    aiAnim.SetBool("IsEating", true);
                    isCurrentlyEating = true;
                }
            }
            // get y value
            float currentYPosition = transform.position.y;

            // check y change
            float deltaY = Mathf.Abs(currentYPosition - previousYPosition);
            if (deltaY > 0.1f)
            {
                aiAnim.SetTrigger("IsJumping"); 
            }

            //update y 
            previousYPosition = currentYPosition;
        }
        
    }
    private void ResetActions()
    {
        aiAnim.SetBool("IsEating", false);
        aiAnim.SetBool("IsSitting", false);
        aiAnim.SetBool("IsSleeping", false);
        isCurrentlyEating = false;
        isCurrentlySitting = false;
       
    }
}

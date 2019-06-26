﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GhostState
{
    Patrolling,
    Chasing,
    Searching
}
public class Ghost : MonoBehaviour
{


    private AudioSource ghostAudio;
    public AudioClip ambiant;
    public AudioClip scare;
    public GameObject ghostMesh;
    public GameObject gameoverCanvas;
    public float closeToPlayerRad;
    public float overlapOffset;

     [Header("Ghost Chase AI")]

    public Transform player;
    public Transform target;
    public bool ghostActive = false;
    public float currentMoveSpeed;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    public float speedDropMod;
    public float stopDistance = 1.0f;
    public float chaseDistance = 10.0f;
    public float slowdownDistance = 5.0f;
    public GhostState currentGhostState;


    public bool canSeePlayer;

    [Header("Ghost Patrol AI")]
    public Transform[] points;
    public int destPoint;
    public float rotSpeed;
    public bool randomPatrol;
    public float patrolStopDistance;
    public float ghostSpotDistance;
    public float ghostSpotRadius;
    public LayerMask playerLayer;
    public float preTurnDist;

    [Header("Ghost Search AI")]
    public float searchTime;
    public float searchTimer;
    public float rotateTime;
    public float arc;
    Vector3 lastKnownPos;

     [Header("Ghost Audio")]
    public float timeBetweeIdleNoises;
    private float ghostIdleTimer;
    public float chanceOfIdleNoise;
    public AudioClip[] idleNoises;
    public AudioClip[] breathingNoises;
    public float breathingVol;
    public float timeBetweenBreath;
    private float breathTimer;

    public AudioClip[] spottedNoises;
    public float spottedVol;

    public AudioClip firstSpottedNoise;

    public GameObject[] targets;
    public int targetIndex =1;

    [Header("Ghost Light")]
    public Light ghostLight;
    public float ghostLightInitIntensity;
    public float ghostLightDuration;

    [Header("Ghost Alpha")]
    [Range(0,1)]
    public float ghostInitAlpha;

    public float ghostFadeDuration;
    public float distanceToTarget;
    Renderer ghostRenderer;
    Coroutine fadeOut;
    Coroutine lightFade;
    public float targetAngle;
    public float floorPos;
    public GhostModel ghostModel;

    public Material ghostMat;

    public PlayerDamage playerDamage;
    public bool playerInHitBox;
    public float damageTimer;
    public float timeBetweenHits;
    private void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        player = playerObj.transform;
        playerDamage = playerObj.GetComponentInChildren<PlayerDamage>();
        ghostAudio = GetComponent<AudioSource>();
        target = points[0];
        damageTimer = timeBetweenHits;
    }
    public void OnFirstActivateGhost()
    {
        ghostActive = true;
        ghostAudio.PlayOneShot(firstSpottedNoise, 1.0f);

    }

    public void ToggleGhost(bool ghostOn)
    {
        if (ghostOn)
        {
            ghostModel.FreezePos();
            if (fadeOut != null)
            {
                StopCoroutine(fadeOut);
            }
            if (lightFade != null)
            {
                StopCoroutine(lightFade);

            }
            //turn ghost on and move to correct position and rotation
            ghostMesh.transform.position = transform.position;
            ghostMesh.transform.rotation = transform.rotation;

            //set up light itencity and alpha
            ghostLight.intensity = ghostLightInitIntensity;
            ghostMat.SetFloat("_Alpha", 0.7f);
            fadeOut = StartCoroutine(FadeTo(ghostMat, 0f, ghostFadeDuration));
            lightFade = StartCoroutine(LightFade(0f, ghostLightDuration));


        }
       
    }

  
    void Update()
    {

        GhostAudio();


        if (playerInHitBox)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer < 0)
            {
                playerDamage.TakeDamage();
                damageTimer = timeBetweenHits;


            }
        }

        if (ghostActive)
        {
            Vector3 transpos = transform.position;

            Debug.DrawRay((transpos + transform.right * ghostSpotRadius), transform.forward * ghostSpotDistance, Color.green);
            Debug.DrawRay((transpos + transform.right * ghostSpotRadius * -1f), transform.forward * ghostSpotDistance, Color.green);
            Debug.DrawRay(transform.position, transform.forward * distanceToTarget, Color.red);
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, ghostSpotRadius, transform.forward,out hit, ghostSpotDistance))
            {
               
                if (hit.collider.tag == "Player")
                {
                    RaycastHit testHit;
                    if (Physics.Raycast(transform.position, (player.position - transform.position), out testHit ,100000.0f ))
                    {
                        Debug.DrawRay(transform.position, (hit.point - transform.position), Color.black);
                        if (testHit.collider.tag == "Player")
                        {
                            canSeePlayer = true;

                        }

                    }

                }
                else
                {
                    canSeePlayer = false;

                }
            }
            else
            {
                canSeePlayer = false;


            }
            Collider[] objs = Physics.OverlapSphere(transform.position + transform.forward * overlapOffset, closeToPlayerRad, playerLayer);
            foreach(Collider ob in objs) 
            {
                if (ob.tag == "Player")
                {
                    canSeePlayer = true;

                }
               

            }

            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            Vector3 pos = target.position;
            pos.y = floorPos;

            switch (currentGhostState)
            {
                case GhostState.Chasing:
                    if (!canSeePlayer)
                    {
                        ChangeGhostState(GhostState.Searching);

                    }

                    MoveTowardsPlayer(pos);
                    if (distanceToTarget > chaseDistance)
                    {
                        ChangeGhostState(GhostState.Patrolling);
                    }
                    transform.LookAt(pos);

                    break;
                case GhostState.Patrolling:
                    if (canSeePlayer)
                    {
                        ChangeGhostState(GhostState.Chasing);

                    }

                    // Rotate our transform a step closer to the target's.
                    Vector3 targetVect = (target.transform.position - transform.position);
                    float currentAngle = Vector3.Angle(targetVect, transform.forward);
                    var rotstep = rotSpeed * Time.deltaTime;
                    if (distanceToTarget > patrolStopDistance)
                    {
                        if (distanceToTarget > preTurnDist)
                        {
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetVect, transform.up), rotstep);

                        }
                        else
                        {
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(points[(destPoint + 1) % points.Length].transform.position, transform.up), rotstep);
                                
                        }

                        transform.position += transform.forward * Time.deltaTime * currentMoveSpeed;

                    }
                    else
                    {
                        GotoNextPoint();

                    }

                    break;
                case GhostState.Searching:
                    if (searchTimer <= 0)
                    {
                        ChangeGhostState(GhostState.Patrolling);
                    }
                    if (!MoveTowardsPlayer(lastKnownPos))
                    {
                        transform.Rotate(Vector3.up, rotateTime * Time.deltaTime);

                    }
                    
                    transform.LookAt(lastKnownPos);
                    if (!canSeePlayer)
                    {
                        searchTimer -= Time.deltaTime;
                    }
                    else
                    {
                        ChangeGhostState(GhostState.Chasing);
                    }
                    break;
            }
        
        }
        
    }

    public bool MoveTowardsPlayer(Vector3 targetPos)
    {
        if (distanceToTarget > stopDistance)
        {
            if (distanceToTarget > slowdownDistance)
            {
                if (currentMoveSpeed <= maxMoveSpeed)
                {
                    currentMoveSpeed += Time.deltaTime * speedDropMod;
                }
            }
            else
            {
                if (currentMoveSpeed >= minMoveSpeed)
                {
                    currentMoveSpeed -= Time.deltaTime * speedDropMod;
                }

            }
            float step = currentMoveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
            return false;
        }
        else
        {
            return true;
        }

  
    }

    public void ChangeGhostState(GhostState newGhostState)
    {
        currentMoveSpeed = maxMoveSpeed;

        switch (newGhostState)
        {
            case GhostState.Chasing:
                Debug.Log("Chasing");
                target = player;
                break;
            case GhostState.Patrolling:
                Debug.Log("Patrolling");
                target = points[FindNearestPoint()];
                break;
            case GhostState.Searching:
                lastKnownPos = target.position;
                Debug.Log("searching");
                searchTimer = searchTime;
                break;

        }
        currentGhostState = newGhostState;

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + transform.forward * overlapOffset, closeToPlayerRad);
        if (currentGhostState == GhostState.Searching)
        {
            // Draw a yellow cube at the transform position
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(lastKnownPos, new Vector3(1, 1, 1));

        }
    }
    public int FindNearestPoint()
    {
        float dist = 99999.0f;
        int currentBest =0;

        for(int i = 0; i < points.Length; i++)
        {
            float testDist = Mathf.Abs(Vector3.Distance(points[i].position, transform.position));
            if (testDist < dist)
            {
                
                dist = testDist;
                currentBest = i;
            }
            
        }
        return currentBest;

    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        target = points[destPoint].transform;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        if (randomPatrol)
        {
            destPoint = Random.Range(0, points.Length);

        }
        else
        {
             destPoint = (destPoint + 1) % points.Length;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ghostActive)
        {
       
            if (other.gameObject.tag == "Player")
            {
                playerInHitBox = true; 


            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (ghostActive)
        {

            if (other.gameObject.tag == "Player")

            {
                playerInHitBox = false;
            }
        }
    }



    IEnumerator LightFade( float targetOpacity, float duration)
    {

        float startBrightness = ghostLight.intensity; 
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp(time, 0,  duration);
            ghostLight.intensity = Mathf.Lerp(startBrightness, targetOpacity, blend);
            yield return null;
        }

    }

    IEnumerator FadeTo(Material material, float targetOpacity, float duration)
    {

        // Cache the current color of the material, and its initiql opacity.
        float startOpacity = material.GetFloat("_Alpha");

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {
            // Step the fade forward one frame.
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            material.SetFloat("_Alpha", Mathf.Lerp(startOpacity, targetOpacity, blend));



            // Wait one frame, and repeat.
            ghostModel.UnFreezePos();

            yield return null;
        }
        yield return null;
        
    }

    public void GhostAudio()
    {
        ghostIdleTimer -= Time.deltaTime;
        breathTimer -= Time.deltaTime;

        if (breathTimer <= 0)
        {
            breathTimer = timeBetweenBreath;
            HelperFunctions.PlayRandomNoiseInArray(breathingNoises, ghostAudio, breathingVol);
        }
        else
        { 

            if (ghostIdleTimer <= 0)
            {
                ghostIdleTimer = timeBetweeIdleNoises;

                if (Random.Range(0, 100) < chanceOfIdleNoise)
                {
                    HelperFunctions.PlayRandomNoiseInArray(idleNoises, ghostAudio, Random.Range(0.5f, 1f));
                }
            }
        
        }

    }
}

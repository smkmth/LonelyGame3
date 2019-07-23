using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetector : MonoBehaviour
{

    public float spaceBetweenTicks;
    public float timer;
    public AudioClip[] gigerClicks;
    public AudioClip[] urgentGigerClicks;
    public Transform ghost;
    private AudioSource audioSource;
    public float startDist;
    public float dist;
    public int helperId = 2;

    public void Start()
    {
        ghost = GameObject.Find("Ghost").transform;
        audioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        if (ghost.gameObject.activeSelf != true)
        {
            return;
        }
        dist = Mathf.Abs(Vector3.Distance(transform.position, ghost.position));


        spaceBetweenTicks = dist*dist * 0.01f;
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = spaceBetweenTicks;
            HelperFunctions.Helper.PlayRandomNoiseInArray(gigerClicks, audioSource, .5f, helperId);
        }



    }
}

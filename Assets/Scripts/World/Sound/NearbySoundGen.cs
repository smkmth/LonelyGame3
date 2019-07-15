using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearbySoundGen : MonoBehaviour
{
    public Transform player;
    public float soundRad;
    public AudioClip[] noises;
    public AudioSource[] sources;
    public float chanceOfNoise;
    public float noisesPerSecond;
    private float noiseTimer;

    public LayerMask selfLayer;
    // Update is called once per frame
    void Update()
    {

        if (noiseTimer <= 0)
        {
            noiseTimer = noisesPerSecond;
            if (Random.Range(0, 100) < chanceOfNoise)
            {
                Collider[] hits = Physics.OverlapSphere(player.position, soundRad);

                foreach (Collider hit in hits)
                {
                    if (hit.tag == "SoundSource")
                    {


                        RaycastHit potplayer;
                        Physics.Linecast(hit.transform.position, player.position, out potplayer, selfLayer, QueryTriggerInteraction.Ignore);

                        if (potplayer.collider.tag == "Player")
                        {
                        }
                        else
                        {
                            Debug.DrawRay(hit.transform.position, (player.position - hit.transform.position), Color.red, 3000.0f);

                            Debug.Log("Play" + hit.transform.name);
                            
                            HelperFunctions.PlayRandomNoiseInArray(noises, hit.GetComponent<AudioSource>(), Random.Range(0.5f, 1f), true);
                            return;
                        }
                        

                    
                    }

                }
                int sourceindex = Random.Range(0, sources.Length);
            }
        }
        else
        {
            noiseTimer -= Time.deltaTime;
        }
    }
}

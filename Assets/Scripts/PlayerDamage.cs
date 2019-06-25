using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public bool isDamaged;
    public GameObject image;
    public GameObject gameOver;
    public float timer;
    public float timeDamaged;

    public void Start()
    {
        timer = timeDamaged;
    }

    public void TakeDamage()
    {
        if (!isDamaged)
        {
            isDamaged = true;

        }
        else
        {
            image.SetActive(false);
            gameOver.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            timer -= Time.deltaTime;
            image.SetActive(true);
            if (timer <= 0)
            {
                timer = timeDamaged;
                isDamaged = false;
            }
        }
        else
        {
            
            image.SetActive(false);
        }
    }
}

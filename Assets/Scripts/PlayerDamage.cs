using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public bool isDamaged;
    public GameObject bloodImage;
    public GameObject screenFlash;
    public GameObject gameOver;
    public float timer;
    public float timeDamaged;
    public AudioClip impactSound;
    public AudioSource playerAudio;

    public void Start()
    {
        timer = timeDamaged;
    }

    public void TakeDamage()
    {
        if (!isDamaged)
        {
            playerAudio.PlayOneShot(impactSound, 4.0f);
            StartCoroutine(DamageFlash());
            isDamaged = true;

        }
        else
        {
            playerAudio.PlayOneShot(impactSound, 4.0f);
            bloodImage.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            gameOver.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public IEnumerator DamageFlash()
    {
        screenFlash.SetActive(true);

        yield return new WaitForSeconds(.05f);
        screenFlash.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        if (isDamaged)
        {
            StartCoroutine(SpriteFade(bloodImage.GetComponent<Image>(), 0, 10.0f));
            timer -= Time.deltaTime;
            bloodImage.SetActive(true);
            if (timer <= 0)
            {
                timer = timeDamaged;
                isDamaged = false;
            }
        }
        else
        {
            
            bloodImage.SetActive(false);
        }
    }

    IEnumerator SpriteFade(Image sprite,float targetOpacity, float duration)
    {

        float startAlpha = sprite.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float blend = Mathf.Clamp(time, 0, duration);
            startAlpha = Mathf.Lerp(startAlpha, targetOpacity, blend);
            Color alpha = new Color(0,0,0,startAlpha);
            sprite.color = alpha;
            yield return null;
        }

    }

}

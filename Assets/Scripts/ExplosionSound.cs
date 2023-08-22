using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioExplosionEnemy;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        audioSource.PlayOneShot(audioExplosionEnemy, 0.5f);
    }
}

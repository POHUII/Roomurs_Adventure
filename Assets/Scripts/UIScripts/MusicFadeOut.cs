using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFadeOut : MonoBehaviour
{
    public AudioSource audioSource;

    private bool isFadeOut;

    private void Start()
    {
        isFadeOut = false;
    }

    private void Update()
    {
        if (isFadeOut)
            audioFadeOut();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isFadeOut = true;
        }
    }

    private void audioFadeOut()
    {
        audioSource.volume -= Time.deltaTime * 0.5f;
    }
}

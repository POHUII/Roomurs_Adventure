using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FootStep()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    public void WaveSword()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }

    public void PullBowstring()
    {
        audioSource.clip = audioClips[2];
        audioSource.Play();
    }

    public void Injury()
    {
        audioSource.clip = audioClips[3];
        audioSource.Play();
    }
}

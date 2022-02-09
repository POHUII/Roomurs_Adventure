using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderActive : MonoBehaviour
{
    public GameObject spiderDen;
    public GameObject spider;
    public GameObject bat;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            audioSource.Play();
    }

    public void DenBroken()
    {
        spiderDen.GetComponent<Animator>().SetBool("Broken", true);
        transform.DetachChildren();
        Destroy(spiderDen, 1f);
        Invoke("CreateSpiderAndBat", 1.1f);
    }

    private void CreateSpiderAndBat()
    {
        spider.SetActive(true);
        bat.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
}

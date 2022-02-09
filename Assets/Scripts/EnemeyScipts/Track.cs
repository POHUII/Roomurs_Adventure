using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public GameObject[] enemis;
    public bool assign;
    public GameObject NextTrack;
    public GameObject lighting;

    private bool timeToLoadNext;
    private bool canSet;

    private void Start()
    {
        canSet = true;
    }

    private void Update()
    {
        LoadNextTrack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player") && canSet)
        {
            lighting.SetActive(true);
            SetTrack();
        }
    }

    public void SetTrack()
    {
        for (int i = 0; i < enemis.Length; i++)
        {
            enemis[i].SetActive(true);
        }
        canSet = false;
    }

    public void LoadNextTrack()
    {
        if (transform.childCount == 0)
        {
            if (!assign)
                NextTrack.SetActive(true);
            transform.DetachChildren();
            Destroy(gameObject, 2f);
        }
    }
}

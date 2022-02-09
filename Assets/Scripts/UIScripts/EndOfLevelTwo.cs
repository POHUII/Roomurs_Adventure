using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevelTwo : MonoBehaviour
{
    public GameObject lastGuide;
    public GameObject bossCrop;
    public GameObject tailor;
    private void Start()
    {
        lastGuide.SetActive(true);
        GameObject Player = GameObject.Find("Player");
        Player.GetComponent<PlayerAttack>().AddArrow();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("arrow"))
        {
            Destroy(other.gameObject);
            Destroy(bossCrop, 0.2f);
            Destroy(gameObject, 0.4f);
            lastGuide.SetActive(false);
        }
        if (other.CompareTag("Player"))
        {
            Destroy(bossCrop);
            tailor.SetActive(true);
            tailor.GetComponent<Animator>().SetTrigger("idle");
            Destroy(gameObject, 0.3f);
            lastGuide.SetActive(false);
        }
    }
}

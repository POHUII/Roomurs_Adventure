using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyrisController : MonoBehaviour
{
    public GameObject panelDialog;
    public GameObject smoke;
    public GameObject leftFirebowl, rightFirebowl;
    public GameObject door, background, boundNextLevel;
    public AudioSource hit, opendoor;
    public GameObject panelDecision;
    private void Start()
    {
        panelDialog.SetActive(true);
        leftFirebowl.GetComponent<Animator>().SetTrigger("fire");
        rightFirebowl.GetComponent<Animator>().SetTrigger("fire");
        leftFirebowl.GetComponent<AudioSource>().Play();
        rightFirebowl.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (!panelDialog.activeSelf)
        {
            GetComponent<Animator>().Play("Syris_Idle");
            opendoor.Play();
            panelDecision.SetActive(true);
            Destroy(door);
            boundNextLevel.SetActive(true);
        }
    }

    public void SyrisDead()
    {
        hit.Play();
        background.GetComponent<AudioSource>().Play();
        Instantiate(smoke, transform.position, Quaternion.identity);
        Destroy(gameObject, 0.2f);
        Destroy(panelDecision);
    }
}

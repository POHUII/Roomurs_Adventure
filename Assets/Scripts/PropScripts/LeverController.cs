using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject buttonLever;

    private bool leverSetup;

    private void Start()
    {
        leverSetup = false;
    }

    private void Update()
    {
        LeverReset();
        PushLever();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonLever.SetActive(true);
            leverSetup = true;
        }
    }

    private void LeverReset()
    {
        if (TrolleyMovement.onLeftHand)
        {
            GetComponent<Animator>().Play("LeverIdle");
            leverSetup = true;
        }
    }

    private void PushLever()
    {
        if (leverSetup && Input.GetKeyDown(KeyCode.F))
        {
            buttonLever.SetActive(false);
            GetComponent<Animator>().Play("PushLever");
            TrolleyMovement.moving = true;
            leverSetup = false;
        }
    }
}

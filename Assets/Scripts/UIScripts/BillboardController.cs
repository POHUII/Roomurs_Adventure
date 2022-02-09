using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardController : MonoBehaviour
{
    public GameObject billboard;
    public GameObject buttonRead;

    private bool openBillboard;
    private bool isCanOpen;

    private void Start()
    {
        openBillboard = false;
        isCanOpen = false;
    }

    private void Update()
    {
        ShowBillboard();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            buttonRead.SetActive(true);
            isCanOpen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            buttonRead.SetActive(false);
            isCanOpen = false;
        }
    }
    private void ShowBillboard()
    {
        if (Input.GetKeyDown(KeyCode.R) && isCanOpen)
        {
            openBillboard = !openBillboard;
            billboard.SetActive(openBillboard);
        }
    }
}

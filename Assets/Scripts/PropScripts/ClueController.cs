using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueController : MonoBehaviour
{
    private bool canRead = false;

    public GameObject readButton;
    public GameObject sheet;

    private void Update()
    {
        OpenSheet();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRead = true;
            readButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRead = false;
            readButton.SetActive(false);
        }
    }

    private void OpenSheet()
    {
        if (canRead && Input.GetKeyDown(KeyCode.R))
        {
            sheet.SetActive(true);
        }
    }
}

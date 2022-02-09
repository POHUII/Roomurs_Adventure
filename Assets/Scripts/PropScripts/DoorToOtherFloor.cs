using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorToOtherFloor : MonoBehaviour
{
    public GameObject useButton;
    public Transform stairPos;
    public GameObject panelCutTo;

    private bool upFloor = false;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("$Player");
    }

    private void Update()
    {
        GoUpstair();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        useButton.SetActive(true);
        upFloor = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        useButton.SetActive(false);
        upFloor = false;
    }

    void GoUpstair()
    {
        if (upFloor && Input.GetKeyDown(KeyCode.F))
        {
            panelCutTo.SetActive(true);
            player.transform.position = stairPos.position;
            Invoke("ResetPanelCutTo", 0.3f);
        }
    }

    void ResetPanelCutTo()
    {
        panelCutTo.SetActive(false);
    }
}

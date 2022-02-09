using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantActive : MonoBehaviour
{
    public GameObject buttonDialog;
    public GameObject dialogWithKing;
    public GameObject buttonBag;
    private GameObject Player;

    private bool meetKing;

    private void Start()
    {
        meetKing = false;

        Player = GameObject.Find("$Player");
    }

    private void Update()
    {
        if (Mathf.Abs(Player.transform.position.x - transform.position.x) < 1f && Mathf.Abs(Player.transform.position.y - transform.position.y) < 0.5f && !meetKing)
        {
            dialogWithKing.SetActive(true);
            meetKing = true;
            buttonBag.SetActive(true);
            Player.GetComponent<PlayerController>().OpenBagMode();
        }
    }
}

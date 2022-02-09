using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailorController : MonoBehaviour
{
    public GameObject dialogWithKing;

    private void Start()
    {
        StartCoroutine(StartTalk());
    }

    IEnumerator StartTalk()
    {
        yield return new WaitForSeconds(2f);
        dialogWithKing.SetActive(true);
    }
}

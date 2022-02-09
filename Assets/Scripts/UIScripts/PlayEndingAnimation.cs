using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndingAnimation : MonoBehaviour
{
    public GameObject panelEndingOne, panelEndingTwo;

    private void Start()
    {
        if (EndingDesision.isBadEnding)
            panelEndingOne.SetActive(true);
        else
            panelEndingTwo.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerForSceneTwo : MonoBehaviour
{
    public GameObject buttonBag;

    private void Start()
    {

    }

    public void OpenBagButton()
    {
        buttonBag.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("$Player");
        GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAttackMode : MonoBehaviour
{
    private GameObject arrow;

    private void Start()
    {
        arrow = Resources.Load<GameObject>("Prefabs/arrow");
    }

    private void Update()
    {
        SwitchSprite();
    }

    private void SwitchSprite()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(arrow, transform.position, Quaternion.identity);
        }
    }
}

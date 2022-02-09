using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDes : MonoBehaviour
{
    public GameObject attackTraningTip;
    public GameObject tipAttackButton;
    public GameObject BoundNextLevel;

    public void Broken()
    {
        GetComponent<Animator>().SetBool("broken", true);
        StartCoroutine(WaitBroken());
    }

    IEnumerator WaitBroken()
    {
        yield return new WaitForSeconds(0.42f);
        Destroy(attackTraningTip);
        Destroy(tipAttackButton);
        BoundNextLevel.GetComponent<Collider2D>().isTrigger = true;
        Destroy(gameObject);

    }
}

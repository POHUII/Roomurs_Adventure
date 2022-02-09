using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestActive : MonoBehaviour
{
    Animator animatorChest;

    private bool canOpen;
    private GameObject prefabKey, prefabHealthPotion;

    public Transform positionCreate;

    private void Start()
    {
        animatorChest = GetComponent<Animator>();

        canOpen = false;

        prefabKey = Resources.Load<GameObject>("Prefabs/KeyForCave");
        prefabHealthPotion = Resources.Load<GameObject>("Prefabs/health_potion");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    private void Update()
    {
        OpenChest();
    }

    private void OpenChest()
    {
        if (canOpen && Input.GetKeyDown(KeyCode.F))
        {
            animatorChest.SetTrigger("open");
            GetComponent<AudioSource>().Play();
            Instantiate(prefabKey, positionCreate.position, Quaternion.identity);
            Instantiate(prefabHealthPotion, positionCreate.position, Quaternion.identity);
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}

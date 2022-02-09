using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeActive : MonoBehaviour
{
    public GameObject coinPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerController>().PlayerInjured(2);
    }

    public void Death()
    {
        Instantiate(coinPrefab, transform.position, Quaternion.identity);

        GetComponent<Animator>().SetBool("die", true);
        Destroy(gameObject, 0.25f);
    }
}

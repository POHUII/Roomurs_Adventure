using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitEnemy : MonoBehaviour
{
    private float startTime = 4f, timeToShowArcher = 0, timeBtwInit;
    private float enemyCount = 0;
    private GameObject Player;
    private BoxCollider2D battleBound;
    private Animator anim;

    public int numSkeleton;
    public GameObject enemySkeleton, enemeyArcher;
    public bool createArcher;
    public static int enemyRest;

    private void Start()
    {
        Player = GameObject.Find("$Player");
        battleBound = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("OpenPortal");

        enemyRest = 0;
    }

    private void Update()
    {
        bool startInit = false;
        if (Player.transform.position.x >= 21f && Player.transform.position.x <= 41f)
            startInit = true;


        if (startInit)
            StartCoroutine(Wait());

        CreateArcher();
    }

    public void RandomInitEnemy()
    {
        if (timeBtwInit <= 0 && enemyCount <= numSkeleton)
        {

            Instantiate(enemySkeleton, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            enemyCount++;
            enemyRest++;

            timeBtwInit = startTime;
        }
        else
            timeBtwInit -= Time.deltaTime;
    }

    private void CreateArcher()
    {
        if (enemyRest < 0 && enemyCount >= numSkeleton && createArcher)
        {
            if (timeToShowArcher >= 7f)
            {
                if (createArcher)
                {
                    Instantiate(enemeyArcher, new Vector3(transform.position.x - 1.3f, transform.position.y, 0), Quaternion.identity);
                    createArcher = false;
                }
                else
                {
                    Instantiate(enemeyArcher, new Vector3(transform.position.x + 1.3f, transform.position.y, 0), Quaternion.identity);
                }
            }
            else
                timeToShowArcher += Time.deltaTime;
        }

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        RandomInitEnemy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : Enemy
{
    private bool move;
    private AnimatorStateInfo animInfo;
    private bool attacking, onleft;
    private const float leftPortalPos = 21.5f;
    private const float rightPortalPos = 40.5f;
    private float startTimeToTransfer = 0, currentTime = 0;
    private int accumulator;
    private GameObject propManager;
    private GameObject arrowPrefab, bowPrefab;
    private GameObject healthPotion;


    protected override void Start()
    {
        base.Start();

        animatorEnemy = GetComponent<Animator>();

        faceLeft = true;
        onleft = false;
        move = false;
        attacking = false;
        dead = false;

        startTimeBtwAttack = 0;

        leftPosition = leftPortalPos;
        rightPosition = rightPortalPos;

        accumulator = 0;

        arrowPrefab = Resources.Load<GameObject>("Prefabs/arrowArcher");
        bowPrefab = Resources.Load<GameObject>("Prefabs/weapon_bow");
        healthPotion = Resources.Load<GameObject>("Prefabs/health_potion");
        propPrefab = bowPrefab;
        propManager = GameObject.Find("BattleSpace");
        Player = GameObject.Find("$Player");
    }

    private void Update()
    {
        if (move)
            EnemyMovement();
        RunAnimation();

        animInfo = animatorEnemy.GetCurrentAnimatorStateInfo(0);
        ArcherAttacking();

        if (assign)
            ArcherTransfer(3f);
    }

    protected override void EnemyMovement()
    {
        base.EnemyMovement();
        if (faceLeft)
        {
            if (collEnemy.IsTouchingLayers(ground) && transform.position.x <= rightPosition)
            {
                transform.localScale = new Vector3(1, 1, 1);
                rigidbodyEnemy.velocity = new Vector2(moveSpeed, 1f);
            }
            if (transform.position.x >= rightPosition - 1)
            {
                move = false;
                transform.position = new Vector2(leftPortalPos + 1, transform.position.y);
                faceLeft = false;
            }
        }
        else
        {
            if (collEnemy.IsTouchingLayers(ground) && transform.position.x >= leftPosition)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rigidbodyEnemy.velocity = new Vector2(-moveSpeed, 0f);
            }
            if (transform.position.x <= leftPosition + 1)
            {
                move = false;
                transform.position = new Vector2(rightPortalPos - 1, transform.position.y);
                faceLeft = true;
            }
        }
    }
    private void ArcherAttacking()
    {
        if (!injured && !move)
        {
            if (timeBtwAttack >= 2)
            {
                GetComponent<Animator>().SetBool("attack", true);

                if (timeBtwAttack >= 3.20f)
                {
                    GameObject arrow = Instantiate(arrowPrefab, attackPos.position, Quaternion.identity) as GameObject;
                    arrow.GetComponent<ArrowMovement>().DetectDirection(transform.localScale.x);
                    accumulator++;
                    timeBtwAttack = startTimeBtwAttack;
                    GetComponent<Animator>().SetBool("attack", false);
                }
            }
            timeBtwAttack += Time.deltaTime;
        }
    }


    private void ArcherTransfer(float timeToTransfer)
    {
        if (currentTime >= timeToTransfer)
        {
            move = true;
            if (currentTime >= timeToTransfer + 0.11f)
            {
                currentTime = startTimeToTransfer;
            }
        }
        currentTime += Time.deltaTime;
    }

    public void DropHealthPotion()
    {
        if (health <= 0)
        {
            Instantiate(healthPotion, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.identity);
        }
    }

}

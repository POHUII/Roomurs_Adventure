using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    private float moveSpeed = 6f;
    private Rigidbody2D rigidArrow;
    private bool faceLeft, isArrowMoving;
    private float timeCurrent;
    public bool isArcher;

    private void Start()
    {
        rigidArrow = GetComponent<Rigidbody2D>();
        isArrowMoving = false;

        timeCurrent = 0;
    }

    private void Update()
    {
        Movement();

        DestroyWhenTimeOut(8f);
    }


    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (isArcher)
        {
            if (hit.CompareTag("Player"))
            {
                hit.GetComponent<PlayerController>().PlayerInjured(7);
                Destroy(gameObject);
            }
        }
        else
        {
            if (hit.CompareTag("Archer"))
            {
                hit.GetComponent<ArcherController>().EnemyInjured(20);
                Destroy(gameObject);
            }

            if (hit.CompareTag("Skeleton"))
            {
                hit.GetComponent<SkeletonActive>().EnemyInjured(20);
                Destroy(gameObject);
            }
            if (hit.CompareTag("Bat"))
            {
                hit.GetComponent<BatActive>().Injured(100);
                Destroy(gameObject);
            }
        }
    }

    private void Movement()
    {
        if (!isArrowMoving)
        {
            if (faceLeft)
            {
                rigidArrow.velocity = new Vector2(-moveSpeed, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                isArrowMoving = true;
            }
            else
            {
                rigidArrow.velocity = new Vector2(moveSpeed, 0);
                transform.localScale = new Vector3(1, 1, 1);
                isArrowMoving = true;
            }
        }
    }

    private void DestroyWhenTimeOut(float timeDestroy)
    {
        if (timeCurrent >= timeDestroy)
        {
            Destroy(gameObject);
        }
        else
            timeCurrent += Time.deltaTime;
    }

    public void DetectDirection(float roleScale)
    {
        if (roleScale == -1)
            faceLeft = true;
        else if (roleScale == 1)
            faceLeft = false;
    }
}

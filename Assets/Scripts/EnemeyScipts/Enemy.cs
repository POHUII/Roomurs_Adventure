using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animatorEnemy;
    protected GameObject Player;
    protected float distanceToPlayer;
    protected Rigidbody2D rigidbodyEnemy;
    protected float leftPosition, rightPosition;
    protected bool faceLeft, onTrackPlayer, betweenBound;
    protected Collider2D collEnemy;
    protected float timeBtwAttack, startTimeBtwAttack;
    protected bool injured, dead;
    protected GameObject propPrefab;
    protected AudioSource audioSourceEnemy;
    protected float signOfLocalScale;

    public LayerMask ground, whatIsPlayer;
    public float moveSpeed;
    public float health, attackRange;
    public Transform attackPos;
    public GameObject hitEffect;
    public GameObject explosionVFXPrefab;
    public bool assign;
    public int valDamage;
    public GameObject soliloquize;

    protected virtual void Start()
    {
        animatorEnemy = GetComponent<Animator>();
        rigidbodyEnemy = GetComponent<Rigidbody2D>();
        collEnemy = GetComponent<Collider2D>();
        faceLeft = true;
        audioSourceEnemy = GetComponent<AudioSource>();
        Player = GameObject.Find("$Player");
        dead = false;
    }

    private void Update()
    {
        EnemyMovement();
    }

    protected void Attacking()
    {
        if (!injured && !dead)
        {
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsPlayer);
            if (playerToDamage.Length != 0)
            {
                for (int i = 0; i < playerToDamage.Length; i++)
                {
                    if (timeBtwAttack <= 0)
                    {
                        if (playerToDamage[i].CompareTag("Player"))
                        {
                            GetComponent<Animator>().SetBool("attack", true);
                            playerToDamage[i].GetComponent<PlayerController>().PlayerInjured(valDamage);
                        }
                        timeBtwAttack = startTimeBtwAttack;
                    }
                    else
                    {
                        timeBtwAttack -= Time.deltaTime;
                    }
                }
            }
            else
            {
                GetComponent<Animator>().SetBool("attack", false);
            }
        }
    }

    protected virtual void EnemyMovement()
    {
        if (faceLeft)
        {
            if (collEnemy.IsTouchingLayers(ground) && transform.position.x >= leftPosition)
            {
                rigidbodyEnemy.velocity = new Vector2(-moveSpeed, 0f);

            }
            if (transform.position.x < leftPosition)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (collEnemy.IsTouchingLayers(ground) && transform.position.x <= rightPosition)
            {
                rigidbodyEnemy.velocity = new Vector2(moveSpeed, 1f);
            }
            if (transform.position.x > rightPosition)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = true;
            }
        }

    }

    protected void RunAnimation()
    {
        animatorEnemy.SetFloat("run", Mathf.Abs(rigidbodyEnemy.velocity.x));
    }

    protected void InstantiateTheProp(GameObject propPrefab)
    {
        Instantiate(propPrefab, transform.position, Quaternion.identity);
    }

    public void EnemyInjured(int damage)
    {
        audioSourceEnemy.Play();
        injured = true;
        GetComponent<Animator>().SetBool("injured", true);
        HitBack();

        health -= damage;

        GameObject hitEffectObj = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(hitEffectObj, 0.3f);

        if (health <= 0)
        {
            dead = true;
            GetComponent<Animator>().SetBool("die", true);
            if (assign)
            {
                Instantiate(propPrefab, transform.position, Quaternion.identity);
            }
            this.gameObject.layer = 0;  //switch Layer to default or Player could attack this during death animation
        }

        StartCoroutine(Reset());
    }

    protected void HitBack()
    {
        signOfLocalScale = transform.localScale.x / Mathf.Abs(transform.localScale.x);
        this.transform.position = new Vector2(transform.position.x - (signOfLocalScale * 0.2f), transform.position.y);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1.5f);

        GetComponent<Animator>().SetBool("injured", false);
        injured = false;
    }

    public void ShowSoliloquize()
    {
        soliloquize.SetActive(true);
    }

    protected void DestroyCorp()
    {
        Destroy(gameObject);
    }
}

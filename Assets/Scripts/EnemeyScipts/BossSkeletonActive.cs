using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSkeletonActive : SkeletonActive
{
    private bool isDead, starting;
    private float timeToStun;

    public Transform explorer;
    public float maxHP = 400f, currentHP = 400f;
    public Image healthPointImage, healthPointEffect;
    public GameObject statueEyeLeft, statueEyeRight;
    public GameObject healthUI;
    public GameObject dialogWithPlayer;
    public GameObject dialogWhenDie, keyForCave;

    protected override void Start()
    {
        base.Start();
        rigidbodySkeleton = GetComponent<Rigidbody2D>();
        collSkeleton = GetComponent<Collider2D>();
        animatorEnemy = GetComponent<Animator>();
        startTimeBtwAttack = 0.7f;
        isDead = false;
        starting = false;
        timeToStun = 0;
        statueEyeLeft.SetActive(true);
        statueEyeRight.SetActive(true);
        healthUI.SetActive(true);
        dialogWithPlayer.SetActive(true);
        Player = GameObject.Find("$Player");
    }
    private void Update()
    {
        if (starting)
        {
            if (!injured && !isDead)
            {
                PlayAttackAnimation();
                EnemyMovement();
                Attacking();
            }
            Stun();
            GoWhere();
            RunAnimation();
            LifeStatus();
        }
        StartAttack();
    }

    private void StartAttack()
    {
        if (!dialogWithPlayer.activeInHierarchy)
        {
            starting = true;
        }
    }

    private void Stun()
    {
        timeToStun += Time.deltaTime;
        if (timeToStun >= 5)
        {
            GetComponent<Animator>().SetBool("injured", true);
            injured = true;
            StartCoroutine(Reset());
        }
    }

    private void PlayAttackAnimation()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(explorer.position, attackRange, whatIsPlayer);
        if (player.Length != 0)
        {
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i].CompareTag("Player"))
                {
                    GetComponent<Animator>().SetBool("attack", true);
                }
            }
        }
    }

    public void Injured(int damage)
    {
        currentHP -= damage;

        GameObject hitEffectObj = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(hitEffectObj, 0.3f);

        if (currentHP <= 0)
        {
            GetComponent<Animator>().SetBool("die", true);
            StartCoroutine(OpenDialogWhenDie());
            isDead = true;
            healthUI.SetActive(false);
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().isTrigger = true;
            keyForCave.SetActive(true);
        }
    }

    IEnumerator OpenDialogWhenDie()
    {
        yield return new WaitForSeconds(2f);
        dialogWhenDie.SetActive(true);
    }

    protected void LifeStatus()
    {
        healthPointImage.fillAmount = currentHP / maxHP;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f);

        GetComponent<Animator>().SetBool("injured", false);
        injured = false;
        timeToStun = 0;
    }
}

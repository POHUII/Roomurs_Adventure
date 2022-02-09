using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatActive : MonoBehaviour
{
    Rigidbody2D rigid;

    private bool faceLeft;
    private float positionPlayer;
    private GameObject player;
    private float timeBtwAttack, startTimeBtwAttack;
    private GameObject prefabChest;

    public float moveSpeed;
    public LayerMask whatIsPlayer;
    public float attackRange;
    public Transform attackPos;
    public GameObject hitEffect;
    public GameObject explosionVFXPrefab;
    public int valDamage;
    public Image healthPointImage, healthPointEffect;
    public GameObject healthUI;
    public float currentHP, maxHP;
    public AudioSource bgm, hit, soundWingWave;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        faceLeft = true;
        startTimeBtwAttack = 0.8f;
        player = GameObject.Find("$Player");
    }

    private void Update()
    {
        positionPlayer = player.transform.position.x;

        if (Mathf.Abs(transform.position.x - positionPlayer) > 0.1f)
            BatMovement();

        Attacking();
        LifeStatus();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prop"))
        {
            healthUI.SetActive(true);
            other.GetComponent<AudioSource>().Play();
            bgm.Play();
            Destroy(other.gameObject, 2f);
        }
    }

    private void BatMovement()
    {
        if (faceLeft)
        {
            if (transform.position.x >= positionPlayer)
            {
                rigid.velocity = new Vector2(-moveSpeed, 0f);

            }
            if (transform.position.x < positionPlayer)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (transform.position.x <= positionPlayer)
            {
                rigid.velocity = new Vector2(moveSpeed, 0f);
            }
            if (transform.position.x > positionPlayer)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = true;
            }
        }

    }

    private void Attacking()
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

    public void Injured(int damage)
    {
        GetComponent<AudioSource>().Play();
        currentHP -= damage;

        GameObject hitEffectObj = Instantiate(hitEffect, transform.position, Quaternion.identity) as GameObject;
        Destroy(hitEffectObj, 0.3f);

        if (currentHP <= 0)
        {
            GetComponent<Animator>().SetBool("die", true);
            healthUI.SetActive(false);

            QuitBackMusic();

            bool chestCreate = false;
            prefabChest = Resources.Load<GameObject>("Prefabs/chest");
            if (!chestCreate)
            {
                chestCreate = true;
                Instantiate(prefabChest, transform.position, Quaternion.identity);
            }
            Destroy(gameObject, 1f);

        }
    }

    private void QuitBackMusic()
    {
        bgm.volume -= Time.deltaTime * 0.3f;
    }

    public void SoundWingWave()
    {
        soundWingWave.Play();
    }

    private void LifeStatus()
    {
        healthPointImage.fillAmount = currentHP / maxHP;
    }
}

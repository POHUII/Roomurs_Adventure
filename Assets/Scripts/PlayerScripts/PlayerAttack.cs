using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    private const int valSwordDamage = 10;
    private GameObject arrowPrefab;
    private GameObject effectHeavyHit;

    public float curValBuffer = 0;
    public int arrowNum;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public float attackRangeX, attackRangeY;
    public LayerMask whatIsEnemy;
    public int damage;
    public Text textArrowNumber;
    public Transform positionHeavyHit;
    /* when curValBuffer equals valBuffer,the heavy hit release. */
    public float valBuffer;
    public Image bufferImage, bufferEffect;

    public static PlayerAttack _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        arrowPrefab = Resources.Load<GameObject>("Prefabs/arrowPlayer");
        arrowNum = 5;

        effectHeavyHit = Resources.Load<GameObject>("Prefabs/heavyHit");
    }

    private void Update()
    {
        Attacking();
        ShowArrowNumber();
        BufferBarStatus();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("arrow"))
            AddArrow();
    }

    public void RefreshObjects()
    {
        if (_instance.bufferImage == null)
        {
            bufferImage = GameObject.Find("BufferImage").GetComponent<Image>();
        }
        if (_instance.bufferEffect == null)
        {
            bufferEffect = GameObject.Find("BufferEffect").GetComponent<Image>();
        }
        if (_instance.textArrowNumber == null)
        {
            textArrowNumber = GameObject.Find("ArrowNumber/Number").GetComponent<Text>();
        }
    }

    public void AddArrow()
    {
        arrowNum += 1;
    }

    private void Attacking()
    {
        if (PlayerController.swordModeRelease)
        {
            if (Input.GetButtonDown("HitButton"))
            {
                attackSword();
            }
            else
            {
                GetComponent<Animator>().SetBool("attacking", false);
            }

            if (Input.GetKeyDown(KeyCode.K) && curValBuffer >= valBuffer)
            {
                GetComponent<Animator>().SetBool("heavyhit", true);
                Invoke("HeavyHit", 0.5f);
            }

            if (Input.GetButton("HitButton") && PlayerController.bowMode && PlayerController.bowModeRelease)
            {
                /* press the hit button persistently -> pull bowstring  */
                GetComponent<Animator>().SetBool("bow_attacking", true);
            }
            else if (Input.GetButtonUp("HitButton") && PlayerController.bowMode && PlayerController.bowModeRelease)
            {
                /* release the hit button -> shoot arrow */
                GetComponent<Animator>().SetBool("bow_attacking", false);
                if (arrowNum > 0)
                {
                    GameObject arrow = Instantiate(arrowPrefab, attackPos.position, Quaternion.identity) as GameObject;
                    arrow.GetComponent<ArrowMovement>().DetectDirection(transform.localScale.x);
                    arrowNum -= 1;
                }
            }
        }

    }

    private void HeavyHit()
    {
        //============================================================================================================
        //  plus 2 on positionHeavyHit or positionSecondHit when face right and minus 2 when face left
        //  use localScaleSign to detect use plus or minus
        //============================================================================================================
        float localScaleSign = transform.localScale.x / Mathf.Abs(transform.localScale.x);

        Vector2 positionSecondHit = new Vector2(positionHeavyHit.position.x + 2f * localScaleSign, positionHeavyHit.position.y);
        Vector2 positionThirdHit = new Vector2(positionSecondHit.x + 2f * localScaleSign, positionHeavyHit.position.y);

        Instantiate(effectHeavyHit, positionHeavyHit.position, Quaternion.identity);
        Instantiate(effectHeavyHit, positionSecondHit, Quaternion.identity);
        Instantiate(effectHeavyHit, positionThirdHit, Quaternion.identity);

        GetComponent<Animator>().SetBool("heavyhit", false);
        curValBuffer = 0;
    }

    private void attackSword()
    {
        GetComponent<Animator>().SetBool("attacking", true);
        Collider2D[] enemiesToDamage = (Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), 0, whatIsEnemy));
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            curValBuffer += 4f;

            if (enemiesToDamage[i].CompareTag("Dummy"))
                enemiesToDamage[i].GetComponent<DummyActive>().DummyInjured(valSwordDamage);

            if (enemiesToDamage[i].CompareTag("Slime"))
                enemiesToDamage[i].GetComponent<SlimeActive>().Death();

            if (enemiesToDamage[i].CompareTag("Skeleton"))
            {
                enemiesToDamage[i].GetComponent<SkeletonActive>().EnemyInjured(valSwordDamage);
                InitEnemy.enemyRest--;
            }

            if (enemiesToDamage[i].CompareTag("Archer"))
            {
                enemiesToDamage[i].GetComponent<ArcherController>().DropHealthPotion();
                enemiesToDamage[i].GetComponent<ArcherController>().EnemyInjured(valSwordDamage);
            }
            if (enemiesToDamage[i].CompareTag("BossSkeleton"))
            {
                enemiesToDamage[i].GetComponent<BossSkeletonActive>().Injured(valSwordDamage);
            }
            if (enemiesToDamage[i].CompareTag("Spider"))
            {
                enemiesToDamage[i].GetComponent<SkeletonActive>().EnemyInjured(valSwordDamage);
                Invoke("Soliloquize", 1.2f);
            }
            if (enemiesToDamage[i].CompareTag("Bat"))
            {
                enemiesToDamage[i].GetComponent<BatActive>().Injured(valSwordDamage);
            }
            if (enemiesToDamage[i].CompareTag("Syris"))
            {
                enemiesToDamage[i].GetComponent<SyrisController>().SyrisDead();
                EndingDesision.isBadEnding = true;
            }
        }
    }

    private void ShowArrowNumber()
    {
        if (textArrowNumber != null)
            textArrowNumber.text = arrowNum.ToString();
    }

    private void BufferBarStatus()
    {
        bufferImage.fillAmount = curValBuffer / valBuffer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }

}


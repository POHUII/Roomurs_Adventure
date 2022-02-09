using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    protected float horizontalMove;
    protected Rigidbody2D rigidbodyPlayer;
    protected Animator anim;
    protected bool jumpPressDone, onGround;
    protected int countPressQ;

    private GameObject propManager;
    private bool isCreatePortal;
    private bool openBillboard;
    private GameObject UIManager;
    private AudioSource audioPlayer;

    public bool isBagCanUse;

    [Header("血量")]
    public static float maxHP = 100f, currentHP = 10f;
    public Image healthPointImage, healthPointEffect;

    [Header("攻击状态")]
    public static bool bowMode, bowModeRelease;
    public static bool swordModeRelease;

    [Header("移动")]
    public float moveSpeed;
    public float jumpForce;

    private float startTimeRoll = 0;
    private float currentTime = 0;
    private Transform startPosition;
    public float timeToRoll;

    private bool climbing = false;
    public LayerMask ladder;

    [Header("物理检测")]
    private Collider2D collPlayer;
    public LayerMask ground;
    public Transform gourndCheckPoint;
    public static bool jumping, injured;

    public bool firstEdit, thirdEdit;
    public float leftPortalPositionX;   //30.4f at first time
    public float rightPortalPositionX;  //31.8f at first time


    [Header("UI相关")]
    public bool showArrowPanel;
    public GameObject myBag;
    public GameObject panelArrowNumber;
    public AudioSource levelFourBgm;
    public GameObject failureScreen;
    public GameObject bagButton;
    bool isOpen;

    public static PlayerController _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        currentHP = maxHP;
    }

    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collPlayer = GetComponent<Collider2D>();
        audioPlayer = GetComponent<AudioSource>();

        propManager = GameObject.Find("BattleSpace");
        UIManager = GameObject.Find("UIManager");

        isCreatePortal = true;
        isBagCanUse = false;

        Initialize();

        openBillboard = false;

        countPressQ = 0;


        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        CreatePortal();
        DetectJump();
        LifeStatus();
        SwitchAttackMode();
        OpenMyBag();
        PlayerRoll();

        if (isBagCanUse)
            bagButton.SetActive(true);
        if (showArrowPanel)
            panelArrowNumber.SetActive(true);
    }

    private void FixedUpdate()
    {
        DetectPlayerMove();
        Jump();
        SwitchAnim();
    }

    private void Initialize()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (sceneIndex)
        {
            case 1:
            case 2:
                swordModeRelease = false;
                bowModeRelease = false;
                break;
            case 3:
                swordModeRelease = true;
                bowModeRelease = false;
                bowMode = false;
                break;
            case 4:
            case 5:
                swordModeRelease = true;
                bowModeRelease = true;
                bowMode = false;
                break;
        }
    }

    //===========================================================================
    //  Refresh the objects such as UI or another GameObjects when switch scenes
    //===========================================================================
    public void RefreshObjects()
    {
        if (healthPointImage == null)
        {
            healthPointImage = GameObject.Find("HealthPointImage").GetComponent<Image>();
        }
        if (healthPointEffect == null)
        {
            healthPointEffect = GameObject.Find("HealthPointEffect").GetComponent<Image>();
        }

        if (propManager == null)
        {
            propManager = GameObject.Find("BattleSpace");
        }
        if (panelArrowNumber == null)
        {
            panelArrowNumber = GameObject.Find("ArrowNumber");
        }
        if (_instance.myBag == null)
        {
            myBag = GameObject.Find("Bag");
        }
        myBag.SetActive(false);

        panelArrowNumber.SetActive(false);

        if (bagButton == null)
        {
            bagButton = GameObject.Find("BagButton");
        }
        bagButton.SetActive(false);

        if (failureScreen == null)
        {
            failureScreen = GameObject.Find("Failure Screen");
        }
        failureScreen.SetActive(false);
    }

    private void CreatePortal()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3 && transform.position.x >= leftPortalPositionX && transform.position.x <= rightPortalPositionX && isCreatePortal)
        {
            propManager.GetComponent<PropManager>().CreatPortal();
            isCreatePortal = false;
        }
    }

    //========================================================================
    // You can only get the bag's function in level02 after talk with Merchant
    //========================================================================
    public void OpenBagMode()
    {
        isBagCanUse = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("LoadManager"))
        {
            other.GetComponent<LoadManager>().LoadNextLevel();
        }

        if (other.transform.CompareTag("Bow"))
        {
            other.gameObject.GetComponent<PropManager>().BowCollected();
            propManager.GetComponent<PropManager>().ClosePortal();

            /* first time to collected the Bow,start the bow mode */
            bowModeRelease = true;
            panelArrowNumber.SetActive(true);
        }

        if (other.transform.CompareTag("Sword"))
        {
            swordModeRelease = true;
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Music"))
        {
            other.GetComponent<AudioSource>().Play();
        }

        if (other.transform.CompareTag("Spider"))
        {
            other.GetComponent<SpiderActive>().DenBroken();
        }

        if (other.transform.CompareTag("Syris"))
        {
            other.GetComponent<PropManager>().LoadSyris();
            levelFourBgm.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Door"))
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("open");
            other.gameObject.GetComponent<AudioSource>().Play();
            other.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void DetectPlayerMove()
    {
        onGround = Physics2D.OverlapCircle(gourndCheckPoint.position, 0.1f, ground);

        if (!injured)
            GroundMovement();
    }

    public void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal"); // only return -1,0,1
        rigidbodyPlayer.velocity = new Vector2(horizontalMove * moveSpeed, rigidbodyPlayer.velocity.y);

        if (horizontalMove != 0)
            transform.localScale = new Vector3(horizontalMove, 1, 1);
    }

    //==========================================================================================
    // Player roll -> dodge
    // cd time:timeToRoll seconds
    // set Player's layer to another layers besides "Player" -> the Enemies couldn't attack it
    //==========================================================================================
    private void PlayerRoll()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            /* cd time's up,set Player's Layer to "default" */
            anim.SetBool("roll", true);
            this.gameObject.layer = 15;  // Layer 15:PlayerRolling

            Invoke("RelieveRoll", 0.7f);
        }
    }

    /* reset the dodge */
    private void RelieveRoll()
    {
        anim.SetBool("roll", false);
        this.gameObject.layer = 10; // Layer 10:Player
    }

    public void SwitchAnim()
    {
        if (bowMode && bowModeRelease)
        {
            anim.SetBool("idle", false);
            anim.SetBool("bow_idle", true);
        }
        else
        {
            anim.SetBool("bow_idle", false);
        }

        if (!injured)
        {
            if (bowMode && bowModeRelease)
                anim.SetFloat("bow_running", Mathf.Abs(rigidbodyPlayer.velocity.x));
            else
                anim.SetFloat("running", Mathf.Abs(rigidbodyPlayer.velocity.x));
        }

        if (rigidbodyPlayer.velocity.y < 1.3f && !collPlayer.IsTouchingLayers(ground) && !injured)
        {
            if (bowMode && bowModeRelease)
                anim.SetBool("bow_falling", true);
            else
                anim.SetBool("falling", true);
        }

        if (onGround)
        {
            anim.SetBool("falling", false);
            anim.SetBool("bow_falling", false);
        }
        else if (!onGround && rigidbodyPlayer.velocity.y > 2f)
        {
            if (bowMode && bowModeRelease)
                anim.SetBool("bow_jumping", true);
            else
                anim.SetBool("jumping", true);
        }
        else if (rigidbodyPlayer.velocity.y < -1.3f && !onGround)
        {
            if (bowMode && bowModeRelease)
            {
                anim.SetBool("bow_jumping", false);
                anim.SetBool("bow_falling", true);
            }
            else
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }

        if (injured)
        {
            anim.SetBool("hurt", true);
        }
        else
        {
            anim.SetBool("hurt", false);
        }
    }

    public void Jump()
    {
        if (onGround)
        {
            jumping = false;
        }
        if (jumpPressDone && onGround)
        {
            jumping = true;
            rigidbodyPlayer.velocity = new Vector2(rigidbodyPlayer.velocity.x, jumpForce);
            jumpPressDone = false;
        }
    }

    protected void LifeStatus()
    {
        healthPointImage.fillAmount = currentHP / maxHP;

        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public void DetectJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressDone = true;
        }
    }

    public void PlayerInjured(int damage)
    {
        injured = true;
        currentHP -= damage;

        if (currentHP <= 0)
        {
            anim.SetTrigger("Die");
            failureScreen.SetActive(true);
        }

        StartCoroutine(PlayerReset());
    }

    IEnumerator PlayerReset()
    {
        yield return new WaitForSeconds(0.3f);
        injured = false;
    }
    public void SwitchAttackMode()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            countPressQ++;
            if (countPressQ % 2 == 1)
            {
                bowMode = true;
            }
            else if (countPressQ % 2 == 0)
            {
                bowMode = false;
            }
        }

        if (countPressQ >= 2)
            countPressQ = 0;
    }

    void OpenMyBag()
    {
        if (Input.GetKeyDown(KeyCode.E) && isBagCanUse)
        {
            isOpen = !isOpen;
            myBag.SetActive(isOpen);
            InventoryManager.RefreshItem();
        }
    }

    public void DestroyCorp()
    {
        Destroy(gameObject);
    }
}

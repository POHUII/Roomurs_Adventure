using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorssBowController : MonoBehaviour
{
    enum CrossbowState
    {
        idle,
        shootOut,
        reset,
    }

    Animator animCrossbow;
    AudioSource crossbowString;

    private GameObject prefabArrow;
    private bool canShoot;
    private CrossbowState currentState;
    private GameObject player;

    public Transform shootPos;
    public GameObject buttonUse;

    private void Start()
    {
        animCrossbow = GetComponent<Animator>();
        crossbowString = GetComponent<AudioSource>();

        prefabArrow = Resources.Load<GameObject>("Prefabs/hugeArrow");

        canShoot = true;

        currentState = CrossbowState.idle;

        player = GameObject.Find("$Player");
    }

    private void Update()
    {
        OpenFire();

        if (currentState == CrossbowState.shootOut)
        {
            Invoke("ResetCrossbow", 8f);
        }

        if (currentState == CrossbowState.reset)
        {
            Invoke("SetCrossbowIdle", 2f);
        }

        if (currentState == CrossbowState.idle)
            canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            buttonUse.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            buttonUse.SetActive(false);
    }

    private void OpenFire()
    {
        if (Input.GetKeyDown(KeyCode.F) && canShoot && Mathf.Abs(transform.position.x - player.transform.position.x) < 1.5f)
        {
            animCrossbow.SetBool("idle", false);
            animCrossbow.SetTrigger("shoot");
            GameObject arrow = Instantiate(prefabArrow, shootPos.transform.position, Quaternion.identity) as GameObject;
            arrow.GetComponent<ArrowMovement>().DetectDirection(transform.localScale.x * -1);
            currentState = CrossbowState.shootOut;
            canShoot = false;
        }
    }

    private void ResetCrossbow()
    {
        animCrossbow.SetTrigger("reset");
        currentState = CrossbowState.reset;
    }

    private void SetCrossbowIdle()
    {
        animCrossbow.ResetTrigger("reset");
        animCrossbow.SetBool("idle", true);
        currentState = CrossbowState.idle;
    }

    public void WindUp()
    {
        crossbowString.Play();
    }
}

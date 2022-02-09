using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    private GameObject leftPortal, rightPortal, LeftPortal, RightPortal;
    private GameObject healthPotion;
    private bool isOpenBag;
    private Vector2 leftPortalPosition = new Vector2(21f, -0.24f);
    private Vector2 rightPortalPosition = new Vector2(41f, -0.24f);

    public GameObject Bag;
    public GameObject syris;

    private void Start()
    {
        healthPotion = Resources.Load<GameObject>("Prefabs/health_potion");
        isOpenBag = false;
    }

    public void SetActiveProperty()
    {
        gameObject.SetActive(true);
    }

    public void CoinCollected()
    {
        GetComponent<Animator>().SetBool("collect", true);
        Destroy(gameObject, 0.2f);
    }

    public void CreatPortal()
    {
        leftPortal = Resources.Load<GameObject>("Prefabs/LeftPortal");
        rightPortal = Resources.Load<GameObject>("Prefabs/RightPortal");
        LeftPortal = Instantiate(leftPortal, leftPortalPosition, Quaternion.identity) as GameObject;
        RightPortal = Instantiate(rightPortal, rightPortalPosition, Quaternion.identity) as GameObject;
    }

    public void ClosePortal()
    {
        LeftPortal.GetComponent<Animator>().SetTrigger("ClosePortal");
        RightPortal.GetComponent<Animator>().SetTrigger("ClosePortal");
        Destroy(LeftPortal, 0.3f);
        Destroy(RightPortal, 0.3f);
    }

    public void BowCollected()
    {
        Destroy(gameObject);
    }


    public void HealthPotionDrop()
    {
        Instantiate(healthPotion, new Vector2(28.61f, 0.42f), Quaternion.identity);
    }

    public void BagButtonOnClicked()
    {
        isOpenBag = !isOpenBag;
        Bag.SetActive(isOpenBag);
    }

    public void LoadSyris()
    {
        syris.SetActive(true);
    }
}

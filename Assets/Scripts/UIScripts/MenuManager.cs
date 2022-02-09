using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool menuState;
    private Animator animatorMenu;

    public GameObject menuScreen;

    public Transform entrance;

    private void Start()
    {
        if (entrance == null)
            entrance = GameObject.Find("Entrance").transform;

        if (!GameSaveManager.loadingPlayer)
            PlayerController._instance.transform.position = entrance.position;

        PlayerAttack._instance.RefreshObjects();
        PlayerController._instance.RefreshObjects();
        print("Refresh");
        menuState = false;
        animatorMenu = menuScreen.GetComponent<Animator>();
    }

    private void Update()
    {
        ChangeMenuStateByKey();
        menuScreen.SetActive(menuState);
    }

    private void ChangeMenuStateByKey()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            print(menuState);
            menuState = !menuState;
        }
    }

    public void ChangeMenuStateByClick()
    {
        menuState = !menuState;
    }

    public void Save()
    {
    }

    public void Menu()
    {
        animatorMenu.SetBool("Close", true);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

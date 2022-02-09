using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillSyris : MonoBehaviour
{
    public GameObject syris, doorClose;
    public GameObject fadeToBlack;

    private void Update()
    {
        if (!syris && !doorClose)
        {
            StartCoroutine(FadeToBlack());
            Invoke("EnterNextLevel", 5f);
        }
    }

    IEnumerator FadeToBlack()
    {
        yield return new WaitForSeconds(3f);
        fadeToBlack.SetActive(true);
    }

    public void EnterNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

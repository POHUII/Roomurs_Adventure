using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailScreenManager : MonoBehaviour
{
    public GameObject failedScreen;
    public GameObject loadScreen;
    public Slider slider;
    public Text textPercent;

    public void RestartGame()
    {
        PlayerController.currentHP = 100;
        PlayerController.injured = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator Restart()
    {
        failedScreen.SetActive(false);
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;

            textPercent.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                slider.value = 1;

                textPercent.text = "Press AnyKey to continue";

                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}

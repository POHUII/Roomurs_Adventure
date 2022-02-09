using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text textPercent;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadLevelOne()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int sceneIndex)
    {
        loadScreen.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false; // Don't load the next level automatically when the slider had filled

        while (!operation.isDone)
        {
            slider.value = operation.progress;

            textPercent.text = operation.progress * 100 + "%";

            if (operation.progress >= 0.9f)
            {
                //================================================================================
                //  set the progress's value to 100 because the load of next scene is finish when 
                //  progress's value is 90 but we canceled the load next level automatically
                //================================================================================
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

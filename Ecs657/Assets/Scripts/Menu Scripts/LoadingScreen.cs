using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public Slider loadingSlider;
    //public GameObject loadingScreen;
    public TMP_Text progressText;
    
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    //displays the progress of loading a scene in %
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        //loadingScreen.SetActive(true);
        gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Math.Clamp(operation.progress,0,1) / 0.9f;
            loadingSlider.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }


    }

}

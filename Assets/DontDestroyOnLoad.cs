using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyOnLoad : MonoBehaviour
{

    void Start()
    {
        var canvasObject = GameObject.FindGameObjectsWithTag("Canvas");
        int numCanvas = canvasObject.Length;
        if (numCanvas != 1)
        {
            Destroy(canvasObject[1].gameObject);
        }
        else
        {

            DontDestroyOnLoad(this.gameObject);
            SceneManager.activeSceneChanged += DestroyOnMenuScreen;
        }

        void DestroyOnMenuScreen(Scene oldScene, Scene newScene)
        {
            if (gameObject != null)
            {
                if (newScene.buildIndex == 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
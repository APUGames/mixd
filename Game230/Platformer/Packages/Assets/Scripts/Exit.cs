﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2.0f;
    [SerializeField] float sloMoFactor = 0.2f;

    void OnTiggerEnter2D(Collider2D other)
    { 
            StartCoroutine(LoadNextLevel());
    }

   IEnumerator LoadNextLevel()
{
        print("Collide?");
        Time.timeScale = sloMoFactor; 

    yield return new WaitForSecondsRealtime(LevelLoadDelay);

        Time.timeScale = 1.0f; 
            
            var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

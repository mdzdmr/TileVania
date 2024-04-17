// Importing libraries.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class handles the logic for the player once they complete a level.
public class LevelExit : MonoBehaviour
{
    // Setting a constant time to load to the next level.
    [SerializeField] float levelLoadDelay = 1f;
    
    // Method called when another collider enters the trigger collider.
    void OnTriggerEnter2D(Collider2D other) { if (other.tag == "Player") { StartCoroutine(LoadNextLevel()); } }

    // Coroutine to delay the loading of the next level.
    IEnumerator LoadNextLevel() {
        // Waiting for the specified delay time.
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // Looping back to the first scene if the next scene index exceeds the number of scenes in build settings.
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) { nextSceneIndex = 0; }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
    }

}

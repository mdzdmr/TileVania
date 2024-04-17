// Importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for persisting certain scene data across scene loads.
public class ScenePersist : MonoBehaviour
{
    // Awake is called when the script instance is loaded.
    void Awake() {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        // If there is more than one ScenePersist instance it means a new scene was loaded where another ScenePersist exists,
        // so we destroy this one to avoid duplicates.
        if (numScenePersists > 1) { Destroy(gameObject); } 
        else{ DontDestroyOnLoad(gameObject); }
    }

    // This method is used to destroy the ScenePersist object.
    public void ResetScenePersist() { Destroy(gameObject); }

}

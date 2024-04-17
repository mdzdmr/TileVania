// Importing libraries.
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// This class manages the game session, tracking player lives, score and level persistence.
public class GameSession : MonoBehaviour
{
    // Setting a constant player life count. 
    [SerializeField] int playerLives = 3;
    // Score to start the game with.
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    // This method is called when the script is loaded.
    void Awake() {
        // If there is more than one game session instance then it destroys that instance and does not 
        // delete the exisiting game session incase a new session is loaded.
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) { Destroy(gameObject); } 
        else{ DontDestroyOnLoad(gameObject); }
    }

    // This method is called before the first frame update.
    void Start() {
        // Updating the lives and score UI text with the current values.
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();    
    }

    // This method is called when the player dies.
    public void ProcessPlayerDeath() {
        // If the player still has more than one life then take one away and if none are left then reset the game session.
        if (playerLives > 1) { TakeLife(); }
        else { ResetGameSession(); }
    }

    // This method is called to increase the score in the game session.
    public void AddToScore(int pointsToAdd) {
        // Increasing the score by the amount of points and updating the score text on the screen.
        score += pointsToAdd;
        scoreText.text = score.ToString(); 
    }

    // Reduced the player's lives and reloads the current level.
    void TakeLife() {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    // Resets the game session when the player runs out of lives.
    void ResetGameSession() {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }


}

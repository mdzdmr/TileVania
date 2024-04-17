// Importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Method to handle coin pickup.
public class CoinPickup : MonoBehaviour
{
    // Audio clip to play when the coin is picked up.
    [SerializeField] AudioClip coinPickupSFX;
    // The amount of points a coin gives when picked up.
    [SerializeField] int pointsForCoinPickup = 100;
    // Boolean to check wether coin has been picked up or not.
    bool wasCollected = false;
    // Method called when another collider enters the trigger collider.
    void OnTriggerEnter2D(Collider2D other) 
    {
        // Checking if the colliding object is the player and not other objects
        // like enemies or any other sprites and making sure it wasn't collected.
        if (other.tag == "Player" && !wasCollected) {
            // Marking as collected to prevent multiple collections.
            wasCollected = true;
            // Add points for the coin pickup to the player's score.
            FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
            // Playing the coint pickup sound at the camera's position.
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            // Deactivating the coin game object.
            gameObject.SetActive(false);
            // Destroying the coint game object to clean up.
            Destroy(gameObject);
        }
    }

}

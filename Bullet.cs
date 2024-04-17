// Importing libraries.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is responsible for the behavior of bullets in the game.
public class Bullet : MonoBehaviour {

    // Setting a constant bullet speed.
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    PlayerMovement player;
    float xSpeed;

    // This method is called before the first frame update.
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        // Setting the horizontal speed based on the player's facing direction multiplied by bullet speed.
        xSpeed = player.transform.localScale.x * bulletSpeed;
    
    }

    // Update is called once per framce and sets the bullet's velocity to make it move.
    void Update() { myRigidbody.velocity = new Vector2 (xSpeed, 0f); }

    // This method is triggered when the bullet's collider enters another collider marked as a trigger.
    void OnTriggerEnter2D(Collider2D other) {
        // If the collided object is tagged as "Enemy", it gets destroyed.
        if (other.tag == "Enemy") { Destroy(other.gameObject); }
        Destroy(gameObject);
    }

    // This method destroys the bullet upon collision with any object.
    void OnCollisionEnter2D(Collision2D other) { Destroy(gameObject); }

}

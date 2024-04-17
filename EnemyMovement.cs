// Importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the enemy movement behavior.
public class EnemyMovement : MonoBehaviour 
{
    // Setting a constant enemy movement speed.
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;

    // This method is called before the first frame update.
    void Start() { myRigidbody = GetComponent<Rigidbody2D>(); }

    // Update is called once per frame and set's the enemy's velocity to move it horizontally.
    void Update() { myRigidbody.velocity = new Vector2 (moveSpeed, 0f); }

    // This method is called when the collider attached to the enemy exits a trigger collider.
    void OnTriggerExit2D(Collider2D other) {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    // This function flips the enemy's facing direction based on the opponents velocity.
    // Flips the enemy sprite when they either go left or right.
    void FlipEnemyFacing() { transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f); }

}

// Importing libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// This class manages the player's movements and ingame interactions.
public class PlayerMovement : MonoBehaviour
{
    // Setting constant speeds for the players runnning, jumping and climbing speed, etc.
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;

    // Start is called before the first frame update.
    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update() {
        // If the player is not alive then exit the function otherwise continue with the movement functions.
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    // Input system action for firing bullets.
    void OnFire(InputValue value) {
        if (!isAlive) { return; }
        // Instantiate a bullet at the position of the gun.
        Instantiate(bullet, gun.position, transform.rotation);
    }

    // Input system action for moving the player.
    void OnMove(InputValue value) {
        if (!isAlive) { return; }
        // Getting the vector from the input system and storing it for movement.
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    // Input system action for making the player jump.
    void OnJump(InputValue value) {
        if (!isAlive) { return; }
        // Checking if the player's feet collider is touching the ground layer to allow jumping.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        // Applying a vertical velocity to the Rigidbody to make the player jump.
        if (value.isPressed) { myRigidbody.velocity += new Vector2(0f, jumpSpeed); }
    }

    // This method takes care of the running movement.
    void Run() {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        // Checking if the player is moving horizontally and updating the animator's parameter to control the running animation.
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    // This method flips the player's sprite depending on the direction of movement.
    void FlipSprite() {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        // Scaling the player's sprite to face the direction of movement.
        if (playerHasHorizontalSpeed) { transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f); }
    }

    // This method is called into affect when the player is climbing the ladder. 
    void ClimbLadder() {
        // Checking if the player is in contact with the ladder layer.
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) { 
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return; 
        }
        // Updating settings accordingly.
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    // This method handles the player's death.
    void Die() {
        // Check if the player has collided with enemy or hazard layers.
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            // Applying force to the player's Rigidbody to simulate a 'death kick'.
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

}

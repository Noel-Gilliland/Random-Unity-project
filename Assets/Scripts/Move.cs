using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Vector3 storedMoveDirection = Vector3.zero;


    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        
    }

    void Update()
    {
    groundedPlayer = controller.isGrounded;

    // Always read input for movement
    Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
    move = Vector3.ClampMagnitude(move, 1f);

    // Jump
    if (Input.GetButtonDown("Jump") && groundedPlayer)
    {
        playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
    }

    // Apply gravity
    playerVelocity.y += gravityValue * Time.deltaTime;

    // Combine horizontal and vertical movement
    Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
    controller.Move(finalMove * Time.deltaTime);
}
    /*void Update()
    {
        groundedPlayer = controller.isGrounded;


        // Horizontal input
        Vector3 move = Vector3.zero;

        if (groundedPlayer)
        {
            // Only accept input if on the ground
            move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
            move = Vector3.ClampMagnitude(move, 1f); // still prevents diagonal speed boost

            storedMoveDirection = move;
        }
        else {
            move = storedMoveDirection;
        }


        move = Vector3.ClampMagnitude(move, 1f); // Optional: prevents faster diagonal movement
        
        // Jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

         

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }*/

}

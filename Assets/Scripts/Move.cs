using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool istouchingwall;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;
    private float walljumps;
    public float maxwalljumps = 1.0f;
    public float minwallAngle = 60f; // Minimum angle to consider as a wall
    public float walljumpPush = 20.0f; // Push force when jumping off a wall

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
        if (controller.isGrounded)
        {
            groundedPlayer = true;
            walljumps = maxwalljumps;
        }
        else
        {
            groundedPlayer = false;
        }
     
    
        // Jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * 2.0f * -gravityValue);
        }

     

        // Apply gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up) + (playerVelocity.x * Vector3.right) + (playerVelocity.z * Vector3.forward);

        playerVelocity.x *= 0.9f; 
        playerVelocity.z *= 0.9f; 
        controller.Move(finalMove * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Vector3 surfaceNormal = hit.normal;
        float angle = Vector3.Angle(surfaceNormal, Vector3.up);
        if (angle < 90f && angle > minwallAngle && walljumps > 0)
        {
            istouchingwall = true;
            // If the player is touching a wall, allow jumping off it
            groundedPlayer = false; // Prevent jumping while touching the wall

            // Check if the player presses the jump button while touching the wall

            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * 2.0f * -gravityValue);
                playerVelocity.x = surfaceNormal.x * walljumpPush * 5.0f; // Push away from the wall
                playerVelocity.z = surfaceNormal.z * walljumpPush * 5.0f;
                walljumps -= 1.0f;
            }
            else
            {
                groundedPlayer = false;
            }
        }
    // surfaceNormal now holds the direction perpendicular to the surface you hit
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

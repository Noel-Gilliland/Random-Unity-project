using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Newmovecontroller : MonoBehaviour
{
   
    public RaycastHit hit;
    public float acceleration = 50f;

    public float playerSpeed = 2.0f;
    public float jumpheight = 2.0f; // Height of the jump
    public float gravity = -9.81f;
    private bool groundedPlayer;
    public Vector3 playerVelocity;
    private Vector3 wallbounce;
    private CharacterController controller;
    private float walljumps;
    public float maxwalljumps = 1.0f;
    public float minwallAngle = 60f; // Minimum angle to consider as a wall
    public float walljumpPush = 10.0f;
    public float wallbouncedecel = 80.0f; // Deceleration for wall bounce
    private bool istouchingwall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        hit.distance = 10f;

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
                playerVelocity.y = Mathf.Sqrt(jumpheight * 2.0f * -gravity);
                wallbounce.x = surfaceNormal.x * walljumpPush * 5.0f; // Push away from the wall
                wallbounce.z = surfaceNormal.z * walljumpPush * 5.0f;
                walljumps -= 1.0f;
            }
            else
            {
                groundedPlayer = false;
                wallbounce = Vector3.zero; // Reset wall bounce if not jumping
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            groundedPlayer = true;
            walljumps = maxwalljumps;
            wallbounce = Vector3.zero;
        }
        else
        {
            groundedPlayer = false;
            playerVelocity.y += gravity * Time.deltaTime * 3.0f;
        }

        groundedPlayer = controller.isGrounded;
        //scalar axial movement
        Vector3 inputDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        Vector3 targetVelocity = inputDirection * playerSpeed;

        playerVelocity.x = Mathf.Lerp(playerVelocity.x, targetVelocity.x, acceleration * Time.deltaTime);
        playerVelocity.z = Mathf.Lerp(playerVelocity.z, targetVelocity.z, acceleration * Time.deltaTime);

        //wallbounce = wallbounce * wallbouncedecel; // Reset wall bounce each frame
        wallbounce.x = Mathf.Lerp(wallbounce.x, 0.0f, 1 / wallbouncedecel * Time.deltaTime);
        wallbounce.z = Mathf.Lerp(wallbounce.z, 0.0f, 1 / wallbouncedecel * Time.deltaTime);
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpheight * 2.0f * -gravity);
        }

        if (wallbounce.x > 0.0f && wallbounce.z > 0.0f)
        {
            playerVelocity.x = playerVelocity.x - wallbounce.x; // Apply wall bounce effect
            playerVelocity.z = playerVelocity.z - wallbounce.z; // Apply wall bounce effect
        }



        Vector3 finalMove = new Vector3(playerVelocity.x + wallbounce.x, playerVelocity.y, playerVelocity.z + wallbounce.z);

        controller.Move(finalMove * Time.deltaTime);
        for (int i = 0; i < 8; i++)
        {
            if (Physics.Raycast(CollisionDetection.collisionRay[i], 30))
            {
                Debug.Log("Collision detected with: ");
            }

        }



    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
public class Newmovecontroller : MonoBehaviour
{
    public float acceleration = 10f;
    public float playerSpeed = 2.0f;
    private bool groundedPlayer;
    private CharacterController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        //scalar axial movement
        Vector3 inputDirection = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);

        Vector3 targetVelocity = inputDirection * playerSpeed;

        playerVelocity.x = Mathf.Lerp(playerVelocity.x, targetVelocity.x, acceleration * Time.deltaTime);
        playerVelocity.z = Mathf.Lerp(playerVelocity.z, targetVelocity.z, acceleration * Time.deltaTime);

        Vector3 finalMove = new Vector3(playerVelocity.x, playerVelocity.y, playerVelocity.z);
        controller.Move(finalMove * Time.deltaTime);
        float c = Update(2.0f, 3.0f);

    }

    
}
*/

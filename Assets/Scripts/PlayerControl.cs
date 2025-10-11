using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    InputAction moveAction;

    public float movementSmoothingSpeed;
    public float movementSpeed;
    public float rotationSpeed ;

    public Animator animator;
    public Rigidbody playerRb;
    public Camera mainCamera; 

    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;


    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rawInputMovement = new Vector3(moveValue.x, 0 , moveValue.y);

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
        animator.SetFloat("Velocity", smoothInputMovement.magnitude);

        MoveThePlayer();
        TurnThePlayer();

    }


    void MoveThePlayer()
    {
        Vector3 movement = movementSpeed * Time.deltaTime * CameraDirection(smoothInputMovement);
        playerRb.MovePosition(transform.position + movement);
    }

    void TurnThePlayer()
    {
        if (smoothInputMovement.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(playerRb.rotation, Quaternion.LookRotation(CameraDirection(smoothInputMovement)), rotationSpeed);
            playerRb.MoveRotation(rotation);
        }
    }


    Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = mainCamera.transform.forward;
        var cameraRight = mainCamera.transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        return cameraForward * movementDirection.z + cameraRight * movementDirection.x;

    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    InputAction moveAction;

    public float movementSmoothingSpeed = 0f;
    public Animator animator;
    public Rigidbody playerRb;

    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;


    private void Start()
    {
        animator = GetComponent<Animator>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rawInputMovement = new Vector3(moveValue.x, 0 , moveValue.y);

        smoothInputMovement = Vector3.Lerp(smoothInputMovement, rawInputMovement, Time.deltaTime * movementSmoothingSpeed);
        animator.SetFloat("Velocity", smoothInputMovement.magnitude);

        playerRb.MoveRotation(Quaternion.LookRotation(smoothInputMovement));
        playerRb.MovePosition(smoothInputMovement * 0.1f);

    }
}